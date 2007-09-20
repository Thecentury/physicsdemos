using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury {
	public sealed class MyMath {
		public static float Remainder(float x, float y) {
			float res = x / y;
			return x - y * (float)System.Math.Floor(res);
		}
		public static bool IsBetween(float what, float a, float b) {
			return a <= what && what <= b;
		}
		public static float Sin(float a) {
			return (float)Math.Sin(a);
		}
		public static float Cos(float a) {
			return (float)Math.Cos(a);
		}
		public static float Atan(float a) {
			return (float)Math.Atan(a);
		}
		public static double Sin01(double a) {
			return 0.5 + 0.5 * Math.Sin(a);
		}
		public static double Cos01(double a) {
			return 0.5 + 0.5 * Math.Cos(a);
		}
	}
}
