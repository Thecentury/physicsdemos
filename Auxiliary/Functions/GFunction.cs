using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.PointTransformation;
using Thecentury.ColorFunctions;
using info.lundin.Math;
using System.Collections;
using System.Drawing.Imaging;
using Thecentury.Diagnostics;
using Thecentury.TimeControl;
using Thecentury.Animations;

namespace Thecentury.Functions {

	[Flags]
	public enum DrawOptions {
		Point = 1,
		Line = 2,
		Curve = 4,
		Square = 8,
		Cross = 16,
		Plus = 32,
		Minus = 64,
		FilledSquare = 128,
		Circle = 256,
		/// <summary>
		/// Picture
		/// </summary>
		Texture = 512
	}

	public abstract class GFunction : PraFunction {

		protected Bitmap texture;
		protected Image thumbnail;

		protected PointF location;
		public PointF Location {
			get { return location; }
			set { location = value; repaint = true; }
		}

		protected bool drawFunction = true;
		public bool DrawFunction {
			get { return drawFunction; }
			set { drawFunction = value; repaint = true; }
		}

		protected bool is_random_color = true;
		public virtual bool IsRandomColor {
			get { return is_random_color; }
			set {
				if (value) {
					Color[] colors = RandomColor.GenerateColors(11);
					cLine = colors[0];
					cCurve = colors[1];
					cPoint = colors[2];
					cCross = colors[3];
					cPlus = colors[4];
					cMinus = colors[5];
					cFilledSquare = colors[6];
					cCircle = colors[7];
					cSquare = colors[8];
				}
				is_random_color = value;
				repaint = true;
			}
		}

		public virtual int GlobalWidth {
			set {
				wLine = value;
				wCurve = value;
				wPoint = value;
				wCross = value;
				wPlus = value;
				wMinus = value;
				wFilledSquare = value;
				wCircle = value;
				repaint = true;
			}
		}

		public virtual Color GlobalColor {
			set {
				cLine = value;
				cCurve = value;
				cPoint = value;
				cCross = value;
				cPlus = value;
				cMinus = value;
				cFilledSquare = value;
				cCircle = value;
				repaint = true;
			}
		}

		public Color CPoint { get { return cPoint; } set { cPoint = value; repaint = true; } }
		public Color CCircle { get { return cCircle; } set { cCircle = value; repaint = true; } }
		public Color CCross { get { return cCross; } set { cCross = value; repaint = true; } }
		public Color CCurve { get { return cCurve; } set { cCurve = value; repaint = true; } }
		public Color CFilledSquare { get { return cFilledSquare; } set { cFilledSquare = value; repaint = true; } }
		public Color CLine { get { return cLine; } set { cLine = value; repaint = true; } }
		public Color CMinus { get { return cMinus; } set { cMinus = value; repaint = true; } }
		public Color CPlus { get { return cPlus; } set { cPlus = value; repaint = true; } }
		public Color CSquare { get { return cSquare; } set { cSquare = value; repaint = true; } }

		protected Color cLine = Color.Blue;
		protected Color cCurve = Color.Red;
		protected Color cPoint = Color.Black;
		protected Color cSquare = Color.Black;
		protected Color cCross = Color.Black;
		protected Color cPlus = Color.Black;
		protected Color cMinus = Color.Black;
		protected Color cFilledSquare = Color.Black;
		protected Color cCircle = Color.Black;

		protected Color transparentColor = Color.Green;
		public Color TransparentColorOfTexture {
			get { return transparentColor; }
			set { transparentColor = value; repaint = true; }
		}

		protected int wLine = 2;
		public int WLine { get { return wLine; } set { wLine = value; repaint = true; } }
		protected int wCurve = 1;
		public int WCurve { get { return wCurve; } set { wCurve = value; repaint = true; } }
		protected int wPoint = 5;
		public int WPoint { get { return wPoint; } set { wPoint = value; repaint = true; } }
		protected int wCross = 1;
		public int WCross { get { return wCross; } set { wCross = value; repaint = true; } }
		protected int lCross = 5;
		public int LCross { get { return lCross; } set { lCross = value; repaint = true; } }
		protected int wPlus = 1;
		public int WPlus { get { return wPlus; } set { wPlus = value; repaint = true; } }
		protected int lPlus = 5;
		public int LPlus { get { return lPlus; } set { lPlus = value; repaint = true; } }
		protected int wMinus = 1;
		public int WMinus { get { return wMinus; } set { wMinus = value; repaint = true; } }
		protected int lMinus = 5;
		public int LMinus { get { return lMinus; } set { lMinus = value; repaint = true; } }
		protected int wSquare = 1;
		public int WSquare { get { return wSquare; } set { wSquare = value; repaint = true; } }
		protected int lSquare = 5;
		public int LSquare { get { return lSquare; } set { lSquare = value; repaint = true; } }
		protected int wFilledSquare = 5;
		public int WFilledSquare { get { return wFilledSquare; } set { wFilledSquare = value; repaint = true; } }
		protected int wCircle = 1;
		public int WCircle { get { return wCircle; } set { wCircle = value; repaint = true; } }
		protected int lCircle = 5;
		public int LCircle { get { return lCircle; } set { lCircle = value; repaint = true; } }
		protected int wTexture = 20;
		public int WTexture { get { return wTexture; } set { wTexture = value; repaint = true; } }
		protected int hTexture = 20;
		public int HTexture { get { return hTexture; } set { hTexture = value; repaint = true; } }


		protected List<GeneralTransformation> transformations = new List<GeneralTransformation>();

		protected DrawOptions options = DrawOptions.Line;
		public DrawOptions Options { get { return options; } set { options = value; repaint = true; } }

		public void LoadBitmap(string name, bool unscaled) {
			texture = new Bitmap(name);
			texture.MakeTransparent(transparentColor);
			if (unscaled) {
				thumbnail = new Bitmap(texture);
			}
			else {
				thumbnail = new Bitmap(texture, wTexture, hTexture);
			}
		}

		public void LoadBitmap(string name, int thumbnailWidth, int thumbnailHeight) {
			this.wTexture = thumbnailWidth;
			this.hTexture = thumbnailHeight;
			LoadBitmap(name, false);
		}

		public GeneralTransformation Transformations(int position) {
			return transformations[position];
		}

		public IEnumerable TransformationsList {
			get {
				foreach (GeneralTransformation t in transformations) {
					yield return t;
				}
			}
		}

		public int NumberOfTransformations {
			get { return transformations.Count; }
		}

		//public GeneralAnimation Animations(int position) {
		//    return animations[position];
		//}

		public IEnumerable AnimationsList {
			get {
				foreach (GeneralAnimation a in animations) {
					yield return a;
				}
			}
		}

		public int NumberOfAnimations {
			get { return animations.Count; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="transformation"></param>
		/// <returns>Position of this transformation in list of transformations</returns>
		public int AddTransformation(GeneralTransformation transformation) {
			transformations.Add(transformation);
			transformation.SetFunction(this);
			return transformations.Count - 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="animation"></param>
		/// <returns>Position of this animation in list of animations</returns>
		public int AddAnimation(GeneralAnimation animation) {
			animation.AppliedToObject = this;
			animations.Add(animation);
			return animations.Count - 1;
		}

		public GeneralAnimation NewAnimation {
			set {
				value.AppliedToObject = this;
				animations.Add(value);
			}
		}

		public GeneralAnimation LatestAnimation {
			get { return animations[animations.Count - 1]; }
			set { value.AppliedToObject = this; animations[animations.Count - 1] = value; }
		}

		public void ChangeAnimationToNull(int number) {
			animations[number] = new NullAnimation();
		}

		public void RemoveAnimation(int number) {
			animations.RemoveAt(number);
		}

		protected bool IsSet(DrawOptions opt) {
			return options == (options | opt);
		}

		protected AutoDisposablePen.AutoDisposablePen pen = new AutoDisposablePen.AutoDisposablePen();

		public static GFunction Parse(string st) {
			st = st.ToLower();
			if (st != "") {
				ExpressionParser parser = new ExpressionParser();
				GFunction temp;

				char[] separators = new char[] { '*', '+', '-', '/', '(', ')' };
				bool usesTime = false;

				string[] substrings = st.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in substrings) {
					if (s == "t") {
						usesTime = true;
						break;
					}
				}

				if (usesTime) {
					temp = new AnimatedFunction(delegate(Vector v) {
						Hashtable h = new Hashtable();
						h.Add("x", v[0].ToString());
						h.Add("t", v[1].ToString());
						try {
							return new Vector(parser.Parse(st, h));
						}
						catch {
							throw new Exception("ExpressionParser: Cannot parse this string!");
							//System.Windows.Forms.MessageBox.Show("ExpressionParser: Cannot parse this string!");
						}
					});
				}
				else {
					temp = new Function(delegate(Vector v) {
						Hashtable h = new Hashtable();
						h.Add("x", v[0].ToString());
						try {
							return new Vector(parser.Parse(st, h));
						}
						catch {
							throw new Exception("ExpressionParser: Cannot parse this string!");
							//System.Windows.Forms.MessageBox.Show("ExpressionParser: Cannot parse this string!");
						}
					});
				}
				temp.Name = st;
				return temp;
			}
			else {
				System.Windows.Forms.MessageBox.Show("Cannot parse empty string!");
				return null;
			}
		}


		public void RestartAnimation(int number) {
			animations[number].Restart(animTimer.time);
		}

		public void RestartAnimations() {
			float t = animTimer.time;
			foreach (GeneralAnimation a in animations) {
				a.Restart(t);
			}
		}

		public void GeneralPaint(Graphics g, List<List<Point>> points) {
			if (!drawFunction) {
				return;
			}

			ApplyAnimations();
			using (pen.Pen) {
				Point[] arr;
				foreach (List<Point> lst in points) {
					arr = lst.ToArray();
					if (arr.Length > 1) {
						if (IsSet(DrawOptions.Line)) {
							pen.Pen = new Pen(cLine, wLine);
							g.DrawLines(pen, arr);
						}
						if (IsSet(DrawOptions.Curve)) {
							pen.Pen = new Pen(cCurve, wCurve);
							g.DrawCurve(pen, arr);
						}
					}
					if (IsSet(DrawOptions.Point)) {
						Brush brush = new SolidBrush(cPoint);

						foreach (Point p in arr) {
							g.FillEllipse(brush, new Rectangle(p.X - wPoint, p.Y - wPoint, 2 * wPoint, 2 * wPoint));
						}

						brush.Dispose();
					}
					if (IsSet(DrawOptions.Square)) {
						pen.Pen = new Pen(cSquare, wSquare);
						foreach (Point p in arr) {
							g.DrawRectangle(pen, p.X - lSquare, p.Y - lSquare, lSquare * 2, lSquare * 2);
						}
					}
					if (IsSet(DrawOptions.Cross)) {
						pen.Pen = new Pen(cCross, wCross);
						foreach (Point p in arr) {
							g.DrawLine(pen, p.X - lCross, p.Y - lCross, p.X + lCross, p.Y + lCross);
							g.DrawLine(pen, p.X - lCross, p.Y + lCross, p.X + lCross, p.Y - lCross);
						}
					}
					if (IsSet(DrawOptions.Plus)) {
						pen.Pen = new Pen(cPlus, wPlus);
						foreach (Point p in arr) {
							g.DrawLine(pen, p.X - lPlus, p.Y, p.X + lPlus, p.Y);
							g.DrawLine(pen, p.X, p.Y - lPlus, p.X, p.Y + lPlus);
						}
					}
					if (IsSet(DrawOptions.Minus)) {
						pen.Pen = new Pen(cMinus, wMinus);
						foreach (Point p in arr) {
							g.DrawLine(pen, p.X - lMinus, p.Y, p.X + lMinus, p.Y);
						}
					}
					if (IsSet(DrawOptions.FilledSquare)) {
						Brush brush = new SolidBrush(cFilledSquare);

						foreach (Point p in arr) {
							g.FillRectangle(brush, new Rectangle(p.X - wFilledSquare, p.Y - wFilledSquare, wFilledSquare * 2, wFilledSquare * 2));
						}

						brush.Dispose();
					}
					if (IsSet(DrawOptions.Circle)) {
						pen.Pen = new Pen(cCircle, wCircle);
						foreach (Point p in arr) {
							g.DrawArc(pen, p.X - lCircle, p.Y - lCircle, 2 * lCircle, 2 * lCircle, 0, 360);
						}
					}
					if (IsSet(DrawOptions.Texture)) {
						MyDebug.CheckCondition(thumbnail != null, "Perform LoadTexture() to load texture image!");
						foreach (Point p in arr) {
							g.DrawImage(thumbnail, new Point((int)(p.X - wTexture / 2), (int)(p.Y - hTexture / 2)));
						}
					}
				}
			}
		}

		[Obsolete]
		public override void Recomputate(RectangleF where) {
			throw new Exception("The method or operation is not implemented.");
		}

	//    #region IAnimated Members

	//    [Obsolete]
	//    public List<GeneralAnimation> Animations {
	//        get {
	//            return animations;
	//        }
	//        set {
	//            animations = value;
	//            repaint = true;
	//        }
	//    }

	//    #endregion
	}
}
