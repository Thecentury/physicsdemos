#define TRACE
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Thecentury.Functions;
using Thecentury.Animations;
using System.Diagnostics;

namespace Thecentury {

	// todo режим перемещени€ по нажатию колеса мыши
	// todo доделать режим плавной прокрутки (как контакты в iPhone))

	public sealed partial class GrapherControl : UserControl {
		public static bool usescr = true;

		bool controlMove = false;
		bool rectangleZooming = false;
		bool middleButtonMove = false;
		Rectangle zoomRect;

		Point prevPos;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AllowedDirections AllowResize = AllowedDirections.All;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AllowedDirections AllowDrag = AllowedDirections.All;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Source ZoomSource = Source.Both;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Speed AllowedZoomSpeed = Speed.SlowNormalFast;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Center ZoomCenter = Center.MouseLocation;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Source DragSource = Source.Both;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Speed AllowedDragSpeed = Speed.SlowNormalFast;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AllowedDirections AllowWheelScroll = AllowedDirections.All;

		private bool allowZoomAnimation = true;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AllowZoomAnimation {
			get { return allowZoomAnimation; }
			set { allowZoomAnimation = value; }
		}


		private bool allowRectangleZooming = true;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AllowRectangleZooming {
			get { return allowRectangleZooming; }
			set { allowRectangleZooming = true; }
		}

		private bool useBlackNWhiteEffectForRectangleZooming = true;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool UseBlackNWhiteEffectForRectangleZooming {
			get { return useBlackNWhiteEffectForRectangleZooming; }
			set { useBlackNWhiteEffectForRectangleZooming = value; }
		}


		private bool allowMiddleButtonMove = false;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AllowMiddleButtonMove {
			get { return allowMiddleButtonMove; }
			set { allowMiddleButtonMove = value; }
		}

		private float middleButtonMoveModifier = 0.09f;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float MiddleButtonMoveModifier {
			get { return middleButtonMoveModifier; }
			set { middleButtonMoveModifier = value; }
		}

		private int middleButtomMovePointSize = 5;
		private Point middleButtonClick;

		Bitmap screen;
		BufferedGraphicsContext context;
		BufferedGraphics g;

		float zIn;
		float fzIn;
		float szIn;

		private float zoomSpeed = 1.3f;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float ZoomSpeed {
			get { return zoomSpeed; }
			set {
				zoomSpeed = value;
				zIn = 1 / zoomSpeed;
			}
		}

		private float fastZoomSpeed = 2;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float FastZoomSpeed {
			get { return fastZoomSpeed; }
			set { fastZoomSpeed = value; fzIn = 1 / fastZoomSpeed; }
		}

		private float slowZoomSpeed = 1.1f;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float SlowZoomSpeed {
			get { return slowZoomSpeed; }
			set { slowZoomSpeed = value; szIn = 1 / slowZoomSpeed; }
		}

		private float dragSpeed = 1;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float DragSpeed {
			get { return dragSpeed; }
			set { dragSpeed = value; }
		}

		private float fastDragSpeed = 6;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float FastDragSpeed {
			get { return fastDragSpeed; }
			set { fastDragSpeed = value; }
		}

		private float slowDragSpeed = 0.3f;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float SlowDragSpeed {
			get { return slowDragSpeed; }
			set { slowDragSpeed = value; }
		}

		private float kDragSpeed = 3;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float KeyboardDragSpeed {
			get { return kDragSpeed; }
			set { kDragSpeed = value; }
		}

		private float wDragSpeed = 10;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float WheelDragSpeed {
			get { return kDragSpeed; }
			set { kDragSpeed = value; }
		}

		RectangleF from = new RectangleF(-10.0f, -10.0f, 20.0f, 20.0f);
		RectangleF prevRect;

		private FunctionGrapher grapher;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FunctionGrapher Grapher {
			get { return grapher; }
		}

		private void repaintIsNeededHandler(object sender, EventArgs e) {
			this.Invalidate();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RectangleF From {
			get { return from; }
			set {
				grapher.From = value;
				this.from = value;
				grapher.AxisDivision.From = value;
			}
		}

		public GrapherControl() {
			InitializeComponent();

			//Debug.Listeners.Add(new TextWriterTraceListener("log.txt"));
			//Debug.AutoFlush = true;

			if (usescr) {
				SetStyle(ControlStyles.UserPaint, true);
				SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				SetStyle(ControlStyles.DoubleBuffer, true);
			}

			this.MouseWheel += new MouseEventHandler(GrapherControl_MouseWheel);

			screen = new Bitmap(this.Width, this.Height);

			context = BufferedGraphicsManager.Current;
			context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
			g = context.Allocate(this.CreateGraphics(), ClientRectangle);

			grapher = new FunctionGrapher();
			grapher.AxisDivision = new FloatingAxisDivision(0.0f, 2.0f, 0.0f, 2.0f);
			grapher.From = from;
			grapher.RepaintNeeded += new EventHandler(repaintIsNeededHandler);

			zIn = 1.0f / zoomSpeed;
			fzIn = 1.0f / fastZoomSpeed;
			szIn = 1.0f / slowZoomSpeed;

			Application.ApplicationExit += new EventHandler(CleanUp);
		}

		void GrapherControl_MouseWheel(object sender, MouseEventArgs e) {
			if (e.Delta != 0) {
				if (MyRectangle.IsPointFInRectangleF(e.Location, grapher.InnerTo) || (AllowWheelScroll == AllowedDirections.None)) {
					if (ZoomSource == (ZoomSource | Source.Mouse)) {
						if (e.Delta > 0) {
							if (ZoomCenter == Center.MouseLocation) {
								Zoom(true, e.Location);
							}
							else {
								Zoom(true);
							}
						}
						else {// e.Delta < 0
							if (ZoomCenter == Center.MouseLocation) {
								Zoom(false, e.Location);
							}
							else {
								Zoom(false);
							}
						}
					}
				}
				else if (AllowWheelScroll != AllowedDirections.None) {
					float coeff = DragCoeff(CurrentDragSpeed);
					if ((AllowWheelScroll == AllowedDirections.X) || (AllowWheelScroll == AllowedDirections.All)) {
						if (MyRectangle.IsPointFInRectangleF(e.Location, new Rectangle(grapher.InnerTo.Left, grapher.InnerTo.Bottom, grapher.InnerTo.Width, this.Bounds.Height - grapher.InnerTo.Bottom))) {
							from = MyRectangle.rectMove(from, MyRectangle.xRatio(from, ClientRectangle) * wDragSpeed * coeff * (e.Delta > 0 ? 1 : -1), 0);
							this.grapher.From = from;
							this.Invalidate();
						}
					}
					if ((AllowWheelScroll == AllowedDirections.Y) || (AllowWheelScroll == AllowedDirections.All)) {
						if (MyRectangle.IsPointFInRectangleF(e.Location, new Rectangle(0, grapher.InnerTo.Top, grapher.InnerTo.Left, grapher.InnerTo.Height))) {
							from = MyRectangle.rectMove(from, 0, MyRectangle.yRatio(from, ClientRectangle) * wDragSpeed * coeff * (e.Delta > 0 ? 1 : -1));
							this.grapher.From = from;
							this.Invalidate();
						}
					}
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			GeneralPaint(e);
		}

		protected override void OnPaintBackground(PaintEventArgs e) { }

		private bool flatMouseDrag = false;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool FlatMouseDrag {
			get { return flatMouseDrag; }
			set { flatMouseDrag = value; }
		}

		private Thecentury.Utils.PerfCounter flatDragCounter = new Thecentury.Utils.PerfCounter();

		private void GrapherControl_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				if (AllowDrag != AllowedDirections.None) {
					if (flatMouseDrag) {
						flatDragCounter.Start();
					}
					controlMove = true;
					prevPos = e.Location;
					prevRect = grapher.From;
					this.Cursor = Cursors.Hand;
				}
			}
			else if (e.Button == MouseButtons.Right) {
				if (allowRectangleZooming && MyRectangle.IsPointFInRectangleF(e.Location, grapher.InnerTo)) {
					rectangleZooming = true;
					this.Cursor = System.Windows.Forms.Cursors.SizeAll;
					prevPos = e.Location;
					zoomRect = MyRectangle.CreateFromDiagonalPoints(prevPos, prevPos);
				}
			}
			else if (e.Button == MouseButtons.Middle) {
				if (allowMiddleButtonMove) {
					if (!middleButtonMove) {
						middleButtonClick = e.Location;
						this.Cursor = System.Windows.Forms.Cursors.NoMove2D;
						prevRect = grapher.From;
						prevPos = e.Location;
						middleButtonMove = true;
						Application.Idle += new EventHandler(Application_Idle);
					}
					else {
						middleButtonMove = false;
					}
				}
			}
		}

		// todo добитьс€ того, чтобы это работало
		void Application_Idle(object sender, EventArgs e) {
			Point p = Cursor.Position;
			p.X -= Location.X;
			p.Y -= Location.Y;

			float coeff = middleButtonMoveModifier * DragCoeff(CurrentDragSpeed);
			bool move = AllowDrag != AllowedDirections.None;
			int tempx = (int)(coeff * (p.X - prevPos.X));
			int tempy = (int)(coeff * (p.Y - prevPos.Y));
			if (AllowDrag == AllowedDirections.X) {
				tempy = 0;
			}
			else if (AllowDrag == AllowedDirections.Y) {
				tempx = 0;
			}
			if (Math.Abs(p.X - prevPos.X) <= 2) {
				tempx = 0;
			}
			if (Math.Abs(p.Y - prevPos.Y) <= 2) {
				tempy = 0;
			}
			if (move && (tempx != 0 || tempy != 0)) {
				RectangleF r = grapher.From;
				this.Cursor = CreateCursor(prevPos, p);
				from = MyRectangle.rectMove(prevRect, MyRectangle.xRatio(prevRect, ClientRectangle) * (-tempx), MyRectangle.yRatio(prevRect, ClientRectangle) * (tempy));
				prevRect = r;
				this.grapher.From = from;
				this.Invalidate();
			}
		}

		private float ZoomCoeff(bool closer, speed s) {
			if (closer) {
				switch (s) {
					case speed.slow:
						return szIn;
					case speed.normal:
						return zIn;
					case speed.fast:
						return fzIn;
					default:
						return 0;
				}
			}
			else {
				switch (s) {
					case speed.slow:
						return slowZoomSpeed;
					case speed.normal:
						return zoomSpeed;
					case speed.fast:
						return fastZoomSpeed;
					default:
						return 0;
				}
			}
		}

		private speed CurrentZoomSpeed {
			get {
				if (AllowedZoomSpeed == Speed.SlowNormalFast) {
					if (shiftPressed) {
						return speed.fast;
					}
					else if (ctrlPressed) {
						return speed.slow;
					}
					else {
						return speed.normal;
					}
				}
				else {
					return speed.normal;
				}
			}
		}

		private float DragCoeff(speed s) {
			switch (s) {
				case speed.slow:
					return slowDragSpeed;
				case speed.normal:
					return dragSpeed;
				case speed.fast:
					return fastDragSpeed;
				default:
					return 0;
			}
		}
		private speed CurrentDragSpeed {
			get {
				if (AllowedDragSpeed == Speed.SlowNormalFast) {
					if (shiftPressed) {
						return speed.fast;
					}
					else if (ctrlPressed) {
						return speed.slow;
					}
					else {
						return speed.normal;
					}
				}
				else {
					return speed.normal;
				}
			}
		}

		private Animation zoomAnimation;

		public void Zoom(bool closer) {
			float zoomcoeff = ZoomCoeff(closer, CurrentZoomSpeed);
			from = MyRectangle.RectZoom(from, MyRectangle.rectCenter(from), zoomcoeff);

			if (allowZoomAnimation) {
				if (zoomAnimation != null) {
					zoomAnimation.Dispose();
				}
				zoomAnimation = new Animation(grapher,
					"From",
					//Interpolators.Linear,
					Interpolators.SlowEndsFastMiddle_Pow4,
					0,
					//Thecentury.MyRectangle.SquareRatioGreaterThan1(grapher.From, from),
					Math.Min(Thecentury.MyRectangle.SquareRatioLessThan1(grapher.From, from), 1f),
					false,
					new object[] { grapher.From, from });
				zoomAnimation.AutoAnimate();
				zoomAnimation.Occured += delegate() { this.Invalidate(); };
			}
			else {
				From = from;
				//grapher.From = from;
			}
		}

		public void Zoom(bool closer, float zoomOutSpeed) {
			if (closer) {
				zoomOutSpeed = 1 / zoomOutSpeed;
			}
			from = MyRectangle.RectZoom(from, MyRectangle.rectCenter(from), zoomOutSpeed);
			grapher.From = from;
			this.Invalidate();
		}

		public void Zoom(bool closer, Point p) {
			float zoomcoeff = ZoomCoeff(closer, CurrentZoomSpeed);
			PointF pf = Thecentury.Screen.TransformPointF(p, this.DisplayRectangle, this.from);
			from = MyRectangle.RectZoom(from, pf, zoomcoeff);
			grapher.From = from;
			this.Invalidate();
		}

		public void ZoomX(bool closer) {
			float zoomcoeff = ZoomCoeff(closer, CurrentZoomSpeed);
			from = MyRectangle.RectZoom(from, MyRectangle.rectCenter(from), zoomcoeff, 1.0f);
			grapher.From = from;
			this.Invalidate();
		}

		public void ZoomY(bool closer) {
			float zoomcoeff = ZoomCoeff(closer, CurrentZoomSpeed);
			from = MyRectangle.RectZoom(from, MyRectangle.rectCenter(from), 1.0f, zoomcoeff);
			grapher.From = from;
			this.Invalidate();
		}

		Animation flatMoveAnimation;

		private void GrapherControl_MouseMove(object sender, MouseEventArgs e) {
			if (controlMove) {
				float coeff = DragCoeff(CurrentDragSpeed);
				bool move = AllowDrag != AllowedDirections.None;
				if (move) {

					float tempx = coeff * (e.X - prevPos.X);
					float tempy = coeff * (e.Y - prevPos.Y);

					if (AllowDrag == AllowedDirections.X) {
						tempy = 0;
					}
					else if (AllowDrag == AllowedDirections.Y) {
						tempx = 0;
					}

					Cursor = Cursors.Hand;
					from = MyRectangle.rectMove(prevRect, MyRectangle.xRatio(prevRect, ClientRectangle) * (-tempx), MyRectangle.yRatio(prevRect, ClientRectangle) * (tempy));

					if (flatMouseDrag) {
						float dt = flatDragCounter.TimeF;
						SizeF s = new SizeF(-tempx, tempy);
						s.Width *= from.Width / ClientSize.Width;
						s.Height *= from.Height / ClientSize.Height;

						if (flatMoveAnimation != null) {
							flatMoveAnimation.Dispose();
						}

						flatMoveAnimation = new Animation(
							grapher,
							"From",
							Interpolators.Root2,
							0,
							dt,
							false,
							new object[] { from, new RectangleF(from.X + s.Width, from.Y + s.Height, from.Width, from.Height) });
						flatMoveAnimation.AutoAnimate();
						flatMoveAnimation.Occured += delegate() { Invalidate(); };
					}
					else {
						grapher.From = from;
						Invalidate();
					}
				}
			}
			else if (rectangleZooming) {
				zoomRect = MyRectangle.CreateFromDiagonalPoints(prevPos, MyRectangle.ClipToRectangle(e.Location, grapher.InnerTo));
				this.Invalidate();
			}
		}

		private Cursor CreateCursor(Point c, Point n) {
			int dx = n.X - c.X;
			int dy = n.Y - c.Y;
			double a = Math.Atan2(dy, dx);
			a *= 180 / Math.PI;
			if (0 <= a && a <= 22.5 || (360 - 22.5) <= a && a <= 360) {
				return Cursors.PanEast;
			}
			else if (22.5 <= a && a <= (90 - 22.5)) {
				return Cursors.PanNE;
			}
			else if ((90 - 22.5) <= a && a <= (90 + 22.5)) {
				return Cursors.PanNorth;
			}
			else if ((90 + 22.5) <= a && a <= (180 - 22.5)) {
				return Cursors.PanNW;
			}
			else if ((180 - 22.5) <= a && a <= (180 + 22.5)) {
				return Cursors.PanWest;
			}
			else if ((180 + 22.5) <= a && a <= (270 - 22.5)) {
				return Cursors.PanSW;
			}
			else if ((270 - 22.5) <= a && a <= (270 + 22.5)) {
				return Cursors.PanSouth;
			}
			else if ((270 + 22.5) <= a && a <= (360 - 22.5)) {
				return Cursors.PanSE;
			}
			else
				return Cursors.Cross;
		}

		private void GrapherControl_MouseUp(object sender, MouseEventArgs e) {
			if (rectangleZooming) {
				rectangleZooming = false;
				if (zoomRect.Width != 0 && zoomRect.Height != 0) { // не пустой пр€моугольник
					this.from = Thecentury.Screen.TransformRectF(zoomRect, this.grapher.InnerTo, from);
					this.from.Y -= from.Height;
					grapher.From = from;
					this.Invalidate();
				}
			}
			if (!middleButtonMove) {
				this.Cursor = Cursors.Default;
				Application.Idle -= new EventHandler(Application_Idle);
			}
			if (flatMouseDrag) {
				float dt = flatDragCounter.TimeF;
				SizeF s = new SizeF(prevPos.X - e.X, prevPos.Y - e.Y);
				s.Width *= from.Width / ClientSize.Width;
				s.Height *= -from.Height / ClientSize.Height;

				if (flatMoveAnimation != null) {
					flatMoveAnimation.Dispose();
				}

				flatMoveAnimation = new Animation(
					grapher,
					"From",
					Interpolators.Root3,
					0,
					dt,
					false,
					new object[] { grapher.From, new RectangleF(from.X + s.Width, from.Y + s.Height, from.Width, from.Height) });
				flatMoveAnimation.AutoAnimate();
				flatMoveAnimation.Occured += delegate() { Invalidate(); };
			}
			controlMove = false;
		}

		private void GrapherControl_Resize(object sender, EventArgs e) {
			context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
			if (g != null) {
				g.Dispose();
				g = null;
			}
			g = context.Allocate(this.CreateGraphics(), ClientRectangle);

			if (screen != null) {
				screen.Dispose();
				screen = null;
			}
			screen = new Bitmap(this.Width, this.Height);

			this.Invalidate();
		}

		private void GeneralPaint(PaintEventArgs e) {
			Thecentury.Utils.PerfCounter c = new Thecentury.Utils.PerfCounter();
			//Thecentury.Utils.PerfCounter c2 = new Thecentury.Utils.PerfCounter();
			c.Start();

			if (usescr) {
				using (Graphics _g = Graphics.FromImage(screen)) {
					grapher.Paint(_g, ClientRectangle);
					if (rectangleZooming) {
						ZoomRectPaint(_g);

						if (useBlackNWhiteEffectForRectangleZooming) {
							Thecentury.MyBitmap.RectangleAnimatedBitmap animBmp = new Thecentury.MyBitmap.RectangleAnimatedBitmap();
							animBmp.Bitmap = screen;
							animBmp.Ptr = new Thecentury.MyBitmap.Ptrs.RectangleBitmapPtr(screen, grapher.InnerTo, zoomRect);
							animBmp.Formula = Thecentury.MyBitmap.BitmapTransformations.GrayScale;
							animBmp.ApplyAnimation();
						}
					}
					if (middleButtonMove) {
						MiddleButtonMovePaint(_g);
					}
					//e.Graphics.DrawImageUnscaled(screen, 0, 0);

					//					using (new Thecentury.Utils.PerfCounter("¬ывод на экран scaled   изображени€: ")) {
					//c2.Start();
					//e.Graphics.DrawImage(screen, 0, 0);
					//int t = c2.Time_nSecs;
					//c2.Start();
					//					}
					//using (new Thecentury.Utils.PerfCounter("¬ывод на экран unscaled изображени€: ")) {
					//    //e.Graphics.DrawImage(screen, 0, 0);


					//screen = 
					e.Graphics.DrawImageUnscaled(screen, 0, 0);
					//int t2 = c2.Time_nSecs;
					//Debug.WriteLine(string.Format("Unscaled: {0}\nScaled:   {1}", t2, t));
					//}
				}
			}
			else {
				grapher.Paint(g.Graphics, ClientRectangle);
				if (rectangleZooming) {
					ZoomRectPaint(g.Graphics);
				}
				if (middleButtonMove) {
					MiddleButtonMovePaint(g.Graphics);
				}
				g.Render(e.Graphics);
			}
			Debug.WriteLine(c.Time_mSecs.ToString());

			//Graphics.FromImage
			//grapher.Paint(grafx.Graphics, this.DisplayRectangle);

			//this.CreateGraphics().DrawImage(screen, 0, 0);
			//this.CreateGraphics().DrawImageUnscaled(screen, 0, 0);
			//e.Graphics.DrawImageUnscaled(screen, 0, 0);
		}

		private void MiddleButtonMovePaint(Graphics graphics) {
			using (Brush brush = new SolidBrush(Color.Black)) {
				graphics.FillEllipse(brush, new Rectangle(middleButtonClick.X - middleButtomMovePointSize, middleButtonClick.Y - middleButtomMovePointSize, 2 * middleButtomMovePointSize, 2 * middleButtomMovePointSize));
			}
		}

		private void ZoomRectPaint(Graphics graphics) {
			using (Pen pen = new Pen(Color.Black, 1)) {
				graphics.DrawRectangle(pen, zoomRect);
			}
		}

		private void GrapherControl_KeyDown(object sender, KeyEventArgs e) {
			if (e.Shift) {
				shiftPressed = true;
			}
			else if (e.Control) {
				ctrlPressed = true;
			}

			if (ZoomSource == Source.Keyboard || ZoomSource == Source.Both) {
				if (e.KeyCode == Keys.OemMinus) {
					Zoom(false);
				}
				else if (e.KeyCode == Keys.Oemplus) {
					Zoom(true);
				}
			}

			if (e.KeyCode == Keys.MediaPlayPause) {
				if ((grapher.State == State.Different) || (grapher.State == State.Stopped)) {
					this.grapher.Start();
				}
				else {
					this.grapher.Pause();
				}
			}
			else if (e.KeyCode == Keys.MediaStop) {
				this.grapher.Stop();
			}
			else if (e.KeyCode == Keys.Space) {
				this.grapher.Reverse();
			}


			if (DragSource == (DragSource | Source.Keyboard)) {
				float coeff = DragCoeff(CurrentDragSpeed);
				bool changed = false;
				if (e.KeyCode == Keys.Left) {
					from = MyRectangle.rectMove(from, MyRectangle.xRatio(from, ClientRectangle) * kDragSpeed * coeff, 0);
					changed = true;
				}
				else if (e.KeyCode == Keys.Right) {
					from = MyRectangle.rectMove(from, MyRectangle.xRatio(from, ClientRectangle) * (-kDragSpeed) * coeff, 0);
					changed = true;
				}
				else if (e.KeyCode == Keys.Up) {
					from = MyRectangle.rectMove(from, 0, MyRectangle.yRatio(from, ClientRectangle) * (-kDragSpeed) * coeff);
					changed = true;
				}
				else if (e.KeyCode == Keys.Down) {
					from = MyRectangle.rectMove(from, 0, MyRectangle.yRatio(from, ClientRectangle) * kDragSpeed * coeff);
					changed = true;
				}
				if (changed) {
					this.grapher.From = from;
					this.Invalidate();
					//Animation a = new Animation(
					//    Grapher,
					//    "From",
					//    Interpolators.Linear,
					//    0, kDragSpeed * 1 / Thecentury.MyRectangle.MaxShift(grapher.From, from),
					//    false,
					//    new object[] { grapher.From, from });
					//a.AutoAnimate();
					//a.Occured += delegate() { this.Invalidate(); };
				}
			}
		}

		protected override bool IsInputKey(Keys keyData) {
			return true;
		}

		private bool ctrlPressed = false;
		private bool shiftPressed = false;

		private void GrapherControl_KeyUp(object sender, KeyEventArgs e) {
			shiftPressed = false;
			ctrlPressed = false;
		}

		private void CleanUp(object sender, EventArgs e) {
			if (screen != null) {
				screen.Dispose();
				screen = null;
			}

			if (g != null) {
				g.Dispose();
				g = null;
			}

			if (context != null) {
				try {
					context.Dispose();
					context = null;
				}
				catch (Exception) { }
			}
		}
	}

	internal enum speed {
		slow,
		normal,
		fast
	}

	[Flags]
	public enum AllowedDirections {
		X = 2,
		Y = 4,
		All = 6,
		None = 1
	}

	[Flags]
	public enum Source {
		None = 1,
		Mouse = 2,
		Keyboard = 4,
		Both = 6
	}

	public enum Speed {
		Default,
		SlowNormalFast
	}

	public enum Center {
		MouseLocation,
		ControlCenter
	}
}