using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussNewtonAlgorithm
{
    public static class Utils
    {
        private static int seed;
        private static Random rng;

        public static void Seed(int s)
        {
            seed = s;
            rng = new Random(seed);
        }

        static Utils()
        {
            seed = (int)DateTime.Now.Ticks;
            rng = new Random(seed);
        }

        public static double Rand(double max)
        {
            return rng.NextDouble() * max;
        }

        public static double Rand(double min, double max)
        {
            return (rng.NextDouble() * (max - min)) + min;
        }

        public static double SigmoidFunction(double x, DMatrix coefficients)
        {
            if (coefficients.Rows != 3 || coefficients.Cols != 1)
            {
                throw new Exception($"Exeption running SigmoidFunction." +
                    $" Expected Column DMatrix of size 3 x 1, received DMatrix of size {coefficients.Rows} x {coefficients.Cols}");
            }

            double A = coefficients[0, 0], B = coefficients[1, 0], C = coefficients[2, 0];
            return A / (1 + Math.Exp(-B * (x - C)));
        }

        public static double CalcRMS(double[] x)
        {
            double rms = 0;

            foreach (double xi in x)
            {
                rms += xi * xi;
            }

            rms /= x.Length;
            return Math.Sqrt(rms);
        }

        public static Data[] GenerateData(Func<double, DMatrix, double> function, DMatrix coefficients,
            double xMin, double xMax, int numSamples)
        {
            Data[] data = new Data[numSamples];

            for(int i = 0; i < numSamples; i++)
            {
                double xi = Rand(xMin, xMax);
                data[i] = new Data(xi, function.Invoke(xi, coefficients));
            }

            return data;
        }

        public static Data[] GenerateNoisyData(Func<double, DMatrix, double> function, DMatrix coefficients,
            double xMin, double xMax, double noiseAmplitude, int numSamples)
        {
            Data[] data = new Data[numSamples];

            for(int i = 0; i < numSamples; i++)
            {
                double xi = Rand(xMin, xMax);
                double yi = function.Invoke(xi, coefficients);
                xi += Rand(-noiseAmplitude, noiseAmplitude);
                yi += Rand(-noiseAmplitude, noiseAmplitude);
                data[i] = new Data(xi, yi);
            }

            return data;
        }
    }

    public record Data(double X, double Y);
}
