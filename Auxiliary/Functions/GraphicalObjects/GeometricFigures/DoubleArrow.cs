using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using Thecentury.Drawing;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	public class DoubleArrow : Arrow {
		protected AdjustableArrowCap startCap;

		protected ArrowCap startArrowCap = new ArrowCap();
		public ArrowCap StartArrowCap {
			get { return startArrowCap; }
			set { startArrowCap = value; repaint = true; }
		}

		public DoubleArrow()
			: base() {
			startCap = startArrowCap.Create();
		}

		public DoubleArrow(ArrowCap arrowCap) : this(arrowCap, arrowCap) { }

		public DoubleArrow(ArrowCap startArrowCap, ArrowCap finishArrowCap)
			: base(finishArrowCap) {
			this.startArrowCap = startArrowCap;
			startCap = startArrowCap.Create();
		}

		public DoubleArrow(float xStart, float yStart, float xFinish, float yFinish,
			ArrowCap arrowCap)
			: this(xStart, yStart, xFinish, yFinish, Color.Black, 1, arrowCap, arrowCap) { }

		public DoubleArrow(PointF start, PointF finish, ArrowCap arrowCap)
			: this(start, finish, Color.Black, 1, arrowCap, arrowCap) { }

		public DoubleArrow(float xStart, float yStart, float xFinish, float yFinish,
			Color color, int width, ArrowCap arrowCap)
			: this(xStart, yStart, xFinish, yFinish, color, width, arrowCap, arrowCap) { }

		public DoubleArrow(PointF start, PointF finish, Color color, int width, ArrowCap arrowCap)
			: this(start, finish, color, width, arrowCap, arrowCap) { }

		public DoubleArrow(float xStart, float yStart, float xFinish, float yFinish,
			ArrowCap startArrowCap, ArrowCap finishArrowCap)
			: this(xStart, yStart, xFinish, yFinish, Color.Black, 1,
			startArrowCap, finishArrowCap) { }

		public DoubleArrow(PointF start, PointF finish,
			ArrowCap startArrowCap, ArrowCap finishArrowCap)
			: this(start, finish, Color.Black, 1, startArrowCap, finishArrowCap) { }

		public DoubleArrow(float xStart, float yStart, float xFinish, float yFinish,
			Color color, int width, ArrowCap startArrowCap, ArrowCap finishArrowCap)
			: this(new PointF(xStart, yStart), new PointF(xFinish, yFinish), color, width,
			startArrowCap, finishArrowCap) { }

		public DoubleArrow(PointF start, PointF finish, Color color, int width,
			ArrowCap startArrowCap, ArrowCap finishArrowCap)
			: base(start, finish, color, width, finishArrowCap) {
			this.startArrowCap = startArrowCap;
			startCap = startArrowCap.Create();
		}

		public override void Paint(System.Drawing.Graphics g) {
			ApplyAnimations();
			using (pen.Pen = new Pen(cLine, wLine)) {
				pen.Pen.CustomEndCap = cap;
				pen.Pen.CustomStartCap = startCap;
				g.DrawLine(pen, drawP1, drawP2);
			}
		}
	}
}
