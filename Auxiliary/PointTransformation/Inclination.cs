using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace Thecentury.PointTransformation {

	public class Inclination : GeneralTransformation {
		
		protected float deltaX = 0.0f;
		public float DeltaX {
			get {
				return deltaX;
			}
			set {
				if (deltaY != value) {
					deltaX = value;
					RepaintFunctions();
				}
			}
		}

		protected float deltaY = 1.0f;
		public float DeltaY {
			get {
				return deltaY;
			}
			set {
				if (deltaX != value) {
					deltaY = value;
					RepaintFunctions();
				}
			}
		}

		public Inclination(float deltaX, float deltaY) {
			this.deltaX = deltaX;
			this.deltaY = deltaY;
		}
		
		public Inclination() { }

		/// <summary>
		/// Наклон графика под углом 45°
		/// </summary>
		/// <returns></returns>
		public static Inclination Pseudo3DTransformation() {
			return new Inclination((float)Math.Sqrt(0.5), (float)Math.Sqrt(0.5));
		}

		/// <summary>
		/// Наклон графика под произвольным углом
		/// </summary>
		/// <param name="angle">Угол наклона от оси х</param>
		/// <returns></returns>
		public Inclination(double angle) {
			this.deltaX = (float)Math.Cos(angle);
			this.deltaY = (float)Math.Sin(angle);
		}

		public override PointF Apply(PointF p) {
			return new PointF(p.X + p.Y * this.deltaX, p.Y * this.deltaY);
		}
	}
}
