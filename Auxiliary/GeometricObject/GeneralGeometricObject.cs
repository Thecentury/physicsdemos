using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.GeometricObject {
	public enum State {
		Clicked,
		Pointed,
		NotOn
	}

	public abstract class GeneralGeometricObject {
		protected AutoDisposablePen.AutoDisposablePen pen = new AutoDisposablePen.AutoDisposablePen();

		protected Point center;
		public Point Center {
			get { return center; }
			set { center = value; }
		}

		public Point Location {
			get { return new Point(center.X - size.Width / 2, center.Y - size.Height / 2); }
			set { center = new Point(value.X + size.Width / 2, value.Y + size.Height / 2); }
		}

		public Rectangle Bounds {
			get {
				return new Rectangle(Location, size);
			}
			set {
				this.Location = value.Location;
				this.size = value.Size;
			}
		}

		protected Size size;
		public Size Size {
			get { return size; }
			set { size = value; }
		}

		public virtual float Distance(Point p) {
			return (float)Math.Sqrt(Math.Pow(p.X - center.X, 2) + Math.Pow(p.Y - center.Y, 2));
		}

		public virtual bool ISClicked(Point p) {
			return MyRectangle.IsPointFInRectangleF(p, Bounds);
		}

		public abstract void Paint(Graphics g);

		public abstract void Paint(Graphics g, float distanceToMouseCursor);

		public virtual void Paint(Graphics g, Point cursorCoordinates) {
			Paint(g, Distance(cursorCoordinates));
		}

		protected void ApplyChanger(float distance) {
			changer.Apply(distance);
		}

		protected GeneralChanger changer;
		public GeneralChanger Changer {
			get { return changer; }
			set {
				changer = value;
				changer.AssignedObject = this;
			}
		}
	}
}
