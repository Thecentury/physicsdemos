using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.Functions;

namespace Thecentury.MyBitmap {
	public abstract class GeneralAnimatedBitmap {
		protected Bitmap bmp = null;
		public Bitmap Bitmap {
			get { return bmp; }
			set { bmp = value; Load(); }
		}

		protected bool repaint = false;

		protected Bitmap modifiedBMP = null;
		public Bitmap ModifiedBitmap {
			get {
				if (repaint) {
					ApplyAnimation();
					repaint = false;
				}
				return modifiedBMP;
			}
			set { modifiedBMP = value; }
		}

		public GeneralAnimatedBitmap() {
			Load();
		}

		public GeneralAnimatedBitmap(string fileName) {
			bmp = new Bitmap(Image.FromFile(fileName));
			Load();
		}

		protected void Load() {
			repaint = true;
		}

		public abstract void ApplyAnimation();
	}
}
