using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Thecentury.Diagnostics;
using System.Globalization;

namespace Thecentury {

	public struct StrokeInfo {
		public float x;
		public int power;
	}

#if false
	internal struct CharInfo : IComparable{
		public char c;
		public int counter;

		public CharInfo(char c, int counter) {
			this.c = c;
			this.counter = counter;
		}

	#region IComparable Members

#if false
		public int CompareTo(object obj) {
			CharInfo ci = (obj as CharInfo);

		}
#endif

	#endregion
	}
#endif

	public class StrokesFormat {
		string format;

		public StrokesFormat(string format) {
			this.format = format;
		}


#if false
		public StrokeInfo[] Apply(float start, float end) {
			List<CharInfo> chars = new List<CharInfo>();
			//List<int> numbers = new List<int>();
			//List<char> chars = new List<char>();
			
			foreach (char c in format) {
				bool found = false;
				for (int i = 0; i < chars.Count; i++) {
					if (chars[i].c == c) {
						chars[i].counter += 1;
						found = true;
						break;
					}
				}
				if (!found) {
					chars.Add(new CharInfo(c, 1));
				}
			}

			int number = MyString.NumberOfOccurences(format, 'a');
 
			StrokeInfo[] res = new StrokeInfo[format.Length];
			foreach (char c in format) {

			}
			return res;
		}
#endif

		//private CharInfo Search4Char(List<CharInfo> where, char c) {
		//    foreach (CharInfo ci in where) {
		//        if (ci.c == c) {
		//            return ci;
		//        }
		//    }
		//    return new CharInfo(c, -1);
		//}
	}

	[Flags]
	public enum AxisElements {
		None = 1,
		Axis = 2,
		Arrow = 4,
		Strokes = 8,
		Values = 16,
		All = 30
	}

	public enum ArrowStyle {
		Arrow,
		EmptyTriangle,
		FilledTriangle,
		StrokedTriangle,
		FilledArrow
	}

	public class FloatingAxisDivision {
		float xstart = 0.0f;
		float xstep = 1.0f;
		float ystart = 0.0f;
		float ystep = 1.0f;
		float[] xDivision;
		float[] yDivision;
		float[] viewXDivision;
		float[] viewYDivision;

		string[] xNames;
		string[] yNames;

		NumberFormatInfo info = new CultureInfo("en-US", false).NumberFormat;
		AutoDisposablePen.AutoDisposablePen pen = new AutoDisposablePen.AutoDisposablePen();

		RectangleF from;
		Rectangle to;

		// TODO Usage of all axis elements
		AxisElements xelems = AxisElements.All;
		AxisElements yelems = AxisElements.All;
		public AxisElements XElems {
			get { return xelems; }
			set { xelems = value; }
		}

		public AxisElements YElems {
			get { return yelems; }
			set { yelems = value; }
		}

		private ArrowStyle xArrow = ArrowStyle.FilledArrow;
		public ArrowStyle XArrowStyle {
			get { return xArrow; }
			set { xArrow = value; }
		}

		private ArrowStyle yArrow = ArrowStyle.FilledArrow;
		public ArrowStyle YArrowStyle {
			get { return yArrow; }
			set { yArrow = value; }
		}

		protected Size arrowSize = new Size(15, 10);
		public Size ArrowSize {
			get { return arrowSize; }
			set { arrowSize = value; }
		}

		protected Color cAxis = Color.Black;
		public Color CAxis {
			get { return cAxis; }
			set { cAxis = value; }
		}

		private Color cArrow = Color.Orange;
		public Color CArrow {
			get { return cArrow; }
			set { cArrow = value; }
		}


		protected int wAxis = 2;
		public int WAxis {
			get { return wAxis; }
			set { wAxis = value; }
		}

		protected int wArrow = 1;
		public int WArrow {
			get { return wArrow; }
			set { wArrow = value; }
		}

		protected Color cGrid = Color.LightGray;
		public Color CGrid { get { return cGrid; } set { cGrid = value; } }
		protected Color cText = Color.Black;
		public Color CText { get { return cText; } set { cText = value; } }

		protected int wGrid = 1;
		public int WGrid { get { return wGrid; } set { wGrid = value; } }
		protected int wText = 1;
		public int WText { get { return wText; } set { wText = value; } }
		protected int fontSize = 8;
		public int FontSize { get { return fontSize; } set { fontSize = value; } }
		protected string fontName = "Arial";
		public string FontName { get { return fontName; } set { fontName = value; } }

		public bool drawXGrid = true;
		public bool drawYGrid = true;
		public bool drawXValues = true;
		public bool drawYValues = true;


		// TODO Uniform Division
		public static FloatingAxisDivision UniformDivision(Rectangle to, float xstart, float ystart, float step, bool isX) {
			float x_div_y = to.Width / (float)to.Height;
			FloatingAxisDivision res;
			if (isX) {
				res = new FloatingAxisDivision(xstart, step, ystart, step / x_div_y);
			}
			else {
				res = new FloatingAxisDivision(xstart, step * x_div_y, ystart, step);
			}
			return res;
		}

		public bool drawGlobal {
			set {
				drawXGrid = value;
				drawXValues = value;
				drawYGrid = value;
				drawYValues = value;
			}
		}

		public RectangleF From {
			get {
				return from;
			}
			set {
				from = value;
				float first = xstart + xstep * (int)((from.Left - xstart) / xstep);
				int number = 1 + (int)((from.Right - first) / xstep);
				xDivision = new float[number];
				xNames = new string[number];
				for (int i = 0; i < number; i++) {
					xDivision[i] = first + i * xstep;
					xNames[i] = Thecentury.MyString.ConvertToString(xDivision[i], info);
				}

				first = ystart + ystep * (int)((from.Top - ystart) / ystep);
				number = 1 + (int)((from.Bottom - first) / ystep);
				yDivision = new float[number];
				yNames = new string[number];
				for (int i = 0; i < number; i++) {
					yDivision[i] = first + i * ystep;
					yNames[i] = Thecentury.MyString.ConvertToString(yDivision[i], info);
				}
			}
		}

		public int MaximalWidth {
			get {
				int max = 0;
				using (Font font = new Font(fontName, fontSize)) {
					foreach (string st in xNames) {
						max = Math.Max(TextRenderer.MeasureText(st, font).Width, max);
					}
				}
				return max;
			}
		}

		public int MaximalHeight {
			get {
				int max = 0;
				using (Font font = new Font(fontName, fontSize)) {
					foreach (string st in yNames) {
						max = Math.Max(TextRenderer.MeasureText(st, font).Height, max);
					}
				}
				return max;
			}
		}

		public Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				viewXDivision = new float[xDivision.Length];
				viewYDivision = new float[yDivision.Length];

				for (int i = 0; i < viewXDivision.Length; i++) {
					viewXDivision[i] = Screen.TransformX(xDivision[i], from, to);
				}
				for (int i = 0; i < viewYDivision.Length; i++) {
					viewYDivision[i] = Screen.TransformY(yDivision[i], from, to);
				}
			}
		}

		public void Paint(Graphics g, Rectangle r, Rectangle inner) {
			PaintX(g, inner);
			PaintY(g, inner);
			PaintXGrid(g, r);
			PaintYGrid(g, r);
			PaintXAxis(g, inner);
			PaintYAxis(g, inner);
		}

		protected void PaintYAxis(Graphics g, Rectangle r) {
			float x = Screen.TransformX(0, from, to);

			if (to.Left <= x && x <= to.Right) {

				if (yelems == (yelems | AxisElements.Axis)) {
					using (pen.Pen = new Pen(cAxis, wAxis)) {
						if (yArrow != ArrowStyle.EmptyTriangle) {
							g.DrawLine(pen, x, r.Top, x, r.Bottom);
						}
						else {
							g.DrawLine(pen, x, r.Top + arrowSize.Width, x, r.Bottom);
						}
					}
				}

				if (yelems == (yelems | AxisElements.Arrow)) {
					using (pen.Pen = new Pen(cArrow, wArrow)) {
						switch (yArrow) {
							case ArrowStyle.Arrow:
								g.DrawLine(pen, x, r.Top, x - arrowSize.Height / 2, r.Top + arrowSize.Width);
								g.DrawLine(pen, x, r.Top, x + arrowSize.Height / 2, r.Top + arrowSize.Width);
								break;
							case ArrowStyle.EmptyTriangle:
								g.DrawLine(pen, x, r.Top, x - arrowSize.Height / 2, r.Top + arrowSize.Width);
								g.DrawLine(pen, x, r.Top, x + arrowSize.Height / 2, r.Top + arrowSize.Width);
								g.DrawLine(pen, x - arrowSize.Height / 2, r.Top + arrowSize.Width, x + arrowSize.Height / 2, r.Top + arrowSize.Width);
								break;
							case ArrowStyle.FilledTriangle:
								MyGraphics.MyGraphics.DrawFilledPath(g, cArrow,
									new PointF(x, r.Top),
									new PointF(x - arrowSize.Height / 2, r.Top + arrowSize.Width),
									new PointF(x + arrowSize.Height / 2, r.Top + arrowSize.Width));
								break;
							case ArrowStyle.StrokedTriangle:
								g.DrawLine(pen, x, r.Top, x - arrowSize.Height / 2, r.Top + arrowSize.Width);
								g.DrawLine(pen, x, r.Top, x + arrowSize.Height / 2, r.Top + arrowSize.Width);
								g.DrawLine(pen, x - arrowSize.Height / 2, r.Top + arrowSize.Width, x + arrowSize.Height / 2, r.Top + arrowSize.Width);
								break;
							case ArrowStyle.FilledArrow:
								MyGraphics.MyGraphics.DrawFilledPath(g, cArrow,
									new PointF(x, r.Top),
									new PointF(x - arrowSize.Height / 2, r.Top + arrowSize.Width),
									new PointF(x, r.Top + arrowSize.Width / 2),
									new PointF(x + arrowSize.Height / 2, r.Top + arrowSize.Width));
								break;
							default:
								break;
						}
					}
				}
			}
		}

		protected void PaintXAxis(Graphics g, Rectangle r) {
			float y = Screen.TransformY(0, from, to);

			if (to.Top <= y && y <= to.Bottom) {

				if (xelems == (xelems | AxisElements.Axis)) {
					using (pen.Pen = new Pen(cAxis, wAxis)) {
						if (yArrow != ArrowStyle.EmptyTriangle) {
							g.DrawLine(pen, r.Left, y, r.Right, y);
						}
						else {
							g.DrawLine(pen, r.Left, y, r.Right - arrowSize.Width, y);
						}
					}
				}

				if (xelems == (xelems | AxisElements.Arrow)) {
					using (pen.Pen = new Pen(cArrow, wArrow)) {
						switch (xArrow) {
							case ArrowStyle.Arrow:
								g.DrawLine(pen, r.Right, y, r.Right - arrowSize.Width, y - arrowSize.Height / 2);
								g.DrawLine(pen, r.Right, y, r.Right - arrowSize.Width, y + arrowSize.Height / 2);
								break;
							case ArrowStyle.EmptyTriangle:
								g.DrawLine(pen, r.Right, y, r.Right - arrowSize.Width, y - arrowSize.Height / 2);
								g.DrawLine(pen, r.Right, y, r.Right - arrowSize.Width, y + arrowSize.Height / 2);
								g.DrawLine(pen, r.Right - arrowSize.Width, y - arrowSize.Height / 2, r.Right - arrowSize.Width, y + arrowSize.Height / 2);
								break;
							case ArrowStyle.FilledTriangle:
								MyGraphics.MyGraphics.DrawFilledPath(g, cArrow,
									new PointF(r.Right, y),
									new PointF(r.Right - arrowSize.Width, y - arrowSize.Height / 2),
									new PointF(r.Right - arrowSize.Width, y + arrowSize.Height / 2));
								break;
							case ArrowStyle.FilledArrow:
								MyGraphics.MyGraphics.DrawFilledPath(g, cArrow,
									new PointF(r.Right, y),
									new PointF(r.Right - arrowSize.Width, y - arrowSize.Height / 2),
									new PointF(r.Right - arrowSize.Width / 2, y),
									new PointF(r.Right - arrowSize.Width, y + arrowSize.Height / 2));
								break;
							case ArrowStyle.StrokedTriangle:
								g.DrawLine(pen, r.Right, y, r.Right - arrowSize.Width, y - arrowSize.Height / 2);
								g.DrawLine(pen, r.Right, y, r.Right - arrowSize.Width, y + arrowSize.Height / 2);
								g.DrawLine(pen, r.Right - arrowSize.Width, y - arrowSize.Height / 2, r.Right - arrowSize.Width, y + arrowSize.Height / 2);
								break;
							default:
								break;
						}
					}
				}
			}
		}

		protected void PaintX(Graphics g, Rectangle r) {
			if (drawXValues) {
				Font font = new Font(fontName, fontSize);
				Brush brush = new SolidBrush(cText);
				string str;
				SizeF newSize;

				using (pen.Pen = new Pen(cText, wText)) {
					for (int i = 0; i < viewXDivision.Length; i++) {
						if (r.Left <= viewXDivision[i] && viewXDivision[i] <= r.Right) {
							str = xNames[i];
							newSize = g.MeasureString(str, font);
							g.DrawString(str, font, brush, new PointF(viewXDivision[i] - 0.5f * newSize.Width, r.Bottom));
						}
					}
				}

				font.Dispose();
				brush.Dispose();
			}
		}

		protected void PaintY(Graphics g, Rectangle r) {
			if (drawYValues) {
				Font font = new Font(fontName, fontSize);
				Brush brush = new SolidBrush(cText);
				string str;
				SizeF newSize;

				using (pen.Pen = new Pen(cText, wText)) {
					for (int i = 0; i < viewYDivision.Length; i++) {
						if (r.Top <= viewYDivision[i] && viewYDivision[i] <= r.Bottom) {
							str = yNames[i];
							newSize = g.MeasureString(str, font);
							g.DrawString(str, font, brush, new PointF(r.Left - newSize.Width, viewYDivision[i] - 0.5f * newSize.Height));
						}
					}
				}

				font.Dispose();
				brush.Dispose();
			}
		}

		protected void PaintXGrid(Graphics g, Rectangle r) {
			if (drawXGrid) {
				using (pen.Pen = new Pen(cGrid, wGrid)) {
					for (int i = 0; i < viewXDivision.Length; i++) {
						g.DrawLine(pen, viewXDivision[i], r.Top, viewXDivision[i], r.Bottom);
					}
				}
			}
		}

		protected void PaintYGrid(Graphics g, Rectangle r) {
			if (drawYGrid) {
				using (pen.Pen = new Pen(cGrid, wGrid)) {
					for (int i = 0; i < viewYDivision.Length; i++) {
						g.DrawLine(pen, r.Left, viewYDivision[i], r.Right, viewYDivision[i]);
					}
				}
			}
		}

		public void ChangeScale(float xstep, float ystep) {
			MyDebug.CheckCondition(xstep > 0);
			MyDebug.CheckCondition(ystep > 0);

			this.xstep = xstep;
			this.ystep = ystep;
		}


		public void ZoomScale(float xratio, float yratio) {
			MyDebug.CheckCondition(xratio > 0);
			MyDebug.CheckCondition(yratio > 0);

			xstep *= xratio;
			ystep *= yratio;
		}


		public FloatingAxisDivision() { }

		public FloatingAxisDivision(float xstart, float xstep, float ystart, float ystep) {
			MyDebug.CheckCondition(xstep > 0);
			MyDebug.CheckCondition(ystep > 0);

			this.xstart = xstart;
			this.ystart = ystart;
			this.xstep = xstep;
			this.ystep = ystep;
		}
	}
}
