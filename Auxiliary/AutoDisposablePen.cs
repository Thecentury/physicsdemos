using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AutoDisposablePen {
	public sealed class AutoDisposablePen : IDisposable {
		private Pen p = null;
		private bool disposed = false;

		public AutoDisposablePen(Pen pen) {
			p = pen;
		}

		public AutoDisposablePen() {
		}

		public Pen Pen {
			get {
				return p;
			}
			set {
				if (p != null) {
					p.Dispose();
				}
				p = value;
			}
		}

		public static implicit operator Pen(AutoDisposablePen adPen) {
			return adPen.p;
		}

		public static implicit operator AutoDisposablePen(Pen pen) {
			return new AutoDisposablePen(pen);
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {
			if (!this.disposed) {
				if (disposing) {
					p.Dispose();
				}
			}
			disposed = true;
		}

		~AutoDisposablePen() {
			Dispose(false);
		}
	}
}
