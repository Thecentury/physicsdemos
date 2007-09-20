using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.GeometricObject {
	public abstract class GeneralVectorGeometricObject : GeneralGeometricObject {
		protected Color c;
		public Color Color {
			get { return c; }
			set { c = value; }
		}
	}
}
