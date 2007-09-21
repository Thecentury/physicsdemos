using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Thecentury;
using System.Globalization;
using Thecentury.GeometricObject;
using Thecentury.MyGraphics;

namespace Thecentury.ImprovedTrackBar {

	public enum Marker {
		Triangle,
		DoubleTriangle,
		Rectangle,
		Ellipse
	}

	public enum ElementsLocation {
		Upper,
		Lower,
		Both
	}

	public enum StrokeStyle {
		Line,
		Triangle,
	}

	public enum HoleStyle {
		Stroke,
		Rectangle
	}

	public delegate void ValueChangedHandler();
	// TODO Правильное (не дергающееся) отображение численного значения
	/// <summary>
	/// 
	/// </summary>
	public sealed partial class ImprovedTrackBar : UserControl {
		public void Repaint() {
			OnPaint(null);
		}

		public event ValueChangedHandler ValueChanged;
		public void OnValueChanged() {
			if (ValueChanged != null) {
				ValueChanged();
			}
		}

		Rectangle rTrackBar; // whole trackbar
		Rectangle rNumericValue;
		Rectangle rAxis; // only axis
		NumberFormatInfo info = new CultureInfo("en-US", false).NumberFormat;

		bool wereChanging = false;
		bool processClicks = true;

		bool ctrlPressed = false;
		bool shiftPressed = false;

		float highSpeedModifier = 5;
		float lowSpeedModifier = 0.3f;

		float keyboardSpeedRatio = 0.05f;

		Size currentValueSize = new Size(10, 10);
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Size CurrentValueSize {
			get { return currentValueSize; }
			set { currentValueSize = value; }
		}

		Color cAxis = Color.Black;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color CAxis {
			get { return cAxis; }
			set { cAxis = value; Repaint(); }
		}

		Color cValues = Color.Blue;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color CValues {
			get { return cValues; }
			set { cValues = value; Repaint(); }
		}

		Color cStrokes = Color.CadetBlue;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color CStrokes {
			get { return cStrokes; }
			set { cStrokes = value; Repaint(); }
		}

		Color cHoles = Color.Red;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color CHoles {
			get { return cHoles; }
			set { cHoles = value; Repaint(); }
		}

		Color cNumericValue = Color.Black;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color CNumericValue {
			get { return cNumericValue; }
			set { cNumericValue = value; Repaint(); }
		}

		Color cCurrentValue = Color.OrangeRed;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color CCurentValue {
			get { return cCurrentValue; }
			set { cCurrentValue = value; }
		}


		// TODO Properties!
		float wAxis = 2;
		float wValues = 1;
		float hStrokes = 10;
		float wStrokes = 2;
		float wHoles = 1;
		float hHoleStroke = 10;

		string fontName = "Arial";
		float axisFontSize = 10;
		float numericValueFontSize = 12;

		float wheelSpeedRatio = 0.01f;

		Marker markerStyle = Marker.Ellipse;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Marker MarkerStyle {
			get { return markerStyle; }
			set { markerStyle = value; }
		}

		// TODO Использовать!
		StrokeStyle strokeStyle = StrokeStyle.Line;
		// TODO Использовать!
		HoleStyle holeStyle = HoleStyle.Stroke;
		// TODO Использовать!
		ElementsLocation strokesLocation = ElementsLocation.Lower;
		// TODO Использовать!
		ElementsLocation valuesLocation = ElementsLocation.Upper;

		private Tracker tracker = new Tracker();
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Tracker Tracker {
			get { return tracker; }
		}

		AutoDisposablePen.AutoDisposablePen pen = new AutoDisposablePen.AutoDisposablePen();

		float widthRatio = 0.8f;

		void ComputeSize() {
			int width = (int)(widthRatio * this.Width);
			rTrackBar = new Rectangle(new Point(0, 0), new Size(width, this.Height));

			rNumericValue = new Rectangle(new Point(0 + width, 0), new Size(this.Width - width, this.Height));

			rAxis = new Rectangle((int)(rTrackBar.X + rTrackBar.Width * 0.5f * (1 - widthRatio)), this.Height / 2,
				(int)(rTrackBar.Width * widthRatio), 0);
		}


		BufferedGraphicsContext context;
		BufferedGraphics grafx;

		public ImprovedTrackBar() {
			InitializeComponent();
			ComputeSize();
			Initialize();
		}

		private void Initialize() {
			this.MouseWheel += new MouseEventHandler(ImprovedTrackBar_MouseWheel);
			context = BufferedGraphicsManager.Current;
			context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
			grafx = context.Allocate(this.CreateGraphics(), Bounds);
		}

		void GeneralPaint(Graphics g) {
			using (new RegionSaver(g, rTrackBar)) {
				DrawAxis(g);
				DrawStrokes(g);
				DrawValues(g);
				DrawHoles(g);
				DrawCurrentValue(g);
			}
			using (new RegionSaver(g, rNumericValue)) {
				// TODO Добавить возможность писать комментарий к значению
				DrawNumericValue(g);
			}
		}

		private void DrawCurrentValue(Graphics g) {
			RectangleF from = new RectangleF(tracker.Minimum, 0, tracker.Length, 0);
			float x = Screen.TransformX(CurrentValue, from, rAxis);

			switch (markerStyle) {
				case Marker.Triangle:
					MyGraphics.MyGraphics.DrawFilledPath(g, cCurrentValue,
						new PointF(x - currentValueSize.Width / 2, rAxis.Y + rAxis.Height / 2 - currentValueSize.Height),
						new PointF(x + currentValueSize.Width / 2, rAxis.Y + rAxis.Height / 2 - currentValueSize.Height),
						new PointF(x, rAxis.Y + rAxis.Height / 2 - 2)
						);
					break;
				case Marker.Rectangle:
					using (Brush brush = new SolidBrush(cCurrentValue)) {
						g.FillRectangle(brush, new Rectangle((int)(x - currentValueSize.Width / 2), rAxis.Y + rAxis.Height / 2 - currentValueSize.Height / 2, currentValueSize.Width, currentValueSize.Height));
					}
					break;
				case Marker.DoubleTriangle:
					MyGraphics.MyGraphics.DrawFilledPath(g, cCurrentValue,
						new PointF(x - currentValueSize.Width / 2, rAxis.Y + rAxis.Height / 2 - currentValueSize.Height),
						new PointF(x + currentValueSize.Width / 2, rAxis.Y + rAxis.Height / 2 - currentValueSize.Height),
						new PointF(x - currentValueSize.Width / 2, rAxis.Y + rAxis.Height / 2 + currentValueSize.Height),
						new PointF(x + currentValueSize.Width / 2, rAxis.Y + rAxis.Height / 2 + currentValueSize.Height)
					);
					break;
				case Marker.Ellipse:
					using (Brush brush = new SolidBrush(cCurrentValue)) {
						g.FillEllipse(brush, new Rectangle((int)(x - currentValueSize.Width / 2), rAxis.Y + rAxis.Height / 2 - currentValueSize.Height / 2, currentValueSize.Width, currentValueSize.Height));
					}
					break;
				default:
					break;
			}
		}

		private void DrawStrokes(Graphics g) {
			RectangleF from = new RectangleF(tracker.Minimum, 0, tracker.Length, 0);
			float x;
			using (pen.Pen = new Pen(cStrokes, wStrokes)) {
				foreach (float f in tracker.Strokes) {
					x = Thecentury.Screen.TransformX(f, from, rAxis);
					g.DrawLine(pen, x, rAxis.Y - hStrokes / 2, x, rAxis.Y + hStrokes / 2);
				}
			}
		}

		private void DrawNumericValue(Graphics g) {
			string st = MyString.ConvertToString(CurrentValue, info);
			string st_max = MyString.ConvertToString(tracker.Maximum, info);
			Font font = new Font(fontName, numericValueFontSize);

			SizeF size = g.MeasureString(st_max, font);
			Brush brush = new SolidBrush(cNumericValue);

			PointF location = new PointF(rNumericValue.X + rNumericValue.Width / 2 - size.Width / 2,
				rNumericValue.Y + rNumericValue.Height / 2 - size.Height / 2);

			g.DrawString(st, font, brush, location);

			font.Dispose();
			brush.Dispose();
		}

		private void DrawHoles(Graphics g) {
			RectangleF from = new RectangleF(tracker.Minimum, 0, tracker.Length, 0);
			float x;
			using (pen.Pen = new Pen(cHoles, wHoles)) {
				foreach (float f in tracker.Holes) {
					x = Thecentury.Screen.TransformX(f, from, rAxis);
					g.DrawLine(pen, x, rAxis.Y - hHoleStroke / 2, x, rAxis.Y + hHoleStroke / 2);
				}
			}
		}

		private void DrawValues(Graphics g) {
			RectangleF from = new RectangleF(tracker.Minimum, 0, tracker.Length, 0);
			float x;
			string st;
			SizeF size;
			Font font = new Font("Arial", axisFontSize);
			Brush brush = new SolidBrush(cValues);

			using (pen.Pen = new Pen(cValues, wValues)) {
				foreach (float f in tracker.Numbers) {
					x = Thecentury.Screen.TransformX(f, from, rAxis);
					st = MyString.ConvertToString(f, info);
					size = g.MeasureString(st, font);
					g.DrawString(st, font, brush, x - size.Width / 2, rAxis.Y + hStrokes / 2);
				}
			}

			font.Dispose();
			brush.Dispose();
		}

		private void DrawAxis(Graphics g) {
			using (pen.Pen = new Pen(cAxis, wAxis)) {
				g.DrawLine(pen, new Point((int)(rTrackBar.X + rTrackBar.Width * 0.5f * (1 - widthRatio)), rAxis.Y),
					new Point((int)(rTrackBar.X + rTrackBar.Width * 0.5f * (1 + widthRatio)), rAxis.Y));

				// @TEMP
				g.DrawLine(pen, new Point(rTrackBar.Right, rTrackBar.Top), new Point(rTrackBar.Right, rTrackBar.Bottom));

			}
			//throw new Exception("The method or operation is not implemented.");
		}


		private void ImprovedTrackBar_Resize(object sender, EventArgs e) {
			context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
			if (grafx != null) {
				grafx.Dispose();
				grafx = null;
			}
			grafx = context.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));
			ComputeSize();
			this.Invalidate();
		}

		private void Clear(Graphics g) {
			g.Clear(Color.White);
		}

		protected override void OnPaintBackground(PaintEventArgs e) { }

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			Clear(grafx.Graphics);
			GeneralPaint(grafx.Graphics);

			grafx.Render(this.CreateGraphics());
		}

		private void ImprovedTrackBar_MouseClick(object sender, MouseEventArgs e) {
			if (MyRectangle.IsPointFInRectangleF(e.Location, rTrackBar)) {
				if (processClicks) {
					SetValueFromCoordinate(e.Location);
				}
			}
		}

		private float SpeedModifier() {
			if (shiftPressed) {
				return highSpeedModifier;
			}
			else if (ctrlPressed) {
				return lowSpeedModifier;
			}
			else return 1;
		}

		private void ImprovedTrackBar_MouseWheel(object sender, MouseEventArgs e) {
			if (e.Delta != 0) {
				if (MyRectangle.IsPointFInRectangleF(e.Location, rTrackBar)) {
					CurrentValue += (e.Delta > 0 ? wheelSpeedRatio : -wheelSpeedRatio) * tracker.Length * SpeedModifier();
				}
			}
		}

		private void ImprovedTrackBar_MouseDown(object sender, MouseEventArgs e) {
			processClicks = false;
			if (MyRectangle.IsPointFInRectangleF(e.Location, rTrackBar)) {
				wereChanging = true;
				SetValueFromCoordinate(e.Location);
			}
		}

		private void ImprovedTrackBar_MouseMove(object sender, MouseEventArgs e) {
			if (wereChanging) {
				SetValueFromCoordinate(e.Location);
			}
			if (MyRectangle.IsPointFInRectangleF(e.Location, rTrackBar)) {
				Invalidate();
			}
		}

		private void ImprovedTrackBar_MouseUp(object sender, MouseEventArgs e) {
			wereChanging = false;
			processClicks = true;
		}

		private void SetValueFromCoordinate(Point p) {
			RectangleF to = new RectangleF(tracker.Minimum, 0, tracker.Length, 0);
			float nextVal = Screen.TransformX(p.X, rAxis, to);
			CurrentValue = nextVal;
		}

		private float CalculateValueFromCoordinate(Point p) {
			RectangleF to = new RectangleF(tracker.Minimum, 0, tracker.Length, 0);
			return Screen.TransformX(p.X, rAxis, to);
		}

		private void SetValueFromCoordinate(float x) {
			RectangleF to = new RectangleF(tracker.Minimum, 0, tracker.Length, 0);
			float nextVal = Screen.TransformX(x, rAxis, to);
			CurrentValue = nextVal;
		}

		private float CalculateValueFromCoordinate(float x) {
			RectangleF to = new RectangleF(tracker.Minimum, 0, tracker.Length, 0);
			return Screen.TransformX(x, rAxis, to);
		}

		private void ImprovedTrackBar_MouseEnter(object sender, EventArgs e) {
			this.ParentForm.ActiveControl = this;
			this.Focus();
		}

		private void ImprovedTrackBar_MouseLeave(object sender, EventArgs e) { }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float CurrentValue {
			get { return tracker.CurrentValue; }
			set {
				float prev = tracker.CurrentValue;
				tracker.CurrentValue = value;
				if (prev != tracker.CurrentValue) {
					Invalidate();
					OnValueChanged();
				}
			}
		}

		private void ImprovedTrackBar_KeyDown(object sender, KeyEventArgs e) {
			if (e.Shift) {
				shiftPressed = true;
			}
			else if (e.Control) {
				ctrlPressed = true;
			}

			Point temp = Location;

			if (e.KeyCode == Keys.Left) {
				if (MyPoint.NewZero(Cursor.Position, temp).X <= rTrackBar.Right) {
					CurrentValue -= tracker.Length * keyboardSpeedRatio * SpeedModifier();
				}
			}
			else if (e.KeyCode == Keys.Right) {
				if (MyPoint.NewZero(Cursor.Position, temp).X <= rTrackBar.Right) {
					CurrentValue += tracker.Length * keyboardSpeedRatio * SpeedModifier();
				}
			}
			else if (e.KeyCode == Keys.Home) {
				if (MyPoint.NewZero(Cursor.Position, temp).X <= rTrackBar.Right) {
					CurrentValue = tracker.Minimum;
				}
			}
			else if (e.KeyCode == Keys.End) {
				if (MyPoint.NewZero(Cursor.Position, temp).X <= rTrackBar.Right) {
					CurrentValue = tracker.Maximum;
				}
			}

		}

		protected override bool IsInputKey(Keys keyData) {
			return true;
		}

		private void ImprovedTrackBar_KeyUp(object sender, KeyEventArgs e) {
			shiftPressed = false;
			ctrlPressed = false;
		}
	}
}