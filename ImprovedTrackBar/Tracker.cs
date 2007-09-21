using System;
using System.Collections.Generic;
using System.Text;
using Thecentury;

namespace Thecentury.ImprovedTrackBar {
	public sealed class Tracker {
		FixedAxisDivision strokes = new FixedAxisDivision(0, 10, 10);
		public FixedAxisDivision Strokes {
			get { return strokes; }
			set { strokes = value; }
		}

		FixedAxisDivision numbers = new FixedAxisDivision(0, 10, 5);
		public FixedAxisDivision Numbers {
			get { return numbers; }
			set { numbers = value; }
		}

		public FixedAxisDivision StrokesAndNumbers {
			set { numbers = value; strokes = value; }
		}

		float[] holes;
		float[] holeWidths;
		float currVal;
		bool useHoles = false;
		float minChange = 0;
		public float MinChange {
			get { return minChange; }
			set { minChange = value; }
		}

		public bool UseHoles {
			get { return useHoles; }
			set { useHoles = value; }
		}

		public float Minimum {
			get { return strokes.Minimum; }
			set { strokes.Minimum = value; }
		}

		public float Maximum {
			get { return strokes.Maximum; }
			set { strokes.Maximum = value; }
		}

		public float Length {
			get { return Maximum - Minimum; }
		}

		public float TickFrequency {
			get { return strokes.Step; }
			set { strokes.Step = value; }
		}

		public float NumberOfTicks {
			get { return strokes.NumberOfTicks; }
			set { strokes.NumberOfTicks = value; }
		}

		public float[] Holes {
			get {
				if (useHoles) { return holes; }
				else { return new float[0]; }
			}
			set { holes = value; }
		}

		public float[] HoleWidths {
			get { return holeWidths; }
			set { holeWidths = value; }
		}

		internal float CurrentValue {
			get { return currVal; }
			set {
				if (Math.Abs(currVal - value) < minChange) {
					if (value > currVal) {
						value = currVal + minChange;
					}
					else {
						value = currVal - minChange;
					}
				}

				if (Minimum <= value && value <= Maximum) {
					currVal = ApplyHoles(value);
				}
				else if (value < Minimum) {
					currVal = ApplyHoles(Minimum);
				}
				else if (value > Maximum) {
					currVal = ApplyHoles(Maximum);
				}
			}
		}

		void ApplyHoles() {
			if (useHoles) {
				for (int i = 0; i < holes.Length; i++) {
					if (Math.Abs(currVal - holes[i]) < holeWidths[i]) {
						if (Minimum <= holes[i] && holes[i] <= Maximum) {
							currVal = holes[i];
							break;
						}
					}
				}
			}
		}

		float ApplyHoles(float value) {
			if (useHoles) {
				for (int i = 0; i < holes.Length; i++) {
					if (Math.Abs(value - holes[i]) < holeWidths[i]) {
						if (Minimum <= holes[i] && holes[i] <= Maximum) {
							return holes[i];
						}
					}
				}
			}
			return value;
		}
	}
}
