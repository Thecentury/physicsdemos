using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Thecentury.Functions;

namespace Thecentury.MyBitmap.Ptrs {
	public abstract class GeneralBitmapPtr{
		protected BmpDescriptor bmpd = null;
		protected formula f = null;
		public formula Formula {
			get { return f; }
			set { f = value; }
		}

		public GeneralBitmapPtr(Bitmap bmp) {
			this.bmpd = new BmpDescriptor(bmp);
		}

		public Bitmap OriginalBitmap {
			get { return bmpd.bmp; }
		}

		protected abstract void Lock();
		protected abstract void Unlock();
	}
}
