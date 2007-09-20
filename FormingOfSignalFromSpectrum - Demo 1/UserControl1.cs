using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Thecentury.Functions;

namespace FormingOfSignalFromSpecter
{
    public partial class UserControl1 : UserControl
    {
        FunctionAndItsSpectrum fas;
        SpectrumFunction sf1;
        Function SignalFunc;
        Thecentury.Functions.Function TempHarmonic; 
        Thecentury.Functions.Function FromSpecterFunc;
        List<Thecentury.Functions.PraFunction> FunctionList = new List<Thecentury.Functions.PraFunction>();
        FunctionAndItsSpectrum fas2;
        int temp;
        bool NeedSum = false;

        public UserControl1()
        {
            InitializeComponent();
            InitializeDemonstration();
        }

        public void InitializeDemonstration()
        {
            this.label4.Text = "Сумма N=0 гармоник";
            this.fas = new FunctionAndItsSpectrum();
            this.fas2 = new FunctionAndItsSpectrum();

            this.Function.Grapher.ClearFunctions();
            this.Function.Grapher.AxisDivision.drawXValues = false;
            this.Function.Grapher.AxisDivision.drawYValues = false;
            this.Function.AllowDrag = Thecentury.AllowedDirections.None;
            this.Function.AllowResize = Thecentury.AllowedDirections.None;
            this.Function.AllowWheelScroll = Thecentury.AllowedDirections.None;
            this.Function.ZoomSource = Thecentury.Source.None;
            this.Function.AllowDrag = Thecentury.AllowedDirections.None;
            this.Function.Grapher.From = new RectangleF(-7, -5, 14, 10);

            this.Spectrum.Grapher.ClearFunctions();
            this.Spectrum.Grapher.AxisDivision.drawXValues = false;
            this.Spectrum.Grapher.AxisDivision.drawYValues = false;
            this.Spectrum.AllowResize = Thecentury.AllowedDirections.None;
			this.Spectrum.ZoomSource = Thecentury.Source.None;
			this.Spectrum.AllowWheelScroll = Thecentury.AllowedDirections.None;
            this.Spectrum.AllowDrag = Thecentury.AllowedDirections.None;
            this.Spectrum.Grapher.From = new RectangleF(-0.5f, -5, 20, 20);

            this.SignalFunc = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas.SignalFunction((float)v)); });
            this.Function.Grapher.AddFunction(SignalFunc);
            this.Function.Grapher.AddFunction(new Thecentury.Functions.NullFunction());

            sf1 = new Thecentury.Functions.SpectrumFunction(fas.Step, fas.Step, fas.Spectrum);

            sf1.Options = DrawOptions.Minus;

            this.Spectrum.Grapher.AddFunction(sf1);
            (this.Spectrum.Grapher.LatestFunction as Thecentury.Functions.SpectrumFunction).WSpectrum = 5;
            (this.Spectrum.Grapher.LatestFunction as GFunction).LMinus = 10;
            (this.Spectrum.Grapher.LatestFunction as GFunction).WMinus = 2;
        }


        private void SetDefault_Click(object sender, EventArgs e)
        {
            InitializeDemonstration();
        }

        private void ChangeHarmonicNumber(int ChangeValue)
        {

            if (ChangeValue < fas.Nmax) fas.N = ChangeValue;
            this.GrapherUpdate();
        }

        private void AddHarmonicButton_Click(object sender, EventArgs e)
        {
            if (this.NeedSum)
            {
                this.fas.N = this.temp;
                if (fas.N < fas.Nmax - 1)
                    if (fas.N == 0) fas.N++;
                    else fas.N += 2;
                this.GrapherUpdate();
                this.NeedSum = !this.NeedSum;
                this.AddHarmonicButton.Text = "Добавить гармонику";
            }
            //    MessageBox.Show("Сперва добавьте в сумму предыдущую гармонику", "Ошибка");
            else
            {
                this.temp = this.fas.N;
                if (fas.N < fas.Nmax - 1)
                    if (fas.N == 0) fas.N++;
                    else fas.N += 2;

                fas2 = new FunctionAndItsSpectrum();
                if (fas.N == 1) fas2.N = 0;
                else fas2.N = fas.N - 2;
                if (fas.N == fas.Nmax - 1) fas2.N = fas.N;

                this.FromSpecterFunc = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas2.SignalFunctionfs((float)v)); });
                this.FromSpecterFunc.GlobalColor = Color.Red;


                this.TempHarmonic = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas.HarmonicTemp((float)v)); });
                TempHarmonic.GlobalColor = Color.Green;
                
                this.FunctionList = new List<Thecentury.Functions.PraFunction>();
                this.FunctionList.Add(this.SignalFunc);
                if (this.FromSpecterFunc != null) this.FunctionList.Add(this.FromSpecterFunc);
                this.FunctionList.Add(this.TempHarmonic);
                if (fas.N != fas2.N)
                {
                    this.Function.Grapher.ClearFunctions();
                    this.Function.Grapher.SetFunctions(this.FunctionList);
                }

                this.NeedSum = !this.NeedSum;
                this.AddHarmonicButton.Text = "Просуммировать";
            }
        }

        private void GrapherUpdate()
        {
            this.label4.Text = "Сумма N=" + fas.N + " гармоник";

            fas2 = new FunctionAndItsSpectrum();
            if (fas.N == 1) fas2.N = 0;
            else fas2.N = fas.N - 2;
            if (fas.N == fas.Nmax - 1) fas2.N = fas.N;

            this.FromSpecterFunc = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas2.SignalFunctionfs((float)v)); });
            this.FromSpecterFunc.GlobalColor = Color.Red;
            this.FromSpecterFunc.AddAnimation(
                    new Thecentury.Animations.Animation("Formula", Thecentury.Animations.Interpolators.Linear, 2, false,
                    new object[] { (formula)delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas2.SignalFunctionfs((float)v)); }, (formula)delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas.SignalFunctionfs((float)v)); } }));
            this.FromSpecterFunc.AddAnimation(new Thecentury.Animations.Animation("CLine", Thecentury.Animations.Interpolators.Linear, 2, false,
                    new object[] { Color.FromArgb(255, Color.Gray), Color.FromArgb(255, Color.Red) }));
            

            this.TempHarmonic = new Function(delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas.HarmonicTemp((float)v)); });
            TempHarmonic.GlobalColor = Color.Green;
            this.TempHarmonic.AddAnimation(
                    new Thecentury.Animations.Animation("Formula", Thecentury.Animations.Interpolators.Linear, 2, false,
                    new object[] { (formula)delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas.HarmonicTemp((float)v)); }, (formula)delegate(Thecentury.Vector v) { return new Thecentury.Vector(fas.SignalFunctionfs((float)v)); } }));
            this.TempHarmonic.AddAnimation(new Thecentury.Animations.Animation("CLine", Thecentury.Animations.Interpolators.Linear, 2, false,
                    new object[] { Color.FromArgb(255, Color.Green), Color.FromArgb(255, Color.Red) }));

            this.FunctionList = new List<Thecentury.Functions.PraFunction>();
            this.FunctionList.Add(SignalFunc);
            this.FunctionList.Add(FromSpecterFunc);
            this.FunctionList.Add(TempHarmonic);
            if (fas.N != fas2.N)
            {
                this.Function.Grapher.ClearFunctions();
                this.Function.Grapher.SetFunctions(this.FunctionList);
            }

            float[] SpectrumArrTemp = new float[fas.N];

            for (int i = 0; i < fas.N; i++)
            {
                SpectrumArrTemp[i] = fas.Spectrum[i];
            }
            Thecentury.Functions.SpectrumFunction sf2 = new Thecentury.Functions.SpectrumFunction(fas.Step, fas.Step, SpectrumArrTemp);
            this.Spectrum.Grapher.ClearFunctions();
            this.Spectrum.Grapher.NewFunction = sf1;
            this.Spectrum.Grapher.NewFunction = sf2;
            sf2.Options = DrawOptions.Point;
            sf2.CSpectrum = Color.Red;
            sf2.WPoint = 10;
            this.Spectrum.Grapher.AddFunction(sf2);
            (this.Spectrum.Grapher.LatestFunction as Thecentury.Functions.SpectrumFunction).WSpectrum = 5;
        }

        //private void MakeSumButton_Click(object sender, EventArgs e)
        //{
        //    if (NeedSum)
        //    {
        //        this.fas.N = this.temp;
        //        if (fas.N < fas.Nmax - 1)
        //            if (fas.N == 0) fas.N++;
        //            else fas.N += 2;
        //        this.GrapherUpdate();
        //        this.NeedSum = !this.NeedSum;
        //    }
        //    else MessageBox.Show("Нет гармоники для добавления\n Нажмите 'Добавить гармонику'", "Ошибка");
        //}

    }
}