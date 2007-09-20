using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.Functions;

namespace Thecentury.PointTransformation {
	public abstract class GeneralTransformation {
		protected GFunction f = null;
		public abstract PointF Apply(PointF p);

		public List<PointF> Apply(List<PointF> points) {
			List<PointF> res = new List<PointF>();
			foreach (PointF p in points) {
				res.Add(Apply(p));
			}
			return res;
		}

		public virtual void SetFunction(GFunction function) {
			f = function;
		}

		public virtual GFunction Function {
			get { return f; }
			set { f = value; }
		}

		protected void RepaintFunctions() {
			f.ForceRepaint();
		}
	}
}
