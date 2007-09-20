using System;
using System.Collections.Generic;
using System.Text;
using Thecentury.Functions;

namespace Thecentury.TimeControl {
	public class FormulaTimer : GeneralTimer {
		protected function_t f;

		public function_t Function { get { return f; } set { f = value; } }

		public FormulaTimer(Functions.function_t f) {
			this.f = f;
		}
		
		public override float time {
			get {
				return f(base.time);
			}
		}

	}
}
