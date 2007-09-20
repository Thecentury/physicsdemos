using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	/// <summary>
	/// This GeneralFunction can be positioned to some n on graph plane, and it will move with it corresponding to coordinates change;
	/// Or it can be positioned directly to the screen, and it will not move while graph plane is moving.
	/// </summary>
	public abstract class GGraphicalObject : PraFunction, IPinnable  {
		protected Color cLine = Color.Black;
		public Color CLine { get { return cLine; } set { cLine = value; repaint = true; } }

		public override void Recomputate(RectangleF where) {
			throw new Exception("The method or operation is not implemented.");
		}

		protected int wLine = 1;

		public int WLine {
			get { return wLine; }
			set { wLine = value; repaint = true; }
		}
		
		protected AutoDisposablePen.AutoDisposablePen pen = new AutoDisposablePen.AutoDisposablePen();

		protected bool isPinned = false;

		public bool IsPinned {
			get { return isPinned; }
			set { isPinned = value; }
		}

		protected abstract void PinnedRecomputate();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="isPinned">If TRUE then figure doesn't move along the screen and its coordinates are taken as coordinates on the screen</param>
		public void Pin(bool isPinned) {
			this.isPinned = isPinned;
			PinnedRecomputate();
		}

		public void Pin() {
			isPinned = !isPinned;
			PinnedRecomputate();
		}

		public static GGraphicalObject PinnedGraphicalObject(GGraphicalObject go) {
			go.Pin();
			return go;
		}
	}
}
