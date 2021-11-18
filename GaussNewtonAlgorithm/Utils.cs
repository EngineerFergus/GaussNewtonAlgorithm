using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussNewtonAlgorithm
{
    public static class Utils
    {
        public static double SigmoidFunction(double x, DMatrix coefficients)
        {
            if(coefficients.Rows != 3 || coefficients.Cols != 1)
            {
                throw new Exception($"Exeption running SigmoidFunction." +
                    $" Expected Column DMatrix of size 3 x 1, received DMatrix of size {coefficients.Rows} x {coefficients.Cols}");
            }

            double A = coefficients[0, 0], B = coefficients[1, 0], C = coefficients[2, 0];
            return A / (1 + Math.Exp(-B * (x - C)));
        }
    }

    public record Data(double X, double Y);
}
