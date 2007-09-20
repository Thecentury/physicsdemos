using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Layout {
	public class Shift {
		ElementaryShift x;
		ElementaryShift y;

		public Shift() {
			x = new ElementaryShift();
			y = new ElementaryShift();
		}

		public Shift(ElementaryShift x, ElementaryShift y) {
			this.x = x;
			this.y = y;
		}

		public Shift(float xPerc, int xPx, float yPerc, int yPx) {
			this.x = new ElementaryShift(xPerc, xPx);
			this.y = new ElementaryShift(yPerc, yPx);
		}
	}
}
