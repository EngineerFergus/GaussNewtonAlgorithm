using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace GaussNewtonAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, DMatrix, double> sigmoidFunc = Utils.SigmoidFunction;
            DMatrix trueBeta = DMatrix.ColVector(new double[] { 20, 1, 5 });

            Data[] noisyData = Utils.GenerateNoisyData(sigmoidFunc, trueBeta, -3, 20, 1.5, 100);

            GaussNewtonSolver solver = new GaussNewtonSolver(sigmoidFunc, 100);

            DMatrix beta = DMatrix.ColVector(new double[] { 10.0, 0.5, 4.0 });

            DMatrix betaHat = solver.Fit(noisyData, beta);
            Console.WriteLine(solver.TrainingInfo.ToString());

            Console.WriteLine($"");
            Console.WriteLine($"Beta used to make data: {trueBeta}");
            Console.WriteLine($"BetaHat: {betaHat}");
            WriteData(solver, noisyData, "SigmoidFit");

            foreach(Data d in noisyData)
            {
                Console.WriteLine($"{d.X:F4},{d.Y:F4}");
            }
        }

        public static void WriteData(GaussNewtonSolver solver, Data[] data, string experimentName)
        {
            //string directory = $"DIRECTORY GOES HERE";
            //string dataDir = $"{directory}{experimentName}_data.csv";
            //string fitDir = $"{directory}{experimentName}_trainingSummary.csv";
            //
            //StringBuilder d = new StringBuilder();
            //
            //foreach(Data x in data)
            //{
            //    d.AppendLine($"{x.X},{x.Y}");
            //}
            //
            //File.AppendAllText(dataDir, d.ToString());
            //File.AppendAllText(fitDir, solver.TrainingInfo.ToString());
        }
    }
}
