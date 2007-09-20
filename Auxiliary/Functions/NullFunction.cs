using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Functions {
	public class NullFunction : PraFunction {
		public override void Paint(System.Drawing.Graphics g) {
			// Draw nothing
		}

		public override void Recomputate(System.Drawing.RectangleF where) {
			// do nothing
		}
	}
}
