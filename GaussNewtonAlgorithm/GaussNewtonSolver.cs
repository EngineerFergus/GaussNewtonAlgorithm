using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussNewtonAlgorithm
{
    public class GaussNewtonSolver
    {
        private Func<double, double[], double> fitFunction;
        private double tolerance;
        private int maxIterations;
        public double[] Coefficients { get; private set; }
    }
}
