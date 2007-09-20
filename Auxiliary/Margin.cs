using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury {

	public enum MarginType {
		Equal,
		Relative,
		Absolute
	}

	public sealed class Margin {
		MarginType type = MarginType.Equal;

		public MarginType Type {
			get { return type; }
		}

		int up;
		int bottom;
		int left;
		int right;

		float leftR;
		float rightR;
		float upR;
		float bottomR;

		/// <summary>
		/// Absolute margin
		/// </summary>
		/// <param name="up"></param>
		/// <param name="bottom"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		public Margin(int up, int bottom, int left, int right) {
			type = MarginType.Absolute;
			this.up = up;
			this.bottom = bottom;
			this.left = left;
			this.right = right;
		}

		/// <summary>
		/// No margin
		/// </summary>
		public Margin() {
			this.type = MarginType.Equal;
		}

		/// <summary>
		/// Relative margin
		/// </summary>
		/// <param name="left_right"></param>
		/// <param name="up_down"></param>
		public Margin(float left_right, float up_down) {
			type = MarginType.Relative;
			this.leftR = this.rightR = left_right;
			this.upR = this.bottomR = up_down;
		}

		public Margin(float left, float right, float up, float bottom) {
			type = MarginType.Relative;
			this.leftR = left;
			this.rightR = right;
			this.upR = up;
			this.bottomR = bottom;
		}

		public Rectangle Transform(Rectangle from) {
			switch (type) {
				case MarginType.Equal:
					return from;
				case MarginType.Relative:
					return new Rectangle(from.X + (int)(from.Width * leftR), from.Y + (int)(from.Height * upR), (int)(from.Width * (1 - leftR - rightR)), (int)(from.Height * (1 - upR - bottomR)));
				case MarginType.Absolute:
					return new Rectangle(from.X + left, from.Y + up, from.Width - left - right, from.Height - up - bottom);
				default:
					return from;
			}
		}
	}
}
