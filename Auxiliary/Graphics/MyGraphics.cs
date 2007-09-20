using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.MyGraphics {
	public static class MyGraphics {

		/// <summary>
		/// Draws and fills closed figure - you should not dublicate start n!
		/// </summary>
		/// <param name="g"></param>
		/// <param name="c"></param>
		/// <param name="points"></param>
		public static void DrawFilledPath(Graphics g, Color c, params PointF[] points) {
			PointF[] path = new PointF[points.Length + 1];
			for (int i = 0; i < points.Length; i++) {
				path[i] = points[i];
			}
			path[points.Length] = path[0];

			byte[] types = new byte[points.Length + 1];
			types[0] = (byte)System.Drawing.Drawing2D.PathPointType.Start;

			for (int i = 1; i < types.Length; i++) {
				types[i] = (byte)System.Drawing.Drawing2D.PathPointType.Line;
			}
			Pen pen = new Pen(c, 1);
			Brush brush = new SolidBrush(c);

			g.DrawLines(pen, path);
			g.FillPath(brush, new System.Drawing.Drawing2D.GraphicsPath(path, types));

			pen.Dispose();
			brush.Dispose();
		}
	}
}
