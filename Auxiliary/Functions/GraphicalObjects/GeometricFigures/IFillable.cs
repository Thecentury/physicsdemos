using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	public interface IFillable {
		void Fill();
		void Fill(Color fillColor);
		Color FillColor {
			get;
			set;
		}
	}
}
