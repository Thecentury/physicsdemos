using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {
	public class TableFunction : DiscreteFunction {

		public TableFunction(params float[] values) : base(values) { }

		public TableFunction(PointF[] values) : base(values) { }

		public override void Paint(Graphics g) {
			GeneralPaint(g, points);
		}

		public override Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				points = LoadPoints(to, values);
			}
		}
	}
}
