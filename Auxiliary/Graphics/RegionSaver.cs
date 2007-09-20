using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.MyGraphics  {
	public sealed class RegionSaver : IDisposable {
		Graphics g;
		Rectangle rect;
		RectangleF prev;

		public RegionSaver(Graphics g, Rectangle rect) {
			this.g = g;
			this.rect = rect;
			this.prev = g.ClipBounds;
			this.g.Clip = new Region(rect);
		}

		#region IDisposable Members

		public void Dispose() {
			this.g.Clip = new Region(prev);
		}

		#endregion
	}
}
