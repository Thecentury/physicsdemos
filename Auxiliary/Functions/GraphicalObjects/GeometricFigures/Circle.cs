using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	public class Circle : Ellipse {
		public Circle() : this(0, 0, 3) { }
		public Circle(PointF center, float radius)
			: base(center.X, center.Y, radius, radius) {
			this.Center = center;
			this.radius = radius;
		}
		public Circle(float centerX, float centerY, float radius) : this(new PointF(centerX, centerY), radius) { }

		protected float radius;
		public float Radius {
			get { return radius; }
			set {
				radius = value; repaint = true;
				this.bounds = new RectangleF(this.center, new SizeF(2 * radius, 2 * radius));
			}
		}

		new public float xHalfAxis {
			get { return radius; }
			set { Radius = value; }
		}
		new public float yHalfAxis {
			get { return radius; }
			set { Radius = value; }
		}
	}
}
