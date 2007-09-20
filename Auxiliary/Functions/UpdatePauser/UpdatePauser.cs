using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Functions {
	public sealed class UpdatePauser : IDisposable {
		PraFunction f;

		public UpdatePauser(PraFunction f) {
			this.f = f;
			f.StopUpdating();
		}

		#region IDisposable Members

		public void Dispose() {
			f.PerformUpdate();
		}

		#endregion
	}
}
