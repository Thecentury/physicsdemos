using System;
using System.Collections.Generic;
using System.Text;

namespace AmplitydnoModylirovanSignal
{
    class FunctionAndItsSpectrum
    {
        public float A;
        public float m;
        public float OmegaM;
        public float OmegaN;
        public float[] Spectrum;

        public float SignalFunction(float x)
        {
            return (float)(this.A * (1 + this.m * Math.Cos(this.OmegaM * x)) * Math.Cos(OmegaN * x));
        }

        public void CalculateSpectrum()
        {
            this.Spectrum = new float[3];
            this.Spectrum[0] = this.A * this.m / 2.0f;
            this.Spectrum[1] = this.A;
            this.Spectrum[2] = this.A * this.m / 2.0f;
            
        }

        public FunctionAndItsSpectrum()
        {
            this.A = 5;
            this.m = 0.5f;
            this.OmegaN = 7.0f;
            this.OmegaM = 1.0f;
            this.CalculateSpectrum();
        }

    }
}
