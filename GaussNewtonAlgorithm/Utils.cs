using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussNewtonAlgorithm
{
    public static class Utils
    {
        public static double SigmoidFunction(double x, double[] coefficients)
        {
            if(coefficients.Length != 3)
            {
                throw new Exception($"Exeption running SigmoidFunction." +
                    $" Expected three coefficient values and received {coefficients.Length}");
            }

            double A = coefficients[0], B = coefficients[1], C = coefficients[2];
            return A / (1 + Math.Exp(-B * (x - C)));
        }
    }

    public record Data(double X, double Y);
}
