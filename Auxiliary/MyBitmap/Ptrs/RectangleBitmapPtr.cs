using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.MyBitmap.Ptrs {
	public class RectangleBitmapPtr : ConsequentBitmapPtr {
		protected Rectangle outer;
		protected Rectangle inner;

		public RectangleBitmapPtr(Bitmap bmp, Rectangle outer, Rectangle inner)
			: base(bmp) {
			this.outer = outer;
			this.inner = inner;
		}

		new public void ApplyToAllPixels() {
			Lock();
			unsafe {
				byte* p = bmpd.p;
				for (int y = 0; y < bmpd.height; y++) {
					for (int x = 0; x < bmpd.width; x++) {
						if (Thecentury.MyRectangle.IsPointInRectangle(x, y, outer) && !Thecentury.MyRectangle.IsPointInRectangle(x, y, inner)) {
							FillIsolatedPoint(p, x, y, f);
						}
						p += 3;
					}
					p += bmpd.nOffset;
				}
			}
			Unlock();
		}
	}
}
