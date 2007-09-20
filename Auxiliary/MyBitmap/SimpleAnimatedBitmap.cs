using System;
using System.Collections.Generic;
using System.Text;
using Thecentury.Functions;
using Thecentury.MyBitmap.Ptrs;

namespace Thecentury.MyBitmap {
	public class SimpleAnimatedBitmap : GeneralAnimatedBitmap {
		public SimpleAnimatedBitmap(string fileName)
			: base(fileName) {
			ptr = new ConsequentNonDestroyingColorBitmapPtr(bmp);
			ptr.Formula = f;
		}

		public SimpleAnimatedBitmap() : base() { }

		protected formula f = Thecentury.Functions.Formulas.Identity;
		public formula Formula {
			get { return f; }
			set { f = value; ptr.Formula = f; repaint = true; }
		}

		protected ConsequentNonDestroyingColorBitmapPtr ptr;
		public ConsequentNonDestroyingColorBitmapPtr Ptr {
			get { return ptr; }
			set { ptr = value; }
		}

		public override void ApplyAnimation() {
			ptr.ApplyToAllPixels(out modifiedBMP);
		}
	}
}
