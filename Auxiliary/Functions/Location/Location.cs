using System;
using System.Drawing;
using System.Collections.Generic;

namespace Thecentury.Functions {
	// todo сделать Location нормально
	public static class Location {
		private static PointF location;

		public static PointF ApplyLocation(PointF p) {
			return new PointF(p.X + location.X, p.Y + location.Y);
		}

		public static PointF[] ApplyLocation(PointF[] pts) {
			PointF[] res = new PointF[pts.Length];
			for (int i = 0; i < res.Length; i++) {
				res[i] = Functions.Location.ApplyLocation(pts[i]);
			}
			return res;
		}

		public static List<PointF> ApplyLocation(List<PointF> lst) {
			List<PointF> res = new List<PointF>();
			foreach (PointF p in lst) {
				res.Add(Functions.Location.ApplyLocation(p));
			}
			return res;
		}

		public static List<List<PointF>> ApplyLocation(List<List<PointF>> lst) {
			List<List<PointF>> res = new List<List<PointF>>();
			foreach (List<PointF> l in lst) {
				res.Add(Functions.Location.ApplyLocation(l));
			}
			return res;
		}

		public static RectangleF ApplyLocation(RectangleF rect) {
			return new RectangleF(Functions.Location.ApplyLocation(rect.Location), rect.Size);
		}
	}
}
