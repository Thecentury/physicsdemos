using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Thecentury {
	public class FixedAxisDivision : IEnumerable {
		float min = 0;
		float max = 1;
		float step = 0.5f;

		public float Minimum {
			get { return min; }
			set { min = value; }
		}

		public float Maximum {
			get { return max; }
			set { max = value; }
		}

		public float Step {
			get { return step; }
			set { step = value; }
		}

		public float NumberOfTicks {
			get { return (max - min) / step; }
			set { step = (max - min) / value; }
		}

		public FixedAxisDivision() : this(0, 1, 3) { }

		public FixedAxisDivision(float start, float end, int number) : this(start, end, (end - start) / number) { }

		public FixedAxisDivision(float start, float end, float step) {
			this.min = start;
			this.max = end;
			this.step = step;
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator() {
			for (float x = min; x <= max + step * 0.5f; x += step) {
				yield return x;
			}
		}

		#endregion
	}
}
