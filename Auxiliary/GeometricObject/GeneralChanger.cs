using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.GeometricObject {
	public abstract class GeneralChanger {
		protected GeneralGeometricObject gObject;

		public abstract void Apply(float distance);

		public virtual GeneralGeometricObject AssignedObject {
			get { return gObject; }
			set { gObject = value; }
		}
	}
}
