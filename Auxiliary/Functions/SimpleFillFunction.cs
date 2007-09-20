using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {
	public class SimpleFillFunction : GComplicatedFunction {

		public SimpleFillFunction(GFormulaFunction f1, GFormulaFunction f2) {
			this.f1 = f1;
			this.f2 = f2;
		}

		public override Rectangle To {
			get { return to; }
			set {
				f1.To = value;
				f2.To = value;
				this.to = value;
				repaint = true;
			}
		}

		public override RectangleF From {
			get { return from; }
			set {
				from = value;
				f1.From = value;
				f2.From = value;
			}
		}

		public override void Paint(Graphics g) {
			f1.Paint(g);
			f2.Paint(g);
			Fill(g);
		}

		protected void Fill(Graphics g) {
			ApplyAnimations();
			if (f1.Points.Count == 0) {
				f1.LoadPoints();
			}
			if (f2.Points.Count == 0) {
				f2.LoadPoints();
			}

			Point[] pts1 = f1.Points[0].ToArray();
			Point[] pts2 = f2.Points[0].ToArray();
			Point[] pts = new Point[pts1.Length + pts2.Length];
			Array.Copy(pts1, 0, pts, 0, pts1.Length);
			Array.Reverse(pts2);
			Array.Copy(pts2, 0, pts, pts1.Length, pts2.Length);
			Array.Reverse(pts2);

			if (fillStyle == FillStyle.Fill) {
				Brush brush = new SolidBrush(cFill);
				g.FillPolygon(brush, pts);
				brush.Dispose();
			}
			if (fillStyle == FillStyle.VerticalStrokes) {
				Pen pen = new Pen(cStrokes, wStrokes);

				// todo убрать необходимость одинаковой размерности массивов точек
				if (pts1.Length == pts2.Length) {
					for (int i = 0; i < pts1.Length; i++) {
						g.DrawLine(pen, pts1[i], pts2[i]);
					}
				}
				else {
					throw new Exception("Что-то не так! Различная длина массивов точек!");
				}

				pen.Dispose();
			}
		}
	}
}
