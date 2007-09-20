using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {

	// todo Привести параметрические функции к единому образцу
	public delegate float function_p(float p);

	public abstract class GParametricFunction : GFormulaFunction {
		protected List<List<Point>> LoadPoints(Rectangle to, function_p x, function_p y) {
			List<List<Point>> pts = new List<List<Point>>();

			float dt = TimeStep;
			PointF pFROM;
			Point pTO;

			List<Point> lst = new List<Point>();


			for (float t = startT; t <= endT; t += dt) {
				pFROM = new PointF(x(t), y(t));

				pTO = Screen.TransformPoint(pFROM, from, to);
				pTO.Y = Thecentury.Interpolation.CutToBorder(pTO.Y, -2000, 2000);
				lst.Add(pTO);
			}
			if (lst.Count > 0) {
				pts.Add(lst);
			}

			return pts;
		}
		
		new public float Quality {
			get { return quality; }
			set { quality = value; }
		}

		public float TimeStep {
			get { return (endT - startT) / quality; }
			set { quality = (endT - startT) / value; }
		}

		public float StartT {
			get { return startT; }
			set { startT = value; }
		}

		public float EndT {
			get { return endT; }
			set { endT = value; }
		}

		protected float startT = 0;
		protected float endT = 1;
		new protected float quality = 100;
	}
}
