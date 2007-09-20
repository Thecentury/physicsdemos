using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	public class Line : GGeometricFigure {
		protected PointF p1;
		public PointF P1 {
			get { return p1; }
			set { p1 = value; repaint = true; }
		}

		protected PointF p2;
		public PointF P2 {
			get { return p2; }
			set { p2 = value; repaint = true; }
		}

		protected Point drawP1;
		protected Point drawP2;

		public Line() : this(new PointF(0, 0), new PointF(1, 1)) { }

		/// <summary>
		/// Initializes a new instance of the Line class.
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		public Line(PointF p1, PointF p2) : this(p1, p2, Color.Black, 1) { }

		public Line(float x1, float y1, float x2, float y2)
			: this(x1, y1, x2, y2, Color.Black, 1) { }

		/// <summary>
		/// Initializes a new instance of the Line class.
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		public Line(PointF p1, PointF p2, Color color, int width) {
			this.p1 = p1;
			this.p2 = p2;
			this.cLine = color;
			this.wLine = width;
		}

		public Line(float x1, float y1, float x2, float y2, Color color, int width)
			: this(new PointF(x1, y1), new PointF(x2, y2), color, width) { }

		protected override void PinnedRecomputate() {
			this.ForceRepaint();
		}

		public override System.Drawing.Rectangle To {
			get { return this.to; }
			set {
				this.to = value;
				if (isPinned) {
					drawP1 = new Point((int)p1.X, (int)p1.Y);
					drawP2 = new Point((int)p2.X, (int)p2.Y);
				}
				else {
					drawP1 = Screen.TransformPoint(p1, from, to);
					drawP2 = Screen.TransformPoint(p2, from, to);
				}
			}
		}

		public override void Paint(System.Drawing.Graphics g) {
			ApplyAnimations();
			using (pen.Pen = new Pen(cLine, wLine)) {
				g.DrawLine(pen, drawP1, drawP2);
			}
		}
	}
}
