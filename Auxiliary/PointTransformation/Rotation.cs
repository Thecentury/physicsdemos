using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.PointTransformation {
	public class Rotation : GeneralTransformation {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="center"></param>
		/// <param name="angle">Angle of rotation, in radians</param>
		public Rotation(PointF center, float angle) {
			this.center = center;
			this.angle = angle;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="center"></param>
		/// <param name="angle">Angle of rotation, in degrees</param>
		public Rotation(PointF center, int angle) {
			this.center = center;
			this.angle = (float)(Math.PI / 180 * angle);
		}

		public static float GradToRad(float grad) {
			return (float)(Math.PI / 180 * grad);
		}

		public Rotation() { }

		public float Angle {
			get { return angle; }
			set {
				if (angle != value) {
					angle = value;
					RepaintFunctions();
				}
			}
		}

		public PointF Center {
			get { return center; }
			set {
				if (center!=value) {
					center = value;
					RepaintFunctions();
				}
			}
		}

		public override PointF Apply(PointF p) {
			float x = p.X - center.X;
			float y = p.Y - center.Y;
			float hypot = (float)Math.Sqrt(x * x + y * y);
			float prevAngle;
			
			if (x != 0.0f) {
				prevAngle = (float)Math.Atan(y / x);
				if (x < 0) {
					prevAngle += (float)Math.PI;
				}
			}
			else {
				prevAngle = (float)(y > 0 ? Math.PI * 0.5f : -Math.PI * 0.5f);
			}

			prevAngle += angle;
			return new PointF((float)(center.X + hypot * Math.Cos(prevAngle)), (float)(center.Y + hypot * Math.Sin(prevAngle)));
		}

		protected PointF center = new PointF(0,0);
		protected float angle = 0;
	}
}
