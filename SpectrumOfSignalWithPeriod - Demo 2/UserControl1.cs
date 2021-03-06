using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Thecentury.Functions;

namespace SpectrumOfSignalWithPeriod {
	public partial class UserControl1 : UserControl {
		private FunctionAndItsSpectrum fas;
		private FunctionAndItsSpectrum fasRep;
		Function SignalFunc;
		Function SignalFuncReper;
		SpectrumFunction sf1;
		bool FirstTime = true;

		public UserControl1() {
			InitializeComponent();
			this.ReperTButton.Enabled = false;
			this.ReperTauButton.Enabled = false;
			this.InitializeDemonstration();
			this.ReperTauButton_Click(null, null);
			this.ReperTButton_Click(null, null);
		}

		private void InitializeDemonstration() {
            this.label2.Text = "Длительность\nимпульса τ";
			this.FunctionReper.Grapher.Margin = new Thecentury.Margin();
			this.Function.Grapher.Margin = new Thecentury.Margin();
			this.Spectrum.Grapher.Margin = new Thecentury.Margin();

			RectangleF functions = new RectangleF(-7, -2, 14, 9);
			RectangleF spectrums = new RectangleF(-0.5f, -5, 20, 20);
			Color reperColor = Color.FromArgb(200, 255, 200);

			this.fas = new FunctionAndItsSpectrum();
			this.fasRep = new FunctionAndItsSpectrum();

            this.PeriodBar.Tracker.Maximum = 24.0f;
            this.PeriodBar.Tracker.Minimum = 1.0f;
            this.PeriodBar.Tracker.StrokesAndNumbers = new Thecentury.FixedAxisDivision(1.0f, 25.0f, 4.0f);
			this.PeriodBar.CurrentValue = fas.T;

            this.DlitelnostBar.Tracker.Maximum = 4.8f;
            this.DlitelnostBar.Tracker.Minimum = 0.2f;
            this.DlitelnostBar.Tracker.StrokesAndNumbers = new Thecentury.FixedAxisDivision(0.4f, 5.2f, 0.8f);
            this.DlitelnostBar.CurrentValue = fas.tau;

			this.Function.Grapher.ClearFunctions();
			this.Function.AllowDrag = Thecentury.AllowedDirections.None;
			this.Function.Grapher.AxisDivision.drawXValues = false;
			this.Function.Grapher.AxisDivision.drawYValues = false;
			this.Function.Grapher.From = functions;
			this.Function.Grapher.DrawBorder = true;
			this.Function.ZoomSource = Thecentury.Source.None;
			this.Function.AllowResize = Thecentury.AllowedDirections.None;
			this.Function.Grapher.AxisDivision.YElems = Thecentury.AxisElements.All;
			this.Function.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("Сигнал", new PointF(5, 5));
			(this.Function.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();

			this.Spectrum.Grapher.ClearFunctions();
			this.Spectrum.AllowDrag = Thecentury.AllowedDirections.None;
			this.Spectrum.Grapher.AxisDivision.drawXValues = false;
			this.Spectrum.Grapher.AxisDivision.drawYValues = false;
			this.Spectrum.Grapher.From = spectrums;
			this.Spectrum.Grapher.DrawBorder = true;
			this.Spectrum.ZoomSource = Thecentury.Source.None;
			this.Spectrum.AllowResize = Thecentury.AllowedDirections.None;
			this.Spectrum.Grapher.AxisDivision.YElems = Thecentury.AxisElements.Axis;
			this.Spectrum.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("Спектр", new PointF(25, 5));
			(this.Spectrum.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();

			this.FunctionReper.Grapher.ClearFunctions();
			this.FunctionReper.AllowDrag = Thecentury.AllowedDirections.None;
			this.FunctionReper.Grapher.AxisDivision.drawXValues = false;
			this.FunctionReper.Grapher.AxisDivision.drawYValues = false;
			this.FunctionReper.Grapher.From = functions;
			this.FunctionReper.Grapher.DrawBorder = true;
			this.FunctionReper.ZoomSource = Thecentury.Source.None;
			this.FunctionReper.AllowResize = Thecentury.AllowedDirections.None;
			this.FunctionReper.Grapher.AxisDivision.YElems = Thecentury.AxisElements.All;
			this.FunctionReper.Grapher.InnerColor = reperColor;
			this.FunctionReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("Реперный сигнал", new PointF(5, 5));
			(this.FunctionReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
			this.FunctionReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("F(t)", Color.Black, 14, new PointF(0.2f, 7));
			this.FunctionReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("t", Color.Black, 14, new PointF(6.6f, 0));

            this.FunctionReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.DoubleArrow(
                new PointF(-fasRep.T / 2.0f, -1), new PointF(fasRep.T / 2.0f, -1), Color.Black, 3, new Thecentury.Drawing.ArrowCap(new SizeF(7,4)), new Thecentury.Drawing.ArrowCap(new SizeF(7,4))); 
            
			this.FunctionReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("T", Color.Black, 14, new PointF(-0f, -0.1f));

            Thecentury.Functions.PraFunction pf = new Thecentury.Functions.GraphicalObjects.Picture(Image.FromFile(Environment.CurrentDirectory + @"\Demonstrations\Formula2.bmp"), new PointF(1, 2));

			this.SpectrumReper.Grapher.ClearFunctions();
			this.SpectrumReper.AllowDrag = Thecentury.AllowedDirections.None;
			this.SpectrumReper.Grapher.AxisDivision = new Thecentury.FloatingAxisDivision(0, 1, 0, 1);
			this.SpectrumReper.Grapher.AxisDivision.drawXValues = true;
			this.SpectrumReper.Grapher.AxisDivision.drawYValues = false;
			this.SpectrumReper.Grapher.From = spectrums;
			this.SpectrumReper.Grapher.DrawBorder = true;
			this.SpectrumReper.ZoomSource = Thecentury.Source.None;
			this.SpectrumReper.AllowResize = Thecentury.AllowedDirections.None;
			this.SpectrumReper.Grapher.InnerColor = reperColor;
			this.SpectrumReper.Grapher.AxisDivision.YElems = Thecentury.AxisElements.Axis;
            this.SpectrumReper.Grapher.NewFunction = pf;
			this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("Реперный спектр", new PointF(105, 5));
			(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.GraphicalObjects.Writing).Pin();
			this.SpectrumReper.Grapher.Margin = new Thecentury.Margin(0, 0, 0, 0.07f);
			this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("ω", Color.Black, 20, new PointF(18.4f, 0));
			this.SpectrumReper.Grapher.NewFunction = new Thecentury.Functions.GraphicalObjects.Writing("S(ω)", Color.Black, 18, new PointF(0.6f, 15f));


			this.SignalFunc = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas.SignalFunction((float)v)); });
			this.Function.Grapher.AddFunction(SignalFunc);

			this.SignalFuncReper = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fasRep.SignalFunction((float)v)); });
			this.FunctionReper.Grapher.AddFunction(SignalFuncReper);

			sf1 = new Thecentury.Functions.SpectrumFunction(0, fasRep.Step, fasRep.Spectrum);

			sf1.Options = DrawOptions.Minus;

			this.Spectrum.Grapher.AddFunction(sf1);
			(this.Spectrum.Grapher.LatestFunction as Thecentury.Functions.SpectrumFunction).WSpectrum = 5;
			(this.Spectrum.Grapher.LatestFunction as GFunction).LMinus = 10;
			(this.Spectrum.Grapher.LatestFunction as GFunction).WMinus = 2;


			sf1 = new Thecentury.Functions.SpectrumFunction(0, fas.Step, fas.Spectrum);

			sf1.Options = DrawOptions.Minus;

			this.SpectrumReper.Grapher.AddFunction(sf1);
			(this.SpectrumReper.Grapher.LatestFunction as Thecentury.Functions.SpectrumFunction).WSpectrum = 5;
			(this.SpectrumReper.Grapher.LatestFunction as GFunction).LMinus = 10;
			(this.SpectrumReper.Grapher.LatestFunction as GFunction).WMinus = 2;

			this.Invalidate();
		}

		private void PeriodBar_ValueChanged() {
			ReperTButton.Enabled = true;

			if (this.PeriodBar.CurrentValue <= this.DlitelnostBar.CurrentValue)
				this.PeriodBar.CurrentValue = this.DlitelnostBar.CurrentValue;
			this.fas.T = this.PeriodBar.CurrentValue;

			this.fas.CalculateSpectrum();
			sf1 = new Thecentury.Functions.SpectrumFunction(0, fas.Step, fas.Spectrum);

			this.AddSpectrum();
		}

		private void DlitelnostBar_ValueChanged() {
			ReperTauButton.Enabled = true;

			if (this.PeriodBar.CurrentValue <= this.DlitelnostBar.CurrentValue)
				this.DlitelnostBar.CurrentValue = this.PeriodBar.CurrentValue;
			this.fas.tau = this.DlitelnostBar.CurrentValue;

			this.fas.CalculateSpectrum();
			sf1 = new Thecentury.Functions.SpectrumFunction(0, fas.Step, fas.Spectrum);

			this.AddSpectrum();
		}

		private void ReperTButton_Click(object sender, EventArgs e) {
			this.ReperTButton.Enabled = false;

			this.fas.T = 7;
			this.PeriodBar.CurrentValue = 7;
			this.fas.CalculateSpectrum();
			sf1 = new Thecentury.Functions.SpectrumFunction(0, fas.Step, fas.Spectrum);
			this.AddSpectrum();
		}

		private void ReperTauButton_Click(object sender, EventArgs e) {
			ReperTauButton.Enabled = false;

			this.fas.tau = 4;
			this.DlitelnostBar.CurrentValue = 4;
			this.fas.CalculateSpectrum();
			sf1 = new Thecentury.Functions.SpectrumFunction(0, fas.Step, fas.Spectrum);
			this.AddSpectrum();
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

	}
}