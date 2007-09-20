using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	public class Ellipse : GGeometricFigure {
		protected RectangleF bounds;
		public RectangleF Bounds {
			get { return bounds; }
			set { bounds = value; repaint = true; }
		}
		protected Rectangle rect2draw;

		public Ellipse() : this(new RectangleF(-2, -2, 4, 4)) { }
		public Ellipse(float centerX, float centerY, float xAxis, float yAxis)
			: this(new RectangleF(centerX - xAxis, centerY + yAxis, 2 * xAxis, 2 * yAxis)) { }
		public Ellipse(RectangleF boundingRect) {
			this.bounds = boundingRect;
			if (isPinned) {
				this.Center = new PointF(boundingRect.X + 0.5f * boundingRect.Width, boundingRect.Y + 0.5f * boundingRect.Height);
			}
			else {
				this.Center = new PointF(boundingRect.X + 0.5f * boundingRect.Width, boundingRect.Y - 0.5f * boundingRect.Height);
			}
		}

		protected PointF center;
		public PointF Center {
			get { return center; }
			set {
				center = value;
				repaint = true;
				if (false & isPinned) {
					bounds.Location = new PointF(center.X - 0.5f * bounds.Width, center.Y - 0.5f * bounds.Height);
				}
				else {
					bounds.Location = new PointF(center.X - 0.5f * bounds.Width, center.Y + 0.5f * bounds.Height);
				}
			}
		}

		public float centerX {
			get { return center.X; }
			set {
				Center = new PointF(value, center.Y);
				repaint = true;
			}
		}

		public float centerY {
			get { return center.Y; }
			set { Center = new PointF(center.X, value); repaint = true; }
		}

		public float xHalfAxis {
			get { return bounds.Width * 0.5f; }
			set { this.bounds.Width = 2 * value; repaint = true; }
		}
		public float yHalfAxis {
			get { return bounds.Height * 0.5f; }
			set { this.bounds.Height = 2 * value; repaint = true; }
		}

		public float XAxis {
			get { return bounds.Width; }
			set { this.bounds.Width = value; repaint = true; }
		}
		public float YAxis {
			get { return bounds.Height; }
			set { this.bounds.Height = value; repaint = true; }
		}

		protected override void PinnedRecomputate() {
			this.ForceRepaint();
			if (isPinned) {
				//this.bounds = new RectangleF(center.X - bounds.Width, center.Y - bounds.Height, bounds.Width, bounds.Height);
				this.centerY -= bounds.Height;
			}
			else {
				//this.bounds = new RectangleF(center.X - bounds.Width, center.Y + bounds.Height, bounds.Width, bounds.Height);
				this.centerY += bounds.Height;
			}
		}

		public override Rectangle To {
			get { return this.to; }
			set {
				this.to = value;
				if (isPinned) {
					rect2draw = Thecentury.MyRectangle.Convert(Functions.Location.ApplyLocation(bounds));
				}
				else {
					rect2draw = Screen.TransformRect(Functions.Location.ApplyLocation(bounds), from, to);
				}
			}
		}

		public override void Paint(Graphics g) {
			ApplyAnimations();
			using (pen.Pen = new Pen(cLine, wLine)) {
				g.DrawEllipse(pen, rect2draw);
				if (filled) {
					using (Brush brush = new SolidBrush(fillColor)) {
						g.FillEllipse(brush, rect2draw);
					}
				}
			}
		}
	}
}
