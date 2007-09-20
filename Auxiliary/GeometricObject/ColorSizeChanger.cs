using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.GeometricObject {
	public class ColorSizeChanger : GeneralChanger {

		protected Direction direction;
		public Direction Direction {
			get { return direction; }
			set { direction = value; }
		}


		protected Size zeroSize;
		public Size ZeroSize {
			get { return zeroSize; }
			set { zeroSize = value; }
		}

		protected Size infSize;
		public Size InfSize {
			get { return infSize; }
			set { infSize = value; }
		}

		protected Color zeroColor;
		public Color ZeroColor {
			get { return zeroColor; }
			set { zeroColor = value; }
		}

		protected Color infColor;
		public Color InfColor {
			get { return infColor; }
			set { infColor = value; }
		}

		public ColorSizeChanger(Color zeroColor, Color infColor, Size zeroSize, Size infSize) {
			this.zeroColor = zeroColor;
			this.infColor = infColor;
			this.zeroSize = zeroSize;
			this.infSize = infSize;
		}

		protected const float two_div_pi = 2 / (float)Math.PI;

		public override void Apply(float distance) {
			float ratio = two_div_pi * MyMath.Atan(distance*0.005f);
			Size prevSize = gObject.Size;
			Point prevLoc = gObject.Location;

			(gObject as GeneralVectorGeometricObject).Color = Interpolation.Interpolate(zeroColor, infColor, ratio);
			gObject.Size = Interpolation.Interpolate(zeroSize, infSize, ratio);

			switch (direction) {
				case Direction.ToSouth:
					gObject.Location = new Point(prevLoc.X + (prevSize.Width - gObject.Size.Width) / 2, prevLoc.Y + (prevSize.Height - gObject.Size.Height) / 2);
					break;
				case Direction.ToNorth:
					gObject.Location = new Point(prevLoc.X + (prevSize.Width - gObject.Size.Width) / 2, prevLoc.Y);
					break;
				case Direction.ToEast:
					gObject.Location = new Point(prevLoc.X + (prevSize.Width - gObject.Size.Width) / 2, prevLoc.Y + (prevSize.Height - gObject.Size.Height) / 2);
					break;
				case Direction.ToWest:
					gObject.Location = new Point(prevLoc.X, prevLoc.Y + (prevSize.Height - gObject.Size.Height) / 2);
					break;
				default:
					break;
			}

		}
	}
}
