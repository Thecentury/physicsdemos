using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using Thecentury.Functions;

namespace Thecentury.MyBitmap.Ptrs {
	public unsafe class ConsequentBitmapPtr : GeneralBitmapPtr {
		public ConsequentBitmapPtr(Bitmap bmp) : base(bmp) { }

		public void ApplyToAllPixels() {
			Lock();
			unsafe {
				byte* p = bmpd.p;
				for (int y = 0; y < bmpd.height; y++) {
					for (int x = 0; x < bmpd.width; x++) {
						FillIsolatedPoint(p, x, y, f);
						p += 3;
					}
					p += bmpd.nOffset;
				}
			}
			Unlock();
		}

		protected unsafe void FillIsolatedPoint(byte* ptr, int x, int y, formula f) {
			Vector res = f(Thecentury.ColorFunctions.MyColor.CompressTo0_1(ptr[2], ptr[1], ptr[0]));
			if (!res.IsEmpty) {
				Color c = Thecentury.ColorFunctions.MyColor.UncompressTo0_255(res[0], res[1], res[2]);
				ptr[0] = c.B;
				ptr[1] = c.G;
				ptr[2] = c.R;
			} // иначе - не меняем
		}

		protected override void Lock() {
			bmpd.Lock();
		}

		protected override void Unlock() {
			bmpd.Unlock();
		}
	}
}
