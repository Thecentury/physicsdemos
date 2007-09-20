using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.Functions;

namespace Thecentury.Animations.Trajectories {
	// todo Сделать события OnOccured, OnStarted и OnFinished
	public abstract class GeneralTrajectory : GeneralAnimation {
		public GeneralTrajectory() { }
		public GeneralTrajectory(GFunction f) : base(f) { }
	}
}
