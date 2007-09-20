using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	/// <summary>
	/// This GraphicalObject can be filled inside with some color
	/// </summary>
	public abstract class GGeometricFigure : GGraphicalObject, IFillable {
		protected bool filled = false;
		protected Color fillColor = Color.Orange;

		/// <summary>
		/// Fill with the same color as line is painted with
		/// </summary>
		public void Fill() { fillColor = cLine; filled = true; }
		public void Fill(Color fillColor) { this.fillColor = fillColor; filled = true; }

		public Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}
	}
}
