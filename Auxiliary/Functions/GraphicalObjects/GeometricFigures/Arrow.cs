using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.Drawing;
using System.Drawing.Drawing2D;

namespace Thecentury.Functions.GraphicalObjects {
	public class Arrow : Line {
		protected AdjustableArrowCap cap;

		protected ArrowCap arrowCap = new ArrowCap();
		public ArrowCap ArrowCap {
			get { return arrowCap; }
			set { arrowCap = value; repaint = true; }
		}

		public Arrow() {
			cap = arrowCap.Create();
		}

		public Arrow(ArrowCap arrowCap) {
			this.arrowCap = arrowCap;
			cap = arrowCap.Create();
		}

		public Arrow(float xStart, float yStart, float xFinish, float yFinish, ArrowCap arrowCap)
			: this(xStart, yStart, xFinish, yFinish, Color.Black, 1, arrowCap) { }

		public Arrow(float xStart, float yStart, float xFinish, float yFinish, Color color,
			int width, ArrowCap arrowCap)
			: this(new PointF(xStart, yStart), new PointF(xFinish, yFinish), color,
			width, arrowCap) { }

		public Arrow(PointF start, PointF finish, ArrowCap arrowCap)
			: this(start, finish, Color.Black, 1, arrowCap) { }

		public Arrow(PointF start, PointF finish, Color color, int width, ArrowCap arrowCap)
			: base(start, finish, color, width) {
			this.arrowCap = arrowCap;
			cap = arrowCap.Create();
		}

		public override void Paint(System.Drawing.Graphics g) {
			ApplyAnimations();
			using (pen.Pen = new Pen(cLine, wLine)) {
				pen.Pen.CustomEndCap = cap;
				g.DrawLine(pen, drawP1, drawP2);
			}
		}
	}
}
