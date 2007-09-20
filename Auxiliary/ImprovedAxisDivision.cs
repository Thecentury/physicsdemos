using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury {
	public class ImprovedAxisDivision {

	}

	public class PartialAxisDivision {
		// TODO Properties
		float xstart = 0.0f;
		public float XStart {
			get { return xstart; }
			set { xstart = value; }
		}

		float xstep = 1.0f;
		float ystart = 0.0f;
		float ystep = 1.0f;
		float[] xDivision;
		float[] yDivision;
		float[] viewXDivision;
		float[] viewYDivision;

		/// <summary>
		/// Level == 0 - primary division
		/// </summary>
		int level = 0;
		public int Level {
			get { return level; }
			set { level = value; }
		}

		Size markSize = new Size(0, 10);

		AutoDisposablePen.AutoDisposablePen pen = new AutoDisposablePen.AutoDisposablePen();

		RectangleF from;
		Rectangle to;

		protected Color cGrid = Color.LightGray;
		public Color CGrid { get { return cGrid; } set { cGrid = value; } }
		protected Color cText = Color.Black;
		public Color CText { get { return cText; } set { cText = value; } }

		protected int wGrid = 1;
		public int WGrid { get { return wGrid; } set { wGrid = value; } }

		public bool drawXGrid = true;
		public bool drawYGrid = true;

		public RectangleF From {
			get {
				return from;
			}
			set {
				from = value;
				float first = xstart + xstep * (int)((from.Left - xstart) / xstep);
				int number = 1 + (int)((from.Right - first) / xstep);
				xDivision = new float[number];
				for (int i = 0; i < number; i++) {
					xDivision[i] = first + i * xstep;
				}

				first = ystart + ystep * (int)((from.Top - ystart) / ystep);
				number = 1 + (int)((from.Bottom - first) / ystep);
				yDivision = new float[number];
				for (int i = 0; i < number; i++) {
					yDivision[i] = first + i * ystep;
				}
			}
		}

		public Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				viewXDivision = new float[xDivision.Length];
				viewYDivision = new float[yDivision.Length];

				for (int i = 0; i < viewXDivision.Length; i++) {
					viewXDivision[i] = Screen.TransformX(xDivision[i], from, to);
				}
				for (int i = 0; i < viewYDivision.Length; i++) {
					viewYDivision[i] = Screen.TransformY(yDivision[i], from, to);
				}
			}
		}

		public void Paint(Graphics g) { 

		}

	}
}
