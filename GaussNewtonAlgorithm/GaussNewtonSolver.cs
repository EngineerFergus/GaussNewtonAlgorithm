using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussNewtonAlgorithm
{
    public class GaussNewtonSolver
    {
        private double rmseTolerance;
        private double iterationTolerance;
        private int maxIterations;

        public Func<double, double[], double> FitFunction { get; private set; }
        public double[] Coefficients { get; private set; }
        public Data[] Data { get; private set; }

        public GaussNewtonSolver(Func<double, double[], double> fitFunction, Data[] data, double[] initGuesses,
            double rmseTol = 10e-9, double iterTol = 10e-16)
        {
            rmseTolerance = rmseTol;
            iterationTolerance = iterTol;
            FitFunction = fitFunction;
            Data = data;
            Coefficients = initGuesses;
        }

        public void Fit()
        {
            // TODO run algo to fit the function
        }
    }
}
