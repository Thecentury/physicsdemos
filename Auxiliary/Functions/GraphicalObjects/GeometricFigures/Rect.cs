using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.Functions;

namespace Thecentury.Functions.GraphicalObjects {

	public class Rect : GGeometricFigure {
		protected RectangleF rect;
		public RectangleF Rectangle {
			get { return rect; }
			set { rect = value; repaint = true; }
		}
		public float X {
			get { return rect.X; }
			set { rect.X = value; repaint = true; }
		}
		public float Y {
			get { return rect.Y; }
			set { rect.Y = value; repaint = true; }
		}
		public float Width {
			get { return rect.Width; }
			set { rect.Width = value; repaint = true; }
		}
		public float Height {
			get { return rect.Height; }
			set { rect.Height = value; repaint = true; }
		}
		public PointF Center {
			get { return MyRectangle.rectCenter(rect); }
			set { rect.Location = new PointF(value.X - rect.Width * 0.5f, value.Y - rect.Height * 0.5f); repaint = true; }
		}

		protected Rectangle rect2draw;

		public Rect() : this(new RectangleF(-1, -1, 2, 2)) { }
		public Rect(float x, float y, float width, float height) : this(new RectangleF(x, y, width, height)) { }
		public Rect(RectangleF rect) { this.rect = rect; }

		public override Rectangle To {
			get { return to; }
			set {
				to = value;
				Center = Functions.Location.ApplyLocation(Center);
				if (isPinned) {
					rect2draw = Thecentury.MyRectangle.Convert(rect);
				}
				else {
					rect2draw = Screen.TransformRect(rect, from, to);
				}
			}
		}

		protected override void PinnedRecomputate() { this.ForceRepaint(); }

		public override void Paint(Graphics g) {
			ApplyAnimations();
			using (pen.Pen = new Pen(cLine, wLine)) {
				g.DrawRectangle(pen, rect2draw);
				if (filled) {
					using (Brush brush = new SolidBrush(fillColor)) {
						g.FillRectangle(brush, rect2draw);
					}
				}
			}
		}
	}
}
