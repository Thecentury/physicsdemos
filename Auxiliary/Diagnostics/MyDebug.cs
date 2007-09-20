using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Diagnostics {
	public static class MyDebug {
		public static void CheckCondition(bool condition) {
			if (!condition) {
				throw new Exception("Condition in Debug.Assert is false!");
			}
		}

		public static void CheckCondition(bool condition, string message) {
			if (!condition) {
				throw new Exception("Debug.Assert: " + message);
			}
		}
	}
}
