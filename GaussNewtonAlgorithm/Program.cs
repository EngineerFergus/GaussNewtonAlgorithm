using System;

namespace GaussNewtonAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, DMatrix, double> sigmoidFunc = Utils.SigmoidFunction;
            DMatrix trueBeta = DMatrix.ColVector(new double[] { 20, 1, 5 });

            Data[] noisyData = Utils.GenerateNoisyData(sigmoidFunc, trueBeta, -3, 20, 0.15, 100);

            GaussNewtonSolver solver = new GaussNewtonSolver(sigmoidFunc, 100);

            DMatrix beta = DMatrix.ColVector(new double[] { 10.0, 0.5, 4.0 });

            DMatrix betaHat = solver.Fit(noisyData, beta);
            Console.WriteLine(solver.TrainingInfo.ToString());

            Console.WriteLine($"");
            Console.WriteLine($"Beta used to make data: {trueBeta}");
            Console.WriteLine($"BetaHat: {betaHat}");
        }
    }
}
