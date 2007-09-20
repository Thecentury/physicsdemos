using System;
using System.Drawing;

namespace Thecentury {
	public enum LayoutKind { 
		Fill,
		Center
	}

	public struct Bind {
		Point p1;
		Point p2;
		public Bind(Point p1, Point p2) {
			this.p1 = p1;
			this.p2 = p2;
		}
	}

	// todo доделать
	public sealed class RectangleFormula {
		private float x1;
		private float x2;
		private float y1;
		private float y2;

		public RectangleFormula(float x1, float x2, float y1, float y2) {

		}
	}

	public sealed class BindManager{
		private Point p;
		public Point P {
			get { return p; }
			set { p = value; }
		}

		private Bind b;
		public Bind B {
			get { return b; }
			set { b = value; }
		}

		private LayoutKind l;
		public LayoutKind L {
			get { return l; }
			set { l = value; }
		}

		public BindManager(Point p) {
			this.p = p;
			l = LayoutKind.Center;
		}

		public BindManager(Bind b) {
			this.b = b;
			l = LayoutKind.Fill;
		}
	}

	public sealed class AutoLayouter {
		private LayoutKind layout = LayoutKind.Fill;
		public LayoutKind LayoutKind {
			get { return layout; }
			set { layout = value; }
		}

		private Rectangle ownSize;
		public Rectangle OwnSize {
			get { return ownSize; }
			set { ownSize = value; }
		}

		//private 
	}
}
