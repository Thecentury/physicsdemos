using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Thecentury.MyGraphics {


	public enum FillStyle {
		Solid,
		LinearGradient
	}

	[Obsolete("Пока не использовать! Утечки памяти!", true)]
	public sealed class FillManager {
		private FillStyle fillStyle = FillStyle.LinearGradient;
		public FillStyle FillStyle {
			get { return fillStyle; }
			set { fillStyle = value; }
		}

		private Point point1;
		private Point point2;
		private Color c1;
		private Color c2;

		public FillManager() : this(Color.White) { }

		public FillManager(Color solidFillColor) {
			this.c1 = solidFillColor;
			fillStyle = FillStyle.Solid;
		}

		public FillManager(Point point1, Point point2, Color color1, Color color2) {
			this.c1 = color1;
			this.c2 = color2;
			this.point1 = point1;
			this.point2 = point2;
			this.fillStyle = FillStyle.LinearGradient;
		}

		// todo не хранить точки, а запрашивать их, также можно запрашивать прямоугольник и направление градиента
		public Brush GenerateBrush() {
			switch (fillStyle) {
				case FillStyle.Solid:
					return new SolidBrush(c1);
				case FillStyle.LinearGradient:
					return new LinearGradientBrush(point1, point2, c1, c2);
				default:
					return null;
			}
		}

	}
}
