using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.Functions;

namespace Thecentury.Animations.Trajectories {
	public class PointTrajectory : GeneralTrajectory {
		protected PointF point;

		public PointTrajectory() { }

		public override bool HasFinished(float currentTime) { return true; }

		public override bool HasBegun(float currentTime) {
			return true;
		}

		public override bool IsWorking(float currentTime) {
			return true;
		}

		public override void Restart(float startTime) { }

		// TODO Написать, что траектории применимы только к GeneralFunction или сделать поиск поля Location по имени
		public override void Apply(float currentTime) { (o as GFunction).Location = point; }

		public override float EndTime {
			get { return 0; }
		}
	}
}
