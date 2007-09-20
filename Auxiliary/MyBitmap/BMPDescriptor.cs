using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace Thecentury.MyBitmap {
	public unsafe class BmpDescriptor {
		public BitmapData bmData;
		public int stride;
		public IntPtr Scan0;
		public int nOffset;
		public int width;
		public int height;
		public byte* p;
		public Bitmap bmp;

		public BmpDescriptor(Bitmap bmp) {
			this.bmp = bmp;
			width = bmp.Width;
			height = bmp.Height;
		}

		public void Lock() {
			bmData = bmp.LockBits(new Rectangle(0, 0, width, height),
				ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			stride = bmData.Stride;
			Scan0 = bmData.Scan0;
			nOffset = stride - bmp.Width * 3;
			p = (byte*)(void*)Scan0;
		}

		public void Lock(Rectangle r) {
			bmData = bmp.LockBits(
				r, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			stride = bmData.Stride;
			Scan0 = bmData.Scan0;
			nOffset = stride - r.Width * 3;// ??? //bmp.Width * 3;
			p = (byte*)(void*)Scan0;
		}

		public void Unlock() {
			bmp.UnlockBits(bmData);
		}
	}
}
