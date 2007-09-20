using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.PointTransformation;

namespace Thecentury.Functions.VectorFields {
	public abstract class GVectorField : GFunction {
		protected formula f;
		public formula Formula {
			get { return f; }
			set { f = value; repaint = true; }
		}

		protected float qualityX = 20;
		/// <summary>
		/// Шаг по оси Х между точками
		/// </summary>
		public float QualityX {
			get { return qualityX; }
			set { qualityX = value; repaint = true; }
		}

		protected float qualityY = 20;
		/// <summary>
		/// Шаг по оси Y между точками
		/// </summary>
		public float QualityY {
			get { return qualityY; }
			set { qualityY = value; repaint = true; }
		}

		/// <summary>
		/// Заполняет points
		/// </summary>
		/// <param name="to"></param>
		/// <param name="f"></param>
		/// <returns></returns>
		protected abstract void LoadPoints(Rectangle to, formula f);

		public override Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				LoadPoints(to, f);
			}
		}

		public override void Paint(Graphics g) {
			using (pen.Pen = new Pen(cLine, wLine)) {
				foreach (Vector v in points) {
					g.DrawLine(pen, (float)v[0], (float)v[1], (float)v[2], (float)v[3]);
				}
			}
		}

		protected List<Vector> points = new List<Vector>();
	}
}
