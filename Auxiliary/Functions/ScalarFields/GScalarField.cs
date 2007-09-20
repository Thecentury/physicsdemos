using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Thecentury.Functions {
	public abstract class GScalarField : GFunction {
		protected Bitmap bmp;
		protected Rectangle BMPRect {
			get { return new Rectangle(0, 0, to.Width, to.Height); }
		}

		protected formula f;
		public formula Formula {
			get { return f; }
			set { f = value; repaint = true; }
		}

		protected Color c0 = Color.Black;
		public Color C0 { get { return c0; } set { c0 = value; repaint = true; } }
		protected Color c1 = Color.White;
		public Color C1 { get { return c1; } set { c1 = value; repaint = true; } }

		unsafe protected void BWFillPoint(byte* ptr, int x, int y, formula f) {
			PointF p = Screen.TransformPointF(new Point(x, y), BMPRect, from);
			Vector res = f(new Vector(p));
			if (!res.IsEmpty) {
				Color color = Thecentury.Interpolation.Interpolate(c0, c1, (double)f(new Vector(p.X, p.Y)));
				ptr[0] = color.B;
				ptr[1] = color.G;
				ptr[2] = color.R;
			}
			else {
				ptr[0] = c0.B;
				ptr[1] = c0.G;
				ptr[2] = c0.R;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ptr"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="f">xy->[0,1],[0,1],[0,1]</param>
		unsafe protected void ColorFillPoint(byte* ptr, int x, int y, formula f) {
			PointF p = Screen.TransformPointF(new Point(x, y), BMPRect, from);
			Vector res = f(new Vector(p));
			if (!res.IsEmpty) {
				Color color = Thecentury.ColorFunctions.MyColor.UncompressTo0_255(res[0], res[1], res[2]);
				ptr[0] = color.B;
				ptr[1] = color.G;
				ptr[2] = color.R;
			}
			else {
				ptr[0] = c0.B;
				ptr[1] = c0.G;
				ptr[2] = c0.R;
			}
		}

		public override void Paint(Graphics g) {
			g.DrawImage(bmp, to.Location);
		}

		protected void UnsafeBWLoadBMP(formula f) {
			bmp = new Bitmap(to.Width, to.Height, PixelFormat.Format24bppRgb);
			BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int stride = bmData.Stride;
			IntPtr Scan0 = bmData.Scan0;
			int nOffset = stride - bmp.Width * 3;
			int width = bmp.Width;

			unsafe {
				byte* p = (byte*)(void*)Scan0;
				for (int y = 0; y < bmp.Height; y++) {
					for (int x = 0; x < width; x++) {
						BWFillPoint(p, x, y, f);
						p += 3;
					}
					p += nOffset;
				}
			}

			bmp.UnlockBits(bmData);
		}

		protected void UnsafeColorLoadBMP(formula f) {
			bmp = new Bitmap(to.Width, to.Height, PixelFormat.Format24bppRgb);
			BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int stride = bmData.Stride;
			IntPtr Scan0 = bmData.Scan0;
			int nOffset = stride - bmp.Width * 3;
			int width = bmp.Width;

			unsafe {
				byte* p = (byte*)(void*)Scan0;
				for (int y = 0; y < bmp.Height; y++) {
					for (int x = 0; x < width; x++) {
						ColorFillPoint(p, x, y, f);
						p += 3;
					}
					p += nOffset;
				}
			}

			bmp.UnlockBits(bmData);
		}
	}
}
