using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {
	// todo восстановить количество ветвей
	public class MultiBranchedFunction : GFormulaFunction {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="f"></param>
		/// <param name="NumberOfBranches">x->many</param>
		public MultiBranchedFunction(formula f) {
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
				// TODO: Переделать LoadPoints
				//to = value;
				//points = new List<List<Point>>();
				//List<List<Point>> temp = new List<List<Point>>();
				//for (int i = 0; i < noBranches; i++) {
				//    temp = LoadPoints(to, delegate(float x) { return f(x, i); });
				//    foreach (List<Point> lst in temp) {
				//        points.Add(lst);
				//    }
				//}
			}
		}
	}
}
