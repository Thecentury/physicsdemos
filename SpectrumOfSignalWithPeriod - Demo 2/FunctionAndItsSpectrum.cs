using System;
using System.Collections.Generic;
using System.Text;

namespace SpectrumOfSignalWithPeriod
{
    class FunctionAndItsSpectrum
    {
        public float A;
        public float T;
        public float tau;
        protected const double eps = 10e-2;
        public float[] Spectrum;
        public float Step;
        public int Nmax;
        public float SpectrumCoef;
        
        protected float SinC(double x)
        {
            if (Math.Abs(x) < eps)
            {
                return 1.0f;
            }
            return (float)(Math.Sin(x) / x);
        }

        public FunctionAndItsSpectrum()
        {
            this.A = 5;
            this.tau = 4;
            this.T = 7;
            this.Nmax = 200;
            this.Spectrum = new float[this.Nmax];
            this.SpectrumCoef = 5.0f;
			this.Step = 1;// (float)(2 * Math.PI / this.T);
            this.CalculateSpectrum();
        }

        public void CalculateSpectrum()
        {
            for (int i = 0; i < this.Nmax; i++)
            {
                Spectrum[i] = Cn(i) * this.SpectrumCoef;
            }
        }
        
        
        public float SignalFunction(float x)
        {
            float x1 = x;
            float result = (float)this.A;
            if (x > 0)
            {
                while (x1 > T / 2.0f )
                {
                    x1 -= T;
                }
            }
            else
            {
                while (x1 < -T / 2.0f)
                {
                    x1 += T;
                }
            }


            if ((x1 > this.tau / 2.0f)||(x1 < -this.tau/2.0f))  return 0;
            else return result;
        }

        public float Cn(int n)
        {
            return (float)(this.A * this.tau / this.T * this.SinC( 2 * Math.PI * this.tau * n / this.T ));
        }
    }
}
