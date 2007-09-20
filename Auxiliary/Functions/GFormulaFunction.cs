using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.PointTransformation;

namespace Thecentury.Functions {

	public delegate Vector formula(Vector v);

	public static class Formulas {
		public static formula Identity {
			get { return new formula(delegate(Vector v) { return v; }); }
		}
	}

	public abstract class GFormulaFunction : GFunction {

		protected float quality = 1.5f;
		/// <summary>
		/// Расстояние между соседними пикселами, в которых вычисляется точное значение функции.
		/// </summary>
		public float Quality {
			get { return quality; }
			set {
				quality = value;
				repaint = true;
			}
		}

		protected float jumpCoeff = 0.8f;
		/// <summary>
		/// Maximal distance between two points of graph, of Screen.Height
		/// </summary>
		public float JumpCoeff { get { return jumpCoeff; } set { jumpCoeff = value; repaint = true; } }
		protected bool checkJump = false;
		public bool CheckJump { get { return checkJump; } set { checkJump = value; repaint = true; } }

		public virtual void LoadPoints() {
			points = LoadPoints(to, f);
		}

		protected virtual List<List<Point>> LoadPoints(Rectangle to, formula f) {
			repaint = true;
			this.to = to;

			List<List<Point>> pts = new List<List<Point>>();

			Point prevTO;
			Point nowTO;
			PointF prevFROM;
			PointF nowFROM;
			bool wereAdded = true;

			float dx = quality;
			float start;
			float finish;

			start = Screen.TransformX(0.0f, from, to);
			start += (int)(Math.Abs(to.Left - start) / dx + (start > to.Left ? +1 : -1)) * dx * (start > to.Left ? -1 : 1);
			finish = to.Right + dx;

			List<Point> lst = new List<Point>();

			Vector prevVEC = f(new Vector(Screen.TransformX(start, to, from)));
			if (prevVEC.IsEmpty) {
				prevFROM = new PointF(Screen.TransformX(start, to, from), 0);
			}
			else {
				prevFROM = new PointF(Screen.TransformX(start, to, from), (float)f(new Vector(Screen.TransformX(start, to, from))));
			}

			prevTO = Screen.TransformPoint(prevFROM, from, to);

			for (float x = start - dx; x <= finish + dx; x += dx) {
				Vector nowVEC = f(new Vector(Screen.TransformX(x, to, from)));
				if (!nowVEC.IsEmpty) {
					nowFROM = new PointF(Screen.TransformX(x, to, from), (float)f(new Vector(Screen.TransformX(x, to, from))));

					nowFROM = Functions.Location.ApplyLocation(nowFROM);

					nowTO = Screen.TransformPoint(nowFROM, from, to);
					nowTO.Y = Thecentury.Interpolation.CutToBorder(nowTO.Y, -2000, 2000);
					if (Screen.YsAreOnDifferentSidesOfScreen(nowTO.Y, prevTO.Y, ref to) || checkJump & Screen.YsAreOnLongDistance(nowTO.Y, prevTO.Y, ref to, jumpCoeff)) {
						if (lst.Count > 0) {
							pts.Add(lst);
							wereAdded = false;
							lst = new List<Point>();
						}
					}
					else {
						if (!wereAdded) {
							lst.Add(prevTO);
							wereAdded = true;
						}
						lst.Add(nowTO);
					}
					prevTO = nowTO;
					prevFROM = nowFROM;
				}
				else {
					if (lst.Count > 0) {
						pts.Add(lst);
						wereAdded = false;
						lst = new List<Point>();
					}
				}
			}
			if (lst.Count > 0) {
				pts.Add(lst);
			}

			return pts;
		}

		protected List<List<Point>> points;
		public List<List<Point>> Points {
			get { return points; }
		}

		protected formula f;
		public formula Formula {
			get { return f; }
			set { f = value; repaint = true; }
		}


		public override void Paint(Graphics g) {
			GeneralPaint(g, points);
		}
	}
}
