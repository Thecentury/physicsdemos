using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {
	public class ParametricFunction : GParametricFunction {
		protected function_p x;
		protected function_p y;

		public ParametricFunction(function_p x, function_p y) : this(x, y, 0, 1) { }

		public ParametricFunction(function_p x, function_p y, float startT, float endT) {
			this.x = x;
			this.y = y;
			this.startT = startT;
			this.endT = endT;
		}

		public static ParametricFunction ConstantFunction(PointF point) {
			return new ParametricFunction(delegate(float p) { return point.X; }, delegate(float p) { return point.Y; });
		}

		public override Rectangle To {
			get { return to; }
			set {
				to = value;
				points = LoadPoints(to, x, y);
			}
		}

		public override void Paint(Graphics g) {
			GeneralPaint(g, points);
		}
	}
}
