using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.ScalarFields {
	public class ColorScalarField : GScalarField {
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="f">xy->r[0,1], g[0,1], b[0,1]</param>
		public ColorScalarField(formula f) {
			this.f = f;
		}

		public override Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				UnsafeColorLoadBMP(f);
			}
		}
	}
}
