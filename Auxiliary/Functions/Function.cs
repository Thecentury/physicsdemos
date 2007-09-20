using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {

	// todo корректная обработка того, что функция не определена
	public class Function : GFormulaFunction {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="f">x->y</param>
		public Function(formula f) {
			this.f = f;
		}

		public override Rectangle To {
			get { return to; }
			set {
				to = value;
				points = LoadPoints(to, f);
				repaint = true;
			}
		}
	}
}
