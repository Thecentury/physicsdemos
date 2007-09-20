using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Thecentury.Functions.ScalarFields {
	public class BWScalarField : GScalarField {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="f">ij->[0,1]</param>
		public BWScalarField(formula f) {
			this.f = f;
		}

		public override Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				UnsafeBWLoadBMP(f);
			}
		}
	}
}
