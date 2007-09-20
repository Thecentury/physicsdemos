using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	public class Square : Rect {
		public Square() : this(-1, -1, 2) { }
		public Square(float centerX, float centerY, float side) : base(centerX, centerY, side, side) { }

		public float Side {
			get { return this.rect.Height; }
			set {
				this.rect.Height = value;
				this.rect.Width = value;
				repaint = true;
			}
		}
	}
}
