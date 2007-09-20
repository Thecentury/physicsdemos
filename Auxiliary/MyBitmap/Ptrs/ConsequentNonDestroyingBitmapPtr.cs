using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Thecentury.Functions;

namespace Thecentury.MyBitmap.Ptrs {
	public unsafe class ConsequentNonDestroyingColorBitmapPtr : GeneralBitmapPtr {
		protected BmpDescriptor bmpd2;

		public Bitmap ModifiedBitmap {
			get { return bmpd2.bmp; }
		}

		public ConsequentNonDestroyingColorBitmapPtr(Bitmap bmp)
			: base(bmp) {
			bmpd2 = new BmpDescriptor(new Bitmap(bmp));
		}

		public void ApplyToAllPixels(out Bitmap outBmp) {
			Lock();
			unsafe {
				byte* p = bmpd.p;
				byte* p2 = bmpd2.p;
				for (int y = 0; y < bmpd.height; y++) {
					for (int x = 0; x < bmpd.width; x++) {
						FillIsolatedPoint(p, p2, x, y, f);
						p += 3;
						p2 += 3;
					}
					p += bmpd.nOffset;
					p2 += bmpd.nOffset;
				}
			}
			Unlock();
			outBmp = bmpd2.bmp;
		}

		protected unsafe void FillIsolatedPoint(byte* ptr, byte* ptr2, int x, int y, formula f) {
			Vector res = f(Thecentury.ColorFunctions.MyColor.CompressTo0_1(ptr[2], ptr[1], ptr[0]));
			if (!res.IsEmpty) {
				Color c = Thecentury.ColorFunctions.MyColor.UncompressTo0_255(res[0], res[1], res[2]);
				ptr2[0] = c.B;
				ptr2[1] = c.G;
				ptr2[2] = c.R;
			} // иначе - не меняем
		}

		protected override void Lock() {
			bmpd.Lock();
			bmpd2.Lock();
		}

		protected override void Unlock() {
			bmpd.Unlock();
			bmpd2.Unlock();
		}
	}

	public unsafe class ConsequentNonDestroyingPointBitmapPtr : GeneralBitmapPtr {
		protected BmpDescriptor bmpd2;

		public Bitmap ModifiedBitmap {
			get { return bmpd2.bmp; }
		}

		public ConsequentNonDestroyingPointBitmapPtr(Bitmap bmp)
			: base(bmp) {
			bmpd2 = new BmpDescriptor(new Bitmap(bmp));
		}

		public void ApplyToAllPixels(out Bitmap outBmp) {
			Lock();
			unsafe {
				byte* p = bmpd.p;
				byte* p2 = bmpd2.p;
				for (int y = 0; y < bmpd.height; y++) {
					for (int x = 0; x < bmpd.width; x++) {
						FillPoint(p, p2, x, y, f);
						p += 3; p2 += 3;
					}
					p += bmpd.nOffset;
					p2 += bmpd.nOffset;
				}
			}
			Unlock();
			outBmp = bmpd2.bmp;
		}

		protected unsafe void FillPoint(byte* ptr, byte* ptr2, int x, int y, formula f) {
			Vector res = f(new Vector(x,y));
			if (!res.IsEmpty) {
				Color c = Thecentury.ColorFunctions.MyColor.UncompressTo0_255(res[0], res[1], res[2]);
				ptr2[0] = c.B;
				ptr2[1] = c.G;
				ptr2[2] = c.R;
			} // иначе - не меняем
		}

		protected override void Lock() {
			bmpd.Lock();
			bmpd2.Lock();
		}

		protected override void Unlock() {
			bmpd.Unlock();
			bmpd2.Unlock();
		}
	}
}
