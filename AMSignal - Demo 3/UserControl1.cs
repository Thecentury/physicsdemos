using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Thecentury.Functions;
using System.Diagnostics;

namespace AmplitydnoModylirovanSignal {
	public partial class UserControl1 : UserControl {
		private FunctionAndItsSpectrum fas;
		private FunctionAndItsSpectrum fasRep;
		private Function SignalFunc;
		private Function SignalFuncReper;
		private SpectrumFunction sf1;
		bool FirstTime = true;

		public UserControl1() {
			InitializeComponent();
			this.improvedTrackBar1.Tracker.Maximum = 1.0f;
			this.improvedTrackBar1.Tracker.Minimum = 0.0f;
			this.improvedTrackBar2.Tracker.Maximum = 3.0f;
			this.improvedTrackBar3.Tracker.Maximum = 9.5f;
			this.improvedTrackBar3.Tracker.Minimum = 4.5f;

			this.improvedTrackBar1.Tracker.StrokesAndNumbers = new Thecentury.FixedAxisDivision(0.0f, 1.0f, 0.2f);

			this.improvedTrackBar2.Tracker.StrokesAndNumbers = new Thecentury.FixedAxisDivision(0.0f, 3.0f, 0.6f);

			this.improvedTrackBar3.Tracker.StrokesAndNumbers = new Thecentury.FixedAxisDivision(4.5f, 9.5f, 1.0f);

			if (!DesignMode) {
				this.InitializeDemonstration();
			}
		}


		private void InitializeDemonstration() {
			if (!DesignMode) {
				this.fas = new FunctionAndItsSpectrum();
				this.fasRep = new FunctionAndItsSpectrum();
				Color reperColor = Color.FromArgb(200, 255, 200);

				this.FunctionReper.Grapher.Margin = new Thecentury.Margin();
				this.Function.Grapher.Margin = new Thecentury.Margin();
				this.Spectrum.Grapher.Margin = new Thecentury.Margin();
				this.SpectrumReper.Grapher.Margin = new Thecentury.Margin();

				this.improvedTrackBar1.CurrentValue = this.fas.m;
				this.improvedTrackBar2.CurrentValue = this.fas.OmegaM;
				this.improvedTrackBar3.CurrentValue = this.fas.OmegaN;

				this.Function.Grapher.ClearFunctions();
				this.Function.AllowDrag = Thecentury.AllowedDirections.X;
				this.Function.Grapher.AxisDivision.drawXValues = false;
				this.Function.Grapher.AxisDivision.drawYValues = false;
				this.Function.Grapher.From = new RectangleF(-14, -14, 28, 28);
				this.Function.Grapher.DrawBorder = true;
				this.Function.Grapher.AxisDivision.YElems = Thecentury.AxisElements.None;
				this.Function.AllowResize = Thecentury.AllowedDirections.None;
				this.Function.ZoomSource = Thecentury.Source.None;
				this.Function.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("Сигнал", new PointF(5, 5));
				(this.Function.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
				//MessageBox.Show((this.Function.Grapher.LatestFunction as Thecentury.Functions.GeneralFormulaFunction).Quality.ToString());
				//(this.Function.Grapher.LatestFunction as Thecentury.Functions.GeneralFormulaFunction).Quality = 1;


				this.Spectrum.Grapher.ClearFunctions();
				this.Spectrum.AllowDrag = Thecentury.AllowedDirections.None;
				this.Spectrum.Grapher.AxisDivision.drawXValues = false;
				this.Spectrum.Grapher.AxisDivision.drawYValues = false;
				this.Spectrum.Grapher.From = new RectangleF(-3f, -1, 6, 7);
				this.Spectrum.Grapher.DrawBorder = true;
				this.Spectrum.Grapher.AxisDivision.YElems = Thecentury.AxisElements.None;
				this.Spectrum.AllowResize = Thecentury.AllowedDirections.None;
				this.Spectrum.ZoomSource = Thecentury.Source.None;
				this.Spectrum.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("Спектр", new PointF(5, 5));
				(this.Spectrum.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();


				this.FunctionReper.Grapher.ClearFunctions();
				this.FunctionReper.AllowDrag = Thecentury.AllowedDirections.None;
				this.FunctionReper.Grapher.AxisDivision.drawXValues = false;
				this.FunctionReper.Grapher.AxisDivision.drawYValues = false;
				this.FunctionReper.Grapher.From = new RectangleF(-14, -14, 28, 28);
				this.FunctionReper.Grapher.DrawBorder = true;
				this.FunctionReper.Grapher.AxisDivision.YElems = Thecentury.AxisElements.None;
				this.FunctionReper.AllowResize = Thecentury.AllowedDirections.None;
				this.FunctionReper.ZoomSource = Thecentury.Source.None;
				this.FunctionReper.Grapher.InnerColor = reperColor;
				this.FunctionReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("Реперный сигнал", new PointF(5, 5));
				(this.FunctionReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();

				//Debug.Listeners.Add(new TextWriterTraceListener("log.log"));
				//Debug.AutoFlush = true;
				//Debug.WriteLine(Environment.CurrentDirectory);

				//Thecentury.Functions.PraFunction pf = new Thecentury.Functions.GraphicalObjects.Picture(Image.FromFile(Environment.CurrentDirectory + @"\Demonstrations\Formula.bmp"), new PointF(-0.5f, 5));
				Thecentury.Functions.PraFunction pf1 = new Thecentury.Functions.GraphicalObjects.Picture(Image.FromFile(Environment.CurrentDirectory + @"\Demonstrations\Formula.bmp"), new PointF(1, 1.65f));
				Thecentury.Functions.PraFunction pf2 = new Thecentury.Functions.GraphicalObjects.Picture(Image.FromFile(Environment.CurrentDirectory + @"\Demonstrations\Formula.bmp"), new PointF(-1, 1.65f));

				this.SpectrumReper.Grapher.ClearFunctions();
				this.SpectrumReper.AllowDrag = Thecentury.AllowedDirections.None;
				this.SpectrumReper.Grapher.AxisDivision.drawXValues = false;
				this.SpectrumReper.Grapher.AxisDivision.drawYValues = false;
				this.SpectrumReper.Grapher.From = new RectangleF(-3f, -1, 6, 7);
				this.SpectrumReper.Grapher.DrawBorder = true;
				this.SpectrumReper.Grapher.AxisDivision.YElems = Thecentury.AxisElements.None;
				this.SpectrumReper.AllowResize = Thecentury.AllowedDirections.None;
				this.SpectrumReper.ZoomSource = Thecentury.Source.None;
				this.SpectrumReper.Grapher.NewFunction = pf1;
				this.SpectrumReper.Grapher.NewFunction = pf2;
				this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("A", new PointF(240, 30));
				(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
				this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("ωn-ωm ", new PointF(130, 220));
				(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
				this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("ωn ", new PointF(220, 220));
				(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
				this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("ωn+ωm ", new PointF(290, 220));
				(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
				this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("Реперный спектр", new PointF(5, 5));
				(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
				this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("ω", Color.Black, 20, new PointF(420, 185));
				(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
				this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("S(ω)", Color.Black, 18, new PointF(200, 0));
				(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
				this.SpectrumReper.Grapher.InnerColor = reperColor;

				this.SignalFunc = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas.SignalFunction((float)v)); });
				this.Function.Grapher.AddFunction(SignalFunc);

				this.SignalFuncReper = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fasRep.SignalFunction((float)v)); });
				this.FunctionReper.Grapher.AddFunction(SignalFuncReper);

				sf1 = new Thecentury.Functions.SpectrumFunction(-fasRep.OmegaM, fasRep.OmegaM, fasRep.Spectrum);

				sf1.Options = DrawOptions.Minus;

				this.Spectrum.Grapher.AddFunction(sf1);
				(this.Spectrum.Grapher.LatestFunction as Thecentury.Functions.SpectrumFunction).WSpectrum = 5;
				(this.Spectrum.Grapher.LatestFunction as GFunction).LMinus = 10;
				(this.Spectrum.Grapher.LatestFunction as GFunction).WMinus = 2;


				sf1 = new Thecentury.Functions.SpectrumFunction(-fas.OmegaM, fas.OmegaM, fas.Spectrum);

				sf1.Options = DrawOptions.Minus;

				this.SpectrumReper.Grapher.AddFunction(sf1);
				(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.SpectrumFunction).WSpectrum = 5;
				(this.SpectrumReper.Grapher.LatestFunction as GFunction).LMinus = 10;
				(this.SpectrumReper.Grapher.LatestFunction as GFunction).WMinus = 2;
			}
		}

		private void AddSpectrum() {
			sf1.Options = DrawOptions.Minus;

			sf1.WSpectrum = 5;
			sf1.LMinus = 10;
			sf1.WMinus = 2;


			if (this.FirstTime) {
				this.Spectrum.Grapher.AddFunction(sf1);
				this.FirstTime = false;
			}
			else this.Spectrum.Grapher.LatestFunction = sf1;
		}

		private void improvedTrackBar1_ValueChanged() {
			this.fas.m = this.improvedTrackBar1.CurrentValue;
			fas.CalculateSpectrum();
			sf1 = new Thecentury.Functions.SpectrumFunction(-fas.OmegaM, fas.OmegaM, fas.Spectrum);
			AddSpectrum();
		}

		private void improvedTrackBar2_ValueChanged() {
			this.fas.OmegaM = improvedTrackBar2.CurrentValue;
			fas.CalculateSpectrum();
			sf1 = new Thecentury.Functions.SpectrumFunction(-fas.OmegaM, fas.OmegaM, fas.Spectrum);
			AddSpectrum();
		}

		private void improvedTrackBar3_ValueChanged() {
			this.fas.OmegaN = this.improvedTrackBar3.CurrentValue;
			this.Spectrum.Grapher.From = new RectangleF(-3f - fas.OmegaN + fasRep.OmegaN, -1, 6, 7);

		}

		private void button1_Click(object sender, EventArgs e) {
			this.improvedTrackBar1.CurrentValue = this.fas.m = this.fasRep.m;
			fas.CalculateSpectrum();
			sf1 = new Thecentury.Functions.SpectrumFunction(-fas.OmegaM, fas.OmegaM, fas.Spectrum);
			AddSpectrum();
		}

		private void button2_Click(object sender, EventArgs e) {
			this.improvedTrackBar2.CurrentValue = this.fas.OmegaM = this.fasRep.OmegaM;
			fas.CalculateSpectrum();
			sf1 = new Thecentury.Functions.SpectrumFunction(-fas.OmegaM, fas.OmegaM, fas.Spectrum);
			AddSpectrum();
		}

		private void button3_Click(object sender, EventArgs e) {
			this.improvedTrackBar3.CurrentValue = this.fas.OmegaN = this.fasRep.OmegaN;
			this.Spectrum.Grapher.From = new RectangleF(-3f, -1, 6, 7);

		}

	}
}