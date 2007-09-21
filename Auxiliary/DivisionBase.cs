using System;
using System.Collections.Generic;
using System.Collections;

namespace Thecentury {
	public abstract class DivisionBase : IEnumerable {

		#region IEnumerable Members

		public abstract IEnumerator GetEnumerator();

		#endregion
	}
}
