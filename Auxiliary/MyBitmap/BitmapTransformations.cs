using System;
using System.Collections.Generic;
using System.Text;
using Thecentury.Functions;

namespace Thecentury.MyBitmap {
	public static class BitmapTransformations {
		//todo доработать
		public static Vector Sepia(Vector v) {
			double ave = 0.33333333333 * (v[0] + v[1] + v[2]);
			return new Vector(1.2 * ave, 0.9 * ave, 0.9 * ave);
		}

		public static Vector GrayScale(Vector v) {
			double ave = 0.333333333 * (v[0] + v[1] + v[2]);
			return new Vector(ave, ave, ave);
		}

		public static Vector BlackNWhite(Vector v) {
			double ave = 0.11 * v[0] + 0.53 * v[1] + 0.36 * v[2];
			return new Vector(ave, ave, ave);
		}

		public static Vector Identity(Vector v) {
			return v;
		}

		public static Vector Invert(Vector v) {
			return new Vector(1 - v[0], 1 - v[1], 1 - v[2]);
		}

		public static Vector IncBrightness(Vector v) {
			return new Vector(Math.Sqrt(v[0]), Math.Sqrt(v[1]), Math.Sqrt(v[2]));
		}

		public static Vector DecBrightness(Vector v) {
			return new Vector(v[0] * v[0], v[1] * v[1], v[2] * v[2]);
		}

		public static Vector IncreaseSmth(Vector v) {
			double sum = (v[0] + v[1] + v[2]) * 0.3333333333;
			return new Vector(
				v[0] + (1 - v[0]) * sum,
				v[1] + (1 - v[1]) * sum,
				v[2] + (1 - v[2]) * sum);
		}

		public static Vector Normalize(Vector v) {
			double len = Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
			len = len > 0 ? len : 0.0000001;
			return v / len;
		}
	}
}
