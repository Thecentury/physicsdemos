using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions.GraphicalObjects {
	public class Writing : GGraphicalObject {
		protected Point point2draw;

		protected PointF point;
		public PointF Point {
			get { return point; }
			set { point = value; repaint = true; }
		}

		protected string writing;
		public string WritingString {
			get { return writing; }
			set { writing = value; repaint = true; }
		}

		protected string fontName;
		public string FontName {
			get { return fontName; }
			set { fontName = value; repaint = true; }
		}

		protected float fontSize;
		public float FontSize {
			get { return fontSize; }
			set { fontSize = value; repaint = true; }
		}

		protected Color fontColor;
		public Color FontColor {
			get { return fontColor; }
			set { fontColor = value; repaint = true; }
		}

		public Writing(string writing, PointF point) : this(writing, Color.Black, 10, point) { }

		public Writing(string writing, Color fontColor, float fontSize, PointF point) {
			this.writing = writing;
			this.fontColor = fontColor;
			this.FontSize = fontSize;
			this.point = point;
		}

		protected override void PinnedRecomputate() { this.ForceRepaint(); }

		public override Rectangle To {
			get {
				return to;
			}
			set {
				to = value;
				if (isPinned) {
					point2draw = MyRectangle.Convert(Functions.Location.ApplyLocation(point));
				}
				else {
					point2draw = Screen.TransformPoint(Functions.Location.ApplyLocation(point), from, to);
				}
			}
		}

		public override void Paint(Graphics g) {
			ApplyAnimations();

			using (Font font = new Font(fontName, fontSize)) {
				using (Brush brush = new SolidBrush(fontColor)) {
					g.DrawString(writing, font, brush, point2draw);
				}
			}
		}
	}
}
