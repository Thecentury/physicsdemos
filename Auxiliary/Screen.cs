using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury {
	public static class Screen {
		public static PointF TransformPointF(PointF p, RectangleF from, RectangleF to) {
			float x = (p.X - from.Left) / from.Width * to.Width + to.Left;
			float y = (1.0f - (p.Y - from.Top) / from.Height) * to.Height + to.Top;
			return new PointF(x, y);
		}

		public static PointF TransformPointF(PointF p, ref RectangleF from, ref RectangleF to) {
			float x = (p.X - from.Left) / from.Width * to.Width + to.Left;
			float y = (1.0f - (p.Y - from.Top) / from.Height) * to.Height + to.Top;
			return new PointF(x, y);
		}

		public static PointF TransformPointF(ref PointF p, ref RectangleF from, ref RectangleF to) {
			float x = (p.X - from.Left) / from.Width * to.Width + to.Left;
			float y = (1.0f - (p.Y - from.Top) / from.Height) * to.Height + to.Top;
			return new PointF(x, y);
		}

		public static void TransformSelfPointF(ref PointF p, ref RectangleF from, ref RectangleF to) {
			float x = (p.X - from.Left) / from.Width * to.Width + to.Left;
			float y = (1.0f - (p.Y - from.Top) / from.Height) * to.Height + to.Top;
			p = new PointF(x, y);
		}

		public static Point TransformPoint(PointF p, RectangleF from, RectangleF to) {
			float x = (p.X - from.Left) / from.Width * to.Width + to.Left;
			float y = (1.0f - (p.Y - from.Top) / from.Height) * to.Height + to.Top;
			return new Point((int)x, (int)y);
		}

		public static Point TransformPoint(ref PointF p, ref RectangleF from, ref RectangleF to) {
			float x = (p.X - from.Left) / from.Width * to.Width + to.Left;
			float y = (1.0f - (p.Y - from.Top) / from.Height) * to.Height + to.Top;
			return new Point((int)x, (int)y);
		}

		public static Point TransformPoint(PointF p, ref RectangleF from, ref RectangleF to) {
			float x = (p.X - from.Left) / from.Width * to.Width + to.Left;
			float y = (1.0f - (p.Y - from.Top) / from.Height) * to.Height + to.Top;
			return new Point((int)x, (int)y);
		}

		public static PointF[] TransformPointFs(ref PointF[] pts, ref RectangleF from, ref RectangleF to) {
			float rx = to.Width / from.Width;
			float ry = to.Height / from.Height;
			float cx = from.Left * rx + to.Left;
			float cy = to.Height + to.Top + from.Top * ry;

			PointF[] res = new PointF[pts.Length];
			for (int i = 0; i < res.Length; i++) {
				res[i] = new PointF(pts[i].X * rx - cx, cy - pts[i].Y * ry);
			}
			return res;
		}

		public static List<PointF[]> TransformPointFs(ref List<PointF[]> pts, ref RectangleF from, ref RectangleF to) {
			float rx = to.Width / from.Width;
			float ry = to.Height / from.Height;
			float cx = from.Left * rx + to.Left;
			float cy = to.Height + to.Top + from.Top * ry;

			List<PointF[]> res = new List<PointF[]>();
			PointF[] arr;
			for (int i = 0; i < res.Count; i++)
			{
				arr = res[i];
				res[i] = TransformPointFs(ref arr, ref from, ref to);
			}
			return res;
		}

		public static float TransformX(float x, RectangleF from, RectangleF to) {
			return (x - from.Left) / from.Width * to.Width + to.Left;
		}

		public static float TransformY(float y, RectangleF from, RectangleF to) {
			return (1.0f - (y - from.Top) / from.Height) * to.Height + to.Top;
		}

		public static Rectangle TransformRect(RectangleF rect, RectangleF from, RectangleF to) {
			Rectangle res = new Rectangle();
			res.Location = TransformPoint(rect.Location, from, to);
			res.Height = (int)(rect.Height * to.Height / from.Height);
			res.Width = (int)(rect.Width * to.Width / from.Width);
			return res;
		}

		public static Rectangle TransformRect(RectangleF rect, ref RectangleF from, ref RectangleF to) {
			Rectangle res = new Rectangle();
			res.Location = TransformPoint(rect.Location, from, to);
			res.Height = (int)(rect.Height * to.Height / from.Height);
			res.Width = (int)(rect.Width * to.Width / from.Width);
			return res;
		}

		public static Rectangle TransformRect(ref RectangleF rect, ref RectangleF from, ref RectangleF to) {
			Rectangle res = new Rectangle();
			res.Location = TransformPoint(rect.Location, from, to);
			res.Height = (int)(rect.Height * to.Height / from.Height);
			res.Width = (int)(rect.Width * to.Width / from.Width);
			return res;
		}

		public static RectangleF TransformRectF(RectangleF rect, RectangleF from, RectangleF to) {
			RectangleF res = new RectangleF();
			res.Location = TransformPointF(rect.Location, from, to);
			res.Height = rect.Height * to.Height / from.Height;
			res.Width = rect.Width * to.Width / from.Width;
			return res;
		}

		public static RectangleF TransformRectF(RectangleF rect, ref RectangleF from, ref RectangleF to) {
			RectangleF res = new RectangleF();
			res.Location = TransformPointF(rect.Location, from, to);
			res.Height = rect.Height * to.Height / from.Height;
			res.Width = rect.Width * to.Width / from.Width;
			return res;
		}

		public static RectangleF TransformRectF(ref RectangleF rect, ref RectangleF from, ref RectangleF to) {
			RectangleF res = new RectangleF();
			res.Location = TransformPointF(rect.Location, from, to);
			res.Height = rect.Height * to.Height / from.Height;
			res.Width = rect.Width * to.Width / from.Width;
			return res;
		}

		public static float XRatio(float x, ref RectangleF from) {
			return (x - from.Left) / from.Width;
		}

		public static float YRatio(float y, ref RectangleF from) {
			return (y - from.Top) / from.Height;
		}

		public static bool YIsNotOnScreen(float y, ref Rectangle screen) {
			return y > screen.Bottom || y < screen.Top;
		}

		public static bool YIsUpperTheScreen(float y, ref Rectangle screen) {
			return y > screen.Bottom;
		}

		public static bool YIsLowerTheScreen(float y, ref Rectangle screen) {
			return y < screen.Top;
		}

		public static bool YsAreOnDifferentSidesOfScreen(float y1, float y2, ref Rectangle screen) {
			return YIsLowerTheScreen(y1, ref screen) && YIsUpperTheScreen(y2, ref screen)
				|| YIsUpperTheScreen(y1, ref screen) && YIsLowerTheScreen(y2, ref screen);
		}

		public static bool YsAreOnLongDistance(float y1, float y2, ref Rectangle screen, float coeff) {
			return Math.Abs(y1 - y2) >= coeff * screen.Height;
		}

		public static bool Intersect(float a1, float b1, float a2, float b2, out float res1, out float res2) {
			res1 = Math.Max(a1, a2);
			res2 = Math.Min(b1, b2);
			return ((res1 == a1) && (res2 == b1) || (res1 == a2) && (res2 == b2)) || (res1 < res2);
		}
	}
}
