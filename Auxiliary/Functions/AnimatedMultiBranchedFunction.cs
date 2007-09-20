using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {

	/// <summary>
	/// Должна возвращать все время одно и то же количество значений
	/// </summary>
	public class AnimatedMultiBranchedFunction : GTimeDependentFunction {
		// TODO: переделать под yield return

		/// <summary>
		/// 
		/// </summary>
		/// <param name="f">xt->y</param>
		public AnimatedMultiBranchedFunction(formula f) {
			this.f = f;
		}

		public override void Paint(Graphics g) {
			GeneralPaint(g, points);
		}

		public override Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				points = new List<List<Point>>();
				List<List<Point>> temp = new List<List<Point>>();
				// TODO: Подумать, может можно сделать лучше
				int noBranches = f(new Vector(0, 0)).Length;
				List<List<Point>> pts = new List<List<Point>>();
				
				for (int i = 0; i < noBranches; i++) {
					pts[i] = new List<Point>();
				}

				// TODO: Переделать LoadPoints для многозначных фукнций
				float t = time;
				for (int i = 0; i < noBranches; i++) {
					//temp = LoadPoints(to, delegate(Vector v) { return f(v, t, i); });
					//foreach (List<Point> lst in temp) {
					//    points.Add(lst);
					//}
				}
			}
		}
	}
}
