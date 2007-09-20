using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury {
	public class MyPoint {
		public static Point NewZero(Point previous, Point newZero) {
			return new Point(previous.X - newZero.X, previous.Y - newZero.Y);
		}

		public static PointF NewZero(PointF previous, PointF newZero) {
			return new PointF(previous.X - newZero.X, previous.Y - newZero.Y);
		}
	}
}
