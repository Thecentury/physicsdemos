using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.Diagnostics;

namespace Thecentury.Functions {
	public abstract class DiscreteFunction : GFunction {

		public DiscreteFunction(PointF[] values) {
			this.values = values;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="values">Array of consequent x's and y's of points of TableFunction</param>
		public DiscreteFunction(float[] values) {
			MyDebug.CheckCondition(values.Length % 2 != 0, "Dimension of values must be a multiple of 2!");
			
			this.values = new PointF[values.Length / 2];
			for (int i = 0; i < values.Length - 1; i += 2) {
				this.values[i / 2] = new PointF(values[i], values[i + 1]);
			}
		}

		public DiscreteFunction(float xStart, float xStep, float[] values) { 
			this.values = new PointF[values.Length];
			for (int i = 0; i < values.Length; i++) {
				this.values[i] = new PointF(xStart + i * xStep, values[i]);
			}
		}


		protected List<Point> LoadPoints(Rectangle to, PointF[] points) {
			this.to = to;
			repaint = true;
			List<Point> res = new List<Point>();
			foreach (PointF p in points) {
				res.Add(Screen.TransformPoint(Functions.Location.ApplyLocation(p), from, to));
			}

			return res;
		}

		public void GeneralPaint(Graphics g, List<Point> points) {
			List<List<Point>> pts = new List<List<Point>>();
			pts.Add(points);
			GeneralPaint(g, pts);
		}

		protected PointF[] values;
		protected List<Point> points;
	}
}
