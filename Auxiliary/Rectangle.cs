using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury {
	public static class MyRectangle {
		public static Rectangle CreateFromDiagonalPoints(Point p1, Point p2) {
			int x1 = Math.Min(p1.X, p2.X);
			int x2 = Math.Max(p1.X, p2.X);
			int y1 = Math.Min(p1.Y, p2.Y);
			int y2 = Math.Max(p1.Y, p2.Y);

			return new Rectangle(x1, y1, x2 - x1, y2 - y1);
		}

		public static RectangleF CreateFromDiagonalPoints(PointF p1, PointF p2) {
			float x1 = Math.Min(p1.X, p2.X);
			float x2 = Math.Max(p1.X, p2.X);
			float y1 = Math.Min(p1.Y, p2.Y);
			float y2 = Math.Max(p1.Y, p2.Y);

			return new RectangleF(x1, y1, x2 - x1, y2 - y1);
		}

		public static Point ClipToRectangle(Point p, Rectangle r) {
			if (p.X < r.Left) {
				p.X = r.Left;
			}
			else if (p.X > r.Right) {
				p.X = r.Right;
			}
			if (p.Y < r.Top) {
				p.Y = r.Top;
			}
			else if (p.Y > r.Bottom) {
				p.Y = r.Bottom;
			}
			return p;
		}

		public static PointF ClipToRectangleF(PointF p, RectangleF r) {
			if (p.X < r.Left) {
				p.X = r.Left;
			}
			else if (p.X > r.Right) {
				p.X = r.Right;
			}
			if (p.Y < r.Top) {
				p.Y = r.Top;
			}
			else if (p.Y > r.Bottom) {
				p.Y = r.Bottom;
			}
			return p;
		}

		public static RectangleF RectZoom(RectangleF from, PointF to, float ratio) {
			RectangleF res = new RectangleF();
			res.X = to.X - (to.X - from.X) * ratio;
			res.Y = to.Y - (to.Y - from.Y) * ratio;
			res.Width = from.Width * ratio;
			res.Height = from.Height * ratio;
			return res;
		}

		public static RectangleF RectZoom(RectangleF from, PointF to, float xratio, float yratio) {
			RectangleF res = new RectangleF();
			res.X = to.X - (to.X - from.X) * xratio;
			res.Y = to.Y - (to.Y - from.Y) * yratio;
			res.Width = from.Width * xratio;
			res.Height = from.Height * yratio;
			return res;
		}

		public static float MaxShift(RectangleF r1, RectangleF r2) {
			return Math.Max(Math.Abs(r1.X - r2.X), Math.Abs(r1.Y - r2.Y));
		}

		public static RectangleF rectMove(RectangleF from, float dX, float dY) {
			RectangleF res = new RectangleF();
			res.X = from.X + dX;
			res.Y = from.Y + dY;
			res.Width = from.Width;
			res.Height = from.Height;
			return res;
		}

		public static float SquareRatio(RectangleF what, RectangleF ofWhat) {
			return what.Width * what.Height / (ofWhat.Width * ofWhat.Height);
		}

		public static float SquareRatioLessThan1(RectangleF r1, RectangleF r2) {
			float res = SquareRatio(r1, r2);
			return res < 1 ? res : 1f / res;
		}

		public static float SquareRatioGreaterThan1(RectangleF r1, RectangleF r2) {
			float res = SquareRatio(r1, r2);
			return res < 1 ? 1f / res : res;
		}

		public static bool IsPointFInRectangleF(ref PointF what, ref RectangleF where) {
			return (where.X <= what.X) && (what.X <= where.Right) && (where.Y <= what.Y) && (what.Y <= where.Bottom);
		}

		public static bool IsPointFInRectangleF(ref PointF what, RectangleF where) {
			return (where.X <= what.X) && (what.X <= where.Right) && (where.Y <= what.Y) && (what.Y <= where.Bottom);
		}

		public static bool IsPointFInRectangleF(PointF what, RectangleF where) {
			return (where.X <= what.X) && (what.X <= where.Right) && (where.Y <= what.Y) && (what.Y <= where.Bottom);
		}

		public static bool IsPointFInRectangleF(float x, float y, ref RectangleF where) {
			return (where.X <= x) && (x <= where.Right) && (where.Y <= y) && (y <= where.Bottom);
		}

		public static bool IsPointFInRectangleF(float x, float y, RectangleF where) {
			return (where.X <= x) && (x <= where.Right) && (where.Y <= y) && (y <= where.Bottom);
		}

		public static bool IsPointInRectangle(ref Point what, ref Rectangle where) {
			return (where.X <= what.X) && (what.X <= where.Right) && (where.Y <= what.Y) && (what.Y <= where.Bottom);
		}

		public static bool IsPointInRectangle(Point what, Rectangle where) {
			return (where.X <= what.X) && (what.X <= where.Right) && (where.Y <= what.Y) && (what.Y <= where.Bottom);
		}

		public static bool IsPointInRectangle(int x, int y, ref Rectangle where) {
			return (where.X <= x) && (x <= where.Right) && (where.Y <= y) && (y <= where.Bottom);
		}

		public static bool IsPointInRectangle(int x, int y, Rectangle where) {
			return (where.X <= x) && (x <= where.Right) && (where.Y <= y) && (y <= where.Bottom);
		}

		public static Rectangle Convert(RectangleF what) {
			return new Rectangle((int)what.X, (int)what.Y, (int)what.Width, (int)what.Height);
		}

		public static Point Convert(PointF what) {
			return new Point((int)what.X, (int)what.Y);
		}

		public static PointF rectCenter(RectangleF rect) {
			return new PointF(rect.Left + rect.Width * 0.5f, rect.Top + rect.Height * 0.5f);
		}

		public static float xRatio(RectangleF from, RectangleF to) {
			return (from.Width / to.Width);
		}

		public static float yRatio(RectangleF from, RectangleF to) {
			return (from.Height / to.Height);
		}
	}
}
