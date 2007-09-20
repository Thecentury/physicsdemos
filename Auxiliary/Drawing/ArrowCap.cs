using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Thecentury.Drawing {
	public class ArrowCap {
		protected SizeF size = new SizeF(6, 4);
		protected bool filled = false;
		protected float middleInset = 0f;

		public ArrowCap() { }

		public ArrowCap(float width, float height) : this(new SizeF(width, height)) { }

		public ArrowCap(SizeF size) {
			this.size = size;
		}

		public ArrowCap(float width, float height, bool filled) : this(new SizeF(width, height), filled) { }

		public ArrowCap(SizeF size, bool filled) {
			this.size = size;
			this.filled = filled;
		}

		public ArrowCap(float width, float height, bool filled, float middleInset) : this(new SizeF(width, height), filled, middleInset) { }

		public ArrowCap(SizeF size, bool filled, float middleInset) {
			this.size = size;
			this.filled = filled;
			this.middleInset = middleInset;
		}

		public AdjustableArrowCap Create() {
			AdjustableArrowCap res = new AdjustableArrowCap(size.Width, size.Height, filled);
			res.MiddleInset = middleInset;
			return res;
		}
	}
}
