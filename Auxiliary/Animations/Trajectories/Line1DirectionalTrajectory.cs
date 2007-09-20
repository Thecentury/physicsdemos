using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.Functions;

namespace Thecentury.Animations.Trajectories {
	// todo убрать GeneralFunctions в конструкторе
	public class Line1DirectionalTrajectory : GeneralTrajectory {
		protected PointF start;
		protected PointF end;
		protected float duration;
		protected float startTime;
		protected bool relativeStart = false;

		public PointF StartPoint { get { return start; } set { start = value; } }
		public PointF EndPoint { get { return end; } set { end = value; } }

		public Line1DirectionalTrajectory() : this(new PointF(0, 0), new PointF(1, 1), 3) { }

		public Line1DirectionalTrajectory(PointF end, float duration)
			: this(new PointF(0, 0), end, duration) {
			relativeStart = true;
		}

		public Line1DirectionalTrajectory(PointF end, float startTime, float duration)
			: this(null, new PointF(0, 0), end, startTime, duration) {
			relativeStart = true;
		}

		public Line1DirectionalTrajectory(PointF start, PointF end, float duration) : this(null, start, end, duration) { }

		public Line1DirectionalTrajectory(PointF start, PointF end, float startTime, float duration) : this(null, start, end, startTime, duration) { }

		public Line1DirectionalTrajectory(GFunction f, PointF start, PointF end, float duration)
			: this(f, start, end, 0, duration) { }

		public Line1DirectionalTrajectory(GFunction f, PointF start, PointF end, float startTime, float duration)
			: base(f) {
			this.startTime = startTime;
			this.start = start;
			this.end = end;
			this.duration = duration;
		}
		
		// TODO Приведение к типу, у которого есть Location
		public override object AppliedToObject {
			get {
				return base.AppliedToObject;
			}
			set {
				base.AppliedToObject = value;
				if (relativeStart) {
					this.start = (o as GFunction).Location;
				}
			}
		}

		public override bool HasBegun(float currentTime) {
			return currentTime >= startTime;
		}

		public override bool IsWorking(float currentTime) {
			return HasBegun(currentTime) && !HasFinished(currentTime);
		}

		public override bool HasFinished(float currentTime) {
			return (startTime + duration) < currentTime;
		}

		public override void Restart(float startTime) {
			this.startTime = startTime;
		}

		public override void Apply(float currentTime) {
			float ratio = (currentTime - startTime) / duration;
			(o as GFunction).Location = Interpolation.Interpolate(start, end, ratio);
		}

		public override float EndTime {
			get { return this.startTime + duration; }
		}
	}
}
