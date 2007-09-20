using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury {
	public static class Interpolation {
		public static Color Interpolate(Color from, Color to, double ratio) {
			return Color.FromArgb(CutToBorder((int)(from.A * (1 - ratio) + to.A * ratio), 0, 255), CutToBorder((int)(from.R * (1 - ratio) + to.R * ratio), 0, 255), CutToBorder((int)(from.G * (1 - ratio) + to.G * ratio), 0, 255), CutToBorder((int)(from.B * (1 - ratio) + to.B * ratio), 0, 255));
		}

		public static int CutToBorder(int value, int min, int max) {
			if (value < min) {
				return min;
			}
			if (value > max) {
				return max;
			}
			return value;
		}

		public static int CutToBorder(double value, int min, int max) {
			if (value < min) {
				return min;
			}
			if (value > max) {
				return max;
			}
			return (int)value;
		}

		public static Size Interpolate(Size from, Size to, float ratio) {
			return new Size((int)(from.Width * (1 - ratio) + to.Width * ratio), (int)(from.Height * (1 - ratio) + to.Height * ratio));
		}
		public static SizeF Interpolate(SizeF from, SizeF to, float ratio) {
			return new SizeF((from.Width * (1 - ratio) + to.Width * ratio), (from.Height * (1 - ratio) + to.Height * ratio));
		}
		public static Point Interpolate(Point from, Point to, float ratio) {
			return new Point((int)(from.X * (1 - ratio) + to.X * ratio), (int)(from.Y * (1 - ratio) + to.Y * ratio));
		}
		public static PointF Interpolate(PointF from, PointF to, float ratio) {
			return new PointF(from.X * (1 - ratio) + to.X * ratio, from.Y * (1 - ratio) + to.Y * ratio);
		}
		public static Rectangle Interpolate(Rectangle from, Rectangle to, float ratio) {
			return new Rectangle(Interpolate(from.Location, to.Location, ratio), Interpolate(from.Size, to.Size, ratio));
		}
		public static RectangleF Interpolate(RectangleF from, RectangleF to, float ratio) {
			return new RectangleF(Interpolate(from.Location, to.Location, ratio), Interpolate(from.Size, to.Size, ratio));
		}

	}
}
