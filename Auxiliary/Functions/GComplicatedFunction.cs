using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {

	public enum FillStyle {
		Fill,
		VerticalStrokes
	}

	public abstract class GComplicatedFunction : GFunction {
		protected FillStyle fillStyle;
		public FillStyle FillStyle {
			get { return fillStyle; }
			set { fillStyle = value; }
		}

		protected GFormulaFunction f1;
		public GFormulaFunction F1 {
			get { return f1; }
			set { f1 = value; repaint = true; }
		}

		protected GFormulaFunction f2;
		public GFormulaFunction F2 {
			get { return f2; }
			set { f2 = value; repaint = true; }
		}

		protected Color cFill = Color.Black;
		public Color CFill {
			get { return cFill; }
			set { cFill = value; repaint = true; }
		}

		protected Color cStrokes = Color.LawnGreen;
		public Color CStrokes {
			get { return cStrokes; }
			set { cStrokes = value; repaint = true; }
		}

		protected int wStrokes = 1;
		public int WStrokes {
			get { return wStrokes; }
			set { wStrokes = value; repaint = true; }
		}
	}
}
