using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Timers;
using System.Threading;
using Thecentury.Functions;
using Thecentury.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;


namespace Thecentury {

	public sealed partial class FunctionGrapher : ITimeControl {



		public event EventHandler RepaintNeeded;
		private void OnRepaintNeeded() {
			if (RepaintNeeded != null) {
				RepaintNeeded(this, null);
			}
		}

		public FunctionGrapher() {
			Initialize();
		}

		private void Initialize() {
			//redrawTimer = new System.Timers.Timer(askPeriod);
			//redrawTimer.Start();
			//redrawTimer.Elapsed += new ElapsedEventHandler(AskFunctions);
			Application.Idle += new EventHandler(AskFunctions);
		}

		private void AskFunctions(object sender, EventArgs e) {
			foreach (PraFunction f in functions) {
				if (f.IsDynamic) {
					recomputate = true;
					OnRepaintNeeded();
				}
			}
		}


		/// <summary>
		/// Paints function graphs
		/// </summary>
		/// <param name="g">Graphics, associated with object to draw on</param>
		/// <param name="r">Rectangle on screen</param>
		public void Paint(Graphics g, Rectangle r) {
			if (recomputate || r != to) {
				LoadViewPoints(r);
			}

			to = r;
			g.Clear(outerColor);

			//r = innerTo;
			Rectangle graphR = margin.Transform(r);

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

			using (Brush brush = new SolidBrush(innerColor)) {
				g.FillRectangle(brush, graphR);
			}

			using (new Thecentury.MyGraphics.RegionSaver(g, to)) {
				axisDivision.Paint(g, graphR, InnerTo);
			}
			g.SmoothingMode = graphsSmoothingMode;
			using (new Thecentury.MyGraphics.RegionSaver(g, InnerTo)) {
				foreach (PraFunction f in functions) {
					f.Paint(g);
				}
			}

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

			if (drawBorder) {
				using (Pen pen = new Pen(borderColor, borderWidth)) {
					g.DrawRectangle(pen, new Rectangle(graphR.Location, new Size(graphR.Width - 1, graphR.Height - 1)));
				}
			}

		}

		#region Settings

		/// <summary>
		/// Shall border be painted or not
		/// </summary>
		public bool DrawBorder {
			get {
				return drawBorder;
			}
			set {
				drawBorder = value;
			}
		}
		private bool drawBorder = false;

		/// <summary>
		/// Can equal functions be added or not?
		/// </summary>
		public bool DrawEqualFunctions {
			get {
				return drawEqualFunctions;
			}
			set {
				drawEqualFunctions = value;
			}
		}
		private bool drawEqualFunctions = true;

		private FloatingAxisDivision axisDivision = new FloatingAxisDivision();
		public FloatingAxisDivision AxisDivision {
			get {
				return axisDivision;
			}
			set {
				axisDivision = value;
			}
		}

		/// <summary>
		/// Rectangle in which we should computate function graph
		/// </summary>
		RectangleF from = new RectangleF(-10.0f, 10.0f, 20.0f, 20.0f);
		public RectangleF From {
			get { return from; }
			set {
				if (from != value) {
					axisDivision.ZoomScale(value.Width / from.Width, value.Height / from.Height);
					from = value;
					LoadGraphPoints();
				}
			}
		}

		private Rectangle to;

		private System.Drawing.Drawing2D.SmoothingMode graphsSmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		public System.Drawing.Drawing2D.SmoothingMode GraphsSmoothingMode {
			get { return graphsSmoothingMode; }
			set {
				graphsSmoothingMode = value;
				// todo перерисовывать надо!
			}
		}

		public Rectangle InnerTo {
			get {
				return margin.Transform(to);
			}
		}

		int askPeriod = 33;
		public int AskPeriod {
			get { return askPeriod; }
			set {
				askPeriod = value;
				this.redrawTimer.Interval = value;
			}
		}

		private Margin margin = new Margin(0.05f, 0.05f);
		public Margin Margin {
			get { return margin; }
			set { margin = value; }
		}

		#endregion


		/// <summary>
		/// Makes recomputating of points
		/// </summary>
		public void Recomputate() {
			recomputate = true;
			OnRepaintNeeded();
		}
		private bool recomputate = true;


		#region Functions

		private List<PraFunction> functions = new List<PraFunction>();
		public List<PraFunction> Functions {
			get { return functions; }
			set {
				functions = value;
				LoadGraphPoints();
				OnRepaintNeeded();
			}
		}

		public PraFunction LatestFunction {
			get {
				return functions[functions.Count - 1];
			}
			set {
				functions[functions.Count - 1] = value;
				LoadGraphPoints();
			}
		}

		public int NumberOfFunctions {
			get { return functions.Count; }
		}

		/// <summary>
		/// Adds functions to list of drawing functions
		/// </summary>
		/// <param name="f"></param>
		/// <returns>Number of added function in list of functions to draw; if value is -1 then function was not added</returns>
		public int AddFunction(PraFunction f) {
			int res = DelayedAddFunction(f);
			LoadGraphPoints();
			return res;
		}

		public int AddFunction(PraFunction f, string name) {
			f.Name = name;
			return AddFunction(f);
		}

		/// <summary>
		/// Adds new function
		/// </summary>
		public PraFunction NewFunction {
			set { this.AddFunction(value); }
		}

		private int DelayedAddFunction(PraFunction f) {
			if (!drawEqualFunctions) {
				foreach (PraFunction func in functions) {
					if (f.CompareTo(func) == 0) {
						return -1;
					}
				}
			}
			functions.Add(f);
			return functions.Count - 1;
		}

		/// <summary>
		/// Adds functions to list of drawing functions
		/// </summary>
		/// <param name="functions">Array of functions to add</param>>
		/// <returns>Array of numbers of added functions in list of functions to draw; if value is -1 then function was not added</returns>
		public int[] AddFunctions(params Function[] functions) {
			int[] res = new int[functions.Length];
			for (int i = 0; i < functions.Length; i++) {
				res[i] = DelayedAddFunction(functions[i]);
			}
			LoadGraphPoints();
			return res;
		}

		/// <summary>
		/// Adds functions to list of drawing functions
		/// </summary>
		/// <param name="functions">List of delegates, describing new functions</param>
		/// <returns>Array of numbers of added functions in list of functions to draw; if value is -1 then function was not added</returns>
		public int[] AddFunctions(List<Function> functions) {
			int[] res = new int[functions.Count];
			for (int i = 0; i < functions.Count; i++) {
				res[i] = DelayedAddFunction(functions[i]);
			}
			LoadGraphPoints();
			return res;
		}

		/// <summary>
		/// Clears list of drawing functions
		/// </summary>
		public void ClearFunctions() {
			functions.Clear();
			LoadGraphPoints();
			OnRepaintNeeded();
		}

		public void ChangeFunctionToNull(int number) {
			functions[number] = new NullFunction();
		}

		public void RemoveFunction(int number) {
			functions.RemoveAt(number);
			LoadGraphPoints();
		}

		public PraFunction GetFunctionByName(string name) {
			foreach (PraFunction f in functions) {
				if (f.Name == name) {
					return f;
				}
			}
			return null;
		}

		public void ReallyRemoveFunction(int number) {
			MessageBox.Show("Вы уверены, что вы хотите удалить функцию?", "Запрос подтверждения удаления", MessageBoxButtons.OKCancel);
		}

		/// <summary>
		/// Sets list of drawing functions
		/// </summary>
		/// <param name="lst"></param>
		public void SetFunctions(List<PraFunction> lst) {
			functions = lst;
			LoadGraphPoints();
		}

		#endregion


		private System.Timers.Timer redrawTimer;

		private void SetFrom(RectangleF value) {
			from = value;
			axisDivision.ZoomScale(value.Width / from.Width, value.Height / from.Height);
			axisDivision.From = from;
			foreach (PraFunction f in functions) {
				f.From = from;
			}
		}

		private void SetTo(Rectangle to) { }


		#region Colors & widths

		private Color borderColor = Color.Black;
		public Color BorderColor {
			get {
				return borderColor;
			}
			set {
				borderColor = value;
			}
		}

		private float borderWidth = 1.0f;
		public float BorderWidth {
			get { return borderWidth; }
			set { borderWidth = value; }
		}

		private Color innerColor = Color.LightYellow;
		public Color InnerColor {
			get {
				return innerColor;
			}
			set {
				innerColor = value;
			}
		}

		private Color outerColor = Color.LightGray;//Color.LightSteelBlue;
		public Color OuterColor {
			get { return outerColor; }
			set { outerColor = value; }
		}

		#endregion

		/// <summary>
		/// Loads points of graph
		/// </summary>
		private void LoadGraphPoints() {
			recomputate = true;
			axisDivision.From = from;
			foreach (PraFunction f in functions) {
				f.From = from;
			}
		}

		void redrawTimer_Elapsed(object sender, ElapsedEventArgs e) {
			OnRepaintNeeded();
		}

		/// <summary>
		/// Converts points of graph to points on the view plane
		/// </summary>
		/// <param name="viewPort">View port</param>
		private void LoadViewPoints(Rectangle viewPort) {
			recomputate = false;
			Rectangle innerTo = margin.Transform(viewPort);
			axisDivision.To = innerTo;
			foreach (PraFunction f in functions) {
				f.To = innerTo;
			}
		}

		#region ITimeControl Members

		public State State {
			get {
				State st = State.Different;
				foreach (GFunction f in functions) {
					if (f is ITimeControl) {
						if (st == State.Different) {
							st = (f as ITimeControl).State;
						}
						else {
							if (st != (f as ITimeControl).State) {
								return State.Different;
							}
						}
					}
				}
				return st;
			}
			set {
				if (value != State.Different) {
					foreach (GFunction f in functions) {
						if (f is ITimeControl) {
							(f as ITimeControl).State = value;
						}
					}
				}
			}
		}

		public AnimationDirection Direction {
			get {
				AnimationDirection dir = AnimationDirection.Different;
				foreach (GFunction f in functions) {
					if (f is ITimeControl) {
						if (dir == AnimationDirection.Different) {
							dir = (f as ITimeControl).Direction;
						}
						else {
							if (dir != (f as ITimeControl).Direction) {
								return AnimationDirection.Different;
							}
						}
					}
				}
				return dir;
			}
			set {
				if (value != AnimationDirection.Different) {
					foreach (GFunction f in functions) {
						if (f is ITimeControl) {
							(f as ITimeControl).Direction = value;
						}
					}
				}
			}
		}

		public float Speed {
			get { return 0; }
			set {
				foreach (GFunction f in functions) {
					if (f is ITimeControl) {
						(f as ITimeControl).Speed = value;
					}
				}
			}
		}

		public void Start() {
			foreach (GFunction f in functions) {
				if (f is ITimeControl) {
					(f as ITimeControl).Start();
				}
			}
		}

		public void Pause() {
			foreach (GFunction f in functions) {
				if (f is ITimeControl) {
					(f as ITimeControl).Pause();
				}
			}
		}

		public void Stop() {
			foreach (GFunction f in functions) {
				if (f is ITimeControl) {
					(f as ITimeControl).Stop();
				}
			}
		}

		public void Reverse() {
			foreach (GFunction f in functions) {
				if (f is ITimeControl) {
					(f as ITimeControl).Reverse();
				}
			}
		}

		#endregion
	}
}
