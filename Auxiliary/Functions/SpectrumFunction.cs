using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.ColorFunctions;
using Thecentury.Diagnostics;

namespace Thecentury.Functions {
	public class SpectrumFunction : DiscreteFunction {
		protected Color cSpectrum = Color.BlueViolet;
		public Color CSpectrum { get { return cSpectrum; } set { cSpectrum = value; repaint = true; } }
		protected float wSpectrum = 1.0f;
		public float WSpectrum { get { return wSpectrum; } set { wSpectrum = value; repaint = true; } }

		public SpectrumFunction(PointF[] values) : base(values) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="values">Array of consequent x's and y's of points of TableFunction</param>
		public SpectrumFunction(float[] values) : base(values) { }

		public SpectrumFunction(float xStart, float xStep, float[] values) : base(xStart, xStep, values) { }

		public override bool IsRandomColor {
			get {
				return base.IsRandomColor;
			}
			set {
				if (value) {
					cSpectrum = RandomColor.Generate();
					base.IsRandomColor = value;
				}
			}
		}

		public override int GlobalWidth {
			set {
				wSpectrum = value;
				base.GlobalWidth = value;
			}
		}

		public override void Paint(Graphics g) {
			using (pen.Pen = new Pen(cSpectrum, wSpectrum)) {
				foreach (Point p in points) {
					g.DrawLine(pen, p, new Point(p.X, (int)Screen.TransformY(0.0f, from, to)));
				}
			}
			GeneralPaint(g, points);
		}

		public override Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				points = LoadPoints(to, values);
			}
		}
	}
}
