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
        private readonly double step = 1e-3;
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

        public DMatrix Fit(Data[] data, DMatrix initGuesses)
        {
            DMatrix beta = new DMatrix(initGuesses);
            double[] residuals = CalcResiduals(data, beta);
            DMatrix rB = DMatrix.ColVector(residuals);
            double rmse = Utils.CalcRMS(residuals);
            bool wasSuccessful = true;
            Console.WriteLine($"Starting RMSE: {rmse:F2}");
            Console.WriteLine($"Starting Beta: {beta}");

            for (int i = 0; i < maxIterations; i++)
            {
                DMatrix J = CalcJacobian(data, beta);
                DMatrix JT = J.Transpose();
                DMatrix bigJ = JT * J;
                (wasSuccessful, bigJ) = bigJ.Invert();

                if (!wasSuccessful)
                {
                    Console.WriteLine("Error in GaussNewtonSolver.Fit: Matrix inversion was not successful.");
                    return beta;
                }

                bigJ *= JT;

                beta -= bigJ * rB;
                residuals = CalcResiduals(data, beta);

                for(int j = 0; j < residuals.Length; j++)
                {
                    rB[j, 0] = residuals[j];
                }

                double temp = rmse;
                rmse = Utils.CalcRMS(residuals);

                if(Math.Abs(temp - rmse) < iterationTolerance)
                {
                    Console.WriteLine($"Convergence to a solution met, change in RMSE smaller than tolerance.");
                    break;
                }

                Console.WriteLine($"Iteration {i + 1} of {maxIterations}... RMSE: {rmse:F2}");
                Console.Write($"Beta estimation: {beta}");

                if (rmse < rmseTolerance)
                {
                    Console.WriteLine($"RMSE tolerance achieved on iteration {i + 1} of {maxIterations}.");
                    Console.Write($"Beta estimation: {beta}");
                    break;
                }
            }

            return beta;
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
            return (yZero - y) / step;
        }
    }
}
