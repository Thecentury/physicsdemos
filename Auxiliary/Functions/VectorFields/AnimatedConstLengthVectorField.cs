using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.VectorFields {
	public class AnimatedConstLengthVectorField : GAnimatedVectorField {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="f">xyt->a - Угол a измеряется в радианах</param>
		public AnimatedConstLengthVectorField(formula f) {
			this.f = f;
		}

		protected float length = 10;
		public float Length {
			get { return length; }
			set { length = value; }
		}

		/// <summary>
		/// Заполняет points
		/// </summary>
		/// <param name="to"></param>
		/// <param name="f"></param>
		/// <returns></returns>
		protected override void LoadPoints(Rectangle to, formula f) {
			points.Clear();
			repaint = true;
			this.to = to;

			float t = time;

			float dx = qualityX;
			float dy = qualityY;

			float startX = Screen.TransformX(0, from, to);
			startX += (int)(Math.Abs(to.Left - startX) / dx + (startX > to.Left ? +1 : -1)) * dx * (startX > to.Left ? -1 : 1);
			float finishX = to.Right + dx;

			float startY = Screen.TransformY(0, from, to);
			startY += (int)(Math.Abs(to.Top - startY) / dx + (startY > to.Top ? +1 : -1)) * dx * (startY > to.Top ? -1 : 1);
			float finishY = to.Bottom + dx;

			for (float x = startX - dx; x <= finishX + dx; x += dx) {
				for (float y = startY - dy; y <= finishY + dy; y += dx) {
					Vector res = f(new Vector(Screen.TransformX(x, to, from), Screen.TransformY(y, to, from), t));
					if (!res.IsEmpty) {
						Vector v = new Vector(x, y, x + length * Math.Cos(res[0]), y + length * Math.Sin(res[0]));
						points.Add(v);
					}
				}
			}
		}

	}
}
