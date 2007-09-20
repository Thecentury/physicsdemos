using System;
using System.Collections.Generic;
using System.Text;

namespace FormingOfSignalFromSpecter
{
    public delegate float fn(float x);
    class FunctionAndItsSpectrum
    {
        public float A;
        public float T;
        public int N;
        public int Nmax;
        public float[] Spectrum;
        public float Step;
        public float SpectrumCoef;

        public FunctionAndItsSpectrum()
        {
            this.A = 5;
            this.T = 5;
            this.N = 0;
            this.Nmax = 20;
            this.Spectrum = new float[this.Nmax];
            this.SpectrumCoef = 8.0f;
            this.Step = (float)(2 * Math.PI / this.T);
            for (int i = 0; i < this.Nmax; i++)
            {
                Spectrum[i] = Cn(i+1) * this.SpectrumCoef;
            }
        }

        public float SignalFunction(float x)
        {
            float x1 = x;
            if (x > 0)
                while (x1 > T / 2)
                {
                    x1 -= T;
                }
            else
                while (x1 < -T / 2)
                {
                    x1 += T;
                }

            float result = (float)this.A / 2.0f;

            if ((x1 > this.T / 4.0f) || (x1 < -this.T / 4.0f)) return -result;
            else return result;
        }

        public float Cn(int N)
        {
            return (float)(A / 2.0f * (Math.Sin(Math.PI * N / 2) - 0.5 * Math.Sin(Math.PI * N)) / (Math.PI * N / 2.0f));
        }

        public float HarmonicTemp(float x)
        {
            return 2.0f * (float)(Cn(this.N) * Math.Cos(2 * Math.PI * this.N * x / this.T));
        }

        public float SignalFunctionfs(float x)
        {
            float sum = 0;
            for (int i = 0; i < (this.N+1)/2.0; i++)
            {
                
                if (i % 2 == 0)
                    sum += (float)(2.0f * this.A / (Math.PI * (2 * i + 1)) * Math.Cos((2 * i + 1) * 2 * Math.PI * x / this.T));
                else
                    sum -= (float)(2.0f * this.A / (Math.PI * (2 * i + 1)) * Math.Cos((2 * i + 1) * 2 * Math.PI * x / this.T));
                
                
                //if (i == 0) sum = 0;
                //else sum += (float)
                  //  (this.A * (Math.Sin(Math.PI * i / 2.0) - Math.Sin(Math.PI * i) / (Math.PI * i * 0.5)) * Math.Cos(2 * Math.PI / this.T * i * x)) / 2.0f;
            }
            return sum;
        }
    }
}
