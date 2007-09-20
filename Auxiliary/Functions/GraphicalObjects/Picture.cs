using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	// todo разобраться с прозрачностью
	public class Picture : GGraphicalObject {
		protected int wTexture = 20;
		public int WTexture { get { return wTexture; } set { wTexture = value; repaint = true; } }

		protected int hTexture = 20;
		public int HTexture { get { return hTexture; } set { hTexture = value; repaint = true; } }

		protected Color transparentColor = Color.White;
		public Color TransparentColorOfTexture {
			get { return transparentColor; }
			set {
				transparentColor = value; repaint = true;
				texture.MakeTransparent(value);
			}
		}

		protected Bitmap texture;
		protected Bitmap thumbnail;

		protected PointF point;
		public PointF Edge {
			get { return point; }
			set { point = value; repaint = true; }
		}
		public PointF Center {
			get { return new PointF(point.X + 0.5f * MyRectangle.xRatio(to, from) * wTexture, point.Y + 0.5f * MyRectangle.yRatio(to, from) * hTexture); }
			set {
				this.point = new PointF(value.X - 0.5f * MyRectangle.xRatio(to, from) * wTexture, value.Y - 0.5f * MyRectangle.yRatio(to, from) * hTexture);
				repaint = true;
			}
		}

		protected Point p;
		public float centerX { get { return Center.X; } set { Center = new PointF(value, Center.Y); repaint = true; } }
		public float centerY { get { return Center.Y; } set { Center = new PointF(Center.X, value); repaint = true; } }

		public Picture(string name, PointF center) : this( name, center, true, 0, 0) { }
		public Picture(string name, float centerX, float centerY) : this(name, new PointF (centerX , centerY ), true, 0, 0) { }
		public Picture(string name, PointF center, int thumbnailWidth, int thumbnailHeight) : this( name, center, false, thumbnailWidth, thumbnailHeight) { }
		public Picture(string name, float centerX, float centerY, int thumbnailWidth, int thumbnailHeight) : this(name, new PointF (centerX , centerY ), false, thumbnailWidth, thumbnailHeight) { }
		public Picture(string name, PointF center, bool unscaled, int thumbnailWidth, int thumbnailHeight) {
			texture = new Bitmap(name);
			//texture.MakeTransparent(transparentColor);
			texture.MakeTransparent();

			this.wTexture = thumbnailWidth;
			this.hTexture = thumbnailHeight;
			//this.Center = center;
			this.Edge = center;


			if (unscaled) {
				thumbnail = new Bitmap(texture);
			}
			else {
				thumbnail = new Bitmap(texture, wTexture, hTexture);
			}
		}

		public Picture(Image image, PointF center) {
			texture = new Bitmap(image);
			texture.MakeTransparent();
			this.Edge = center;
			thumbnail = new Bitmap(texture);
			wTexture = thumbnail.Width;
			hTexture = thumbnail.Height;
		}

		protected override void PinnedRecomputate() { this.ForceRepaint(); }

		public override Rectangle To {
			get { return to; }
			set {
				this.to = value;
				if (isPinned) {
					p = MyRectangle.Convert(Functions.Location.ApplyLocation(point));
				}
				else {
					p = Screen.TransformPoint(Functions.Location.ApplyLocation(point), from, to);
				}
			}
		}

		public override void Paint(Graphics g) {
			ApplyAnimations();
			g.DrawImage(thumbnail, new Point((int)(p.X - wTexture / 2), (int)(p.Y - hTexture / 2)));
		}
	}
}
