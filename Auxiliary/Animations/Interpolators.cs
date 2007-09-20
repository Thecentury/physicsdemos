using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Animations {
	public delegate float interpolate(float ratio);

	public static class Interpolators {
		//public static float PI_Div_2 = (float)Math.PI * 0.5f;

		public static float Linear(float ratio) {
			return ratio;
		}

		public static float SinPI(float ratio) {
			return (float)(Math.Sin(ratio * Math.PI));
		}

		public static float Abs(float ratio) {
			return (ratio <= 0.5f) ? (2 * ratio) : (2 - 2 * ratio);
		}

		public static float Pow2(float ratio) {
			return ratio * ratio;
		}

		public static float Pow3(float ratio) {
			return ratio * ratio * ratio;
		}

		public static float Pow4(float ratio) {
			return (ratio * ratio * ratio * ratio);
		}

		public static float Root2(float ratio) {
			return (float)Math.Sqrt(ratio);
		}

		public static float Root3(float ratio) {
			return (float)Math.Pow(ratio, 0.333333333333);
		}

		public static float Root4(float ratio) {
			return (float)Math.Pow(ratio, 0.25);
		}

		public static float SlowEndsFastMiddle_Cos(float ratio) {
			return (float)(0.5 - 0.5 * Math.Cos(Math.PI * ratio));
		}

		public static float SlowEndsFastMiddle_Pow2(float ratio) {
			return ratio < 0.5f ?
				ratio * ratio * 2 :
				1 - 2 * (1 - ratio) * (1 - ratio);
		}

		public static float SlowEndsFastMiddle_Pow3(float ratio) {
			return ratio < 0.5f ?
				ratio * ratio * ratio * 4 :
				1 - 4 * (1 - ratio) * (1 - ratio) * (1 - ratio);
		}

		public static float SlowEndsFastMiddle_Pow4(float ratio) {
			return ratio < 0.5f ?
				ratio * ratio * ratio * ratio * 8 :
				1 - 8 * (1 - ratio) * (1 - ratio) * (1 - ratio) * (1 - ratio);
		}

	}
}
