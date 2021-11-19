using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussNewtonAlgorithm
{
    public class GaussNewtonSolver
    {
        private readonly double rmseTolerance;
        private readonly double iterationTolerance;
        private readonly double step = 10e-6;
        private readonly int maxIterations;

        public Func<double, DMatrix, double> FitFunction { get; private set; }

        public GaussNewtonSolver(Func<double, DMatrix, double> fitFunction, int maxIterations = 1000,
            double rmseTol = 10e-9, double iterTol = 10e-16)
        {
            rmseTolerance = rmseTol;
            iterationTolerance = iterTol;
            FitFunction = fitFunction;
            this.maxIterations = maxIterations;
        }

        public double[] Fit(Data[] data, double[] initGuesses)
        {
            // TODO Implement Algorithm

            DMatrix beta = DMatrix.ColVector(initGuesses);

            throw new NotImplementedException();
        }

        private DMatrix CalcJacobian(Data[] data, DMatrix beta)
        {
            DMatrix Jacobian = new DMatrix(data.Length, beta.Rows);
            DMatrix betaStep = new DMatrix(beta);

            for(int i = 0; i < beta.Rows; i++)
            {
                for(int j = 0; j < beta.Rows; j++)
                {
                    betaStep[j, 0] = beta[j, 0];
                    if(i == j)
                    {
                        betaStep[j, 0] += step;
                    }
                }

                for(int j = 0; j < data.Length; j++)
                {
                    Jacobian[j, i] = CalcGradient(beta, betaStep, data[j].X);
                }
            }

            return Jacobian;
        }

        private double[] CalcResiduals(Data[] data, DMatrix beta)
        {
            if(FitFunction == null)
            {
                throw new Exception("Exception in CalcResiduals: FitFunction was null.");
            }

            double[] ri = new double[data.Length];

            for(int i = 0; i < data.Length; i++)
            {
                ri[i] = data[i].Y - FitFunction.Invoke(data[i].X, beta);
            }

            return ri;
        }

        private double CalcGradient(DMatrix beta, DMatrix betaStep, double x)
        {
            double yZero = FitFunction.Invoke(x, beta);
            double y = FitFunction.Invoke(x, betaStep);
            return (y - yZero) / step;
        }
    }
}
