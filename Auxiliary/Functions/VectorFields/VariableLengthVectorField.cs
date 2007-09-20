using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.VectorFields {
	public class VariableLengthVectorField : GVectorField {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="f">xy->al - Угол a измеряется в радианах, l - длина вектора в пикселах</param>
		public VariableLengthVectorField(formula f) {
			this.f = f;
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
					Vector res = f(new Vector(Screen.TransformX(x, to, from), Screen.TransformY(y, to, from)));
					if (!res.IsEmpty) {
						Vector v = new Vector(x, y, x + res[1] * Math.Cos(res[0]), y + res[1] * Math.Sin(res[0]));
						points.Add(v);
					}
				}
			}
		}
	}
}
