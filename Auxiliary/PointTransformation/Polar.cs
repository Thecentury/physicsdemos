using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.PointTransformation {

	/// <summary>
	/// Takes x coordinate of n as radius, and y coordinate as angle
	/// </summary>
	public class Polar : GeneralTransformation {
		public override PointF Apply(PointF p) {
			return new PointF(p.X * (float)Math.Cos(p.Y), p.X * (float)Math.Sin(p.Y));
		}
	}
}
