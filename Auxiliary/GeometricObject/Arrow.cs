using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.GeometricObject {

	public enum Direction {
		ToSouth,
		ToNorth,
		ToEast,
		ToWest
	}

	public class Arrow : GeneralVectorGeometricObject {
		protected Direction direction;
		public Direction Direction {
			get { return direction; }
			set { direction = value; }
		}

		protected float wLine = 3;
		public float WLine {
			get { return wLine; }
			set { wLine = value; }
		}

		protected float ratio = 0.6f;
		public float LineRatio {
			get { return ratio; }
			set { ratio = value; }
		}

		public Arrow(Direction direction) {
			this.direction = direction;
		}

		public override void Paint(Graphics g) {
		}

		public override void Paint(Graphics g, float distanceToMouseCursor) {
			ApplyChanger(distanceToMouseCursor);
			pen.Pen = new Pen(c, wLine);

			switch (direction) {
				case Direction.ToSouth:
					g.DrawLine(pen, Location.X + 0.5f * size.Width, Location.Y, Location.X + 0.5f * size.Width, Location.Y + ratio * size.Height);
					MyGraphics.MyGraphics.DrawFilledPath(g, c,
						new PointF(Location.X, Location.Y + ratio * size.Height),
						new PointF(Bounds.Right, Location.Y + ratio * size.Height),
						new PointF(Location.X + size.Width / 2, Bounds.Bottom));
					break;
				case Direction.ToNorth:
					// TODO стрелка на север
					break;
				case Direction.ToEast:
					// TODO стрелка на восток
					break;
				case Direction.ToWest:
					// TODO стрелка на запад
					break;
				default:
					break;
			}

			pen.Dispose();
		}
	}
}
