using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Functions.GraphicalObjects {
	public interface IPinnable {
		bool IsPinned {
			get;
			set;
		}

		void Pin(bool isPinned);

		void Pin();
	}
}
