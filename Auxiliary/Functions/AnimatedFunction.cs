using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.ColorFunctions;

namespace Thecentury.Functions {
	public class AnimatedFunction : GTimeDependentFunction {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="f">xt->1</param>
		public AnimatedFunction(formula f) {
			this.f = f;
		}

		public override void LoadPoints() {
			float t = time;
			points = LoadPoints(to, delegate(Vector v) { return f(new Vector(v[0], t)); });
		}

		public override Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				LoadPoints();
			}
		}
	}
}
