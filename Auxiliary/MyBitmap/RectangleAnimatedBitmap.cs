using System;
using System.Collections.Generic;
using System.Text;
using Thecentury.Functions;
using Thecentury.MyBitmap.Ptrs;

namespace Thecentury.MyBitmap {
	public class RectangleAnimatedBitmap : GeneralAnimatedBitmap {
		public RectangleAnimatedBitmap(string fileName)
			: base(fileName) {
		}

		public RectangleAnimatedBitmap() : base() { }

		protected formula f = Thecentury.Functions.Formulas.Identity;
		public formula Formula {
			get { return f; }
			set { f = value; ptr.Formula = f; repaint = true; }
		}

		protected RectangleBitmapPtr ptr;
		public RectangleBitmapPtr Ptr {
			get { return ptr; }
			set { ptr = value; }
		}

		public override void ApplyAnimation() {
			ptr.ApplyToAllPixels();
		}
	}
}
