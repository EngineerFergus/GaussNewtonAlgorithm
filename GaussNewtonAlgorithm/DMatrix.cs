using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussNewtonAlgorithm
{
    /// <summary>
    /// Class for linear algebra operations of matrices. Uses type double for all data within a matrix.
    /// LUP decomposition is used for inverting a matrix and for calculating it's determinant.
    /// </summary>
    public class DMatrix
    {
        private readonly double[,] data;
        public int Rows { get; }
        public int Cols { get; }
        public double this[int i, int j]
        {
            get
            {
                return data[i, j];
            }
            private set
            {
                data[i, j] = value;
            }
        }

        public DMatrix(int N) : this(N, N) { }

        public DMatrix(int N, double x) : this(N, N, x) { }

        public DMatrix(int M, int N) : this(M, N, default) { }

        public DMatrix(int M, int N, double x)
        {
            if (M < 1 || N < 1)
            {
                throw new Exception($"Cannot create DMatrix of size {M} x {N}, both M and N must be greater or equal to 1.");
            }

            Rows = M;
            Cols = N;
            data = new double[M, N];

            if(x != default)
            {
                Fill(x);
            }
        }

        public DMatrix(double[,] data)
        {
            Rows = data.GetLength(0);
            Cols = data.GetLength(1);
            this.data = new double[Rows, Cols];

            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Cols; j++)
                {
                    this.data[i, j] = data[i, j];
                }
            }
        }

        public DMatrix(DMatrix A) : this(A.Rows, A.Cols)
        {
            for(int i = 0; i < A.Rows; i++)
            {
                for(int j = 0; j < A.Cols; j++)
                {
                    this.data[i, j] = A[i, j];
                }
            }
        }

        public void Fill(double x)
        {
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Cols; j++)
                {
                    data[i, j] = x;
                }
            }
        }

        public DMatrix Transpose()
        {
            DMatrix TMat = new DMatrix(Cols, Rows);

            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Cols; j++)
                {
                    TMat[j, i] = data[i, j];
                }
            }

            return TMat;
        }

        public double Trace()
        {
            if(Rows != Cols)
            {
                throw new Exception("Cannot compute trace, matrix is not square");
            }

            double trace = 0.0;

            for(int i = 0; i < Rows; i++)
            {
                trace += data[i, i];
            }

            return trace;
        }

        public void SwapRows(int rowOne, int rowTwo)
        {
            if(rowOne < 0 || rowTwo < 0)
            {
                throw new Exception($"Could not SwapRows, desired rows must be greater than zero. rowOne = {rowOne}, rowTwo = {rowTwo}");
            }

            if(rowOne >= Rows || rowTwo >= Rows)
            {
                throw new Exception($"Could not SwapRows, desired rows must be smaller than total rows." +
                    $" rowOne = {rowOne}, rowTwo = {rowTwo}, total Rows = {Rows}");
            }

            for(int i = 0; i < Cols; i++)
            {
                double temp = data[rowOne, i];
                data[rowOne, i] = data[rowTwo, i];
                data[rowTwo, i] = temp;
            }
        }

        public (bool wasSuccessful, DMatrix LU, int[] P, int S) LUPDecompose(double tol = 0.0001)
        {
            if(Rows != Cols)
            {
                throw new Exception("Cannot run LUP Decomposition on non-square matrix.");
            }

            int N = Rows;
            int S = 0;
            int[] P = new int[N];

            for(int i = 0; i < N; i++) { P[i] = i; }

            DMatrix LU = new DMatrix(this);

            double uMax, absA;
            int iMax;

            for(int i = 0; i < N; i++)
            {
                uMax = 0.0;
                iMax = i;

                // find max pivot row
                for (int k = i; k < N; k++)
                {
                    absA = Math.Abs(LU[k, i]);
                    if(absA > uMax)
                    {
                        uMax = absA;
                        iMax = k;
                    }
                }

                // check for degeneracy
                if(uMax < tol)
                {
                    return (false, null, null, -1);
                }

                // pivot if necessary
                if(iMax != i)
                {
                    LU.SwapRows(i, iMax);
                    int temp = P[i];
                    P[i] = P[iMax];
                    P[iMax] = temp;
                    S++;
                }

                for(int j = i + 1; j < N; j++)
                {
                    LU[j, i] = LU[j, i] / LU[i, i];

                    for(int k = i + 1; k < N; k++)
                    {
                        LU[j, k] -= LU[j, i] * LU[i, k];
                    }

                }
            }

            return (true, LU, P, S);
        }

        public (bool wasSuccessful, DMatrix inverse) Invert(double tol = 0.0001)
        {
            (bool success, DMatrix lu, int[] p, int S) = this.LUPDecompose(tol);

            if (!success)
            {
                return (false, null);
            }

            int N = Rows;
            DMatrix IA = new DMatrix(Rows);

            for(int j = 0; j < N; j++)
            {
                for(int i = 0; i < N; i++)
                {
                    IA[i, j] = p[i] == j ? 1.0 : 0.0;

                    for(int k = 0; k < i; k++)
                    {
                        IA[i, j] -= lu[i, k] * IA[k, j];
                    }
                }

                for(int i = N - 1; i >= 0; i--)
                {
                    for(int k = i + 1; k < N; k++)
                    {
                        IA[i, j] -= lu[i, k] * IA[k, j];
                    }

                    IA[i, j] /= lu[i, i];
                }
            }



            return (true, IA);
        }

        public (bool successful, double det) Det(double tol = 0.0001)
        {
            (bool success, DMatrix lu, _, int S) = this.LUPDecompose(tol);

            if (!success)
            {
                return (false, 0.0);
            }

            double det = lu[0, 0];

            for(int i = 1; i < Rows; i++)
            {
                det *= lu[i, i];
            }

            return (true, S % 2 == 0 ? det : -det);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Rows} by {Cols} DMatrix: ");

            StringBuilder line = new StringBuilder();

            for(int i = 0; i < Rows; i++)
            {
                line.Append('{');
                for(int j = 0; j < Cols; j++)
                {
                    line.Append($"{data[i, j]:F2}");
                    if(j < Cols - 1)
                    {
                        line.Append(", ");
                    }
                    else
                    {
                        line.Append(';');
                    }
                }
                line.Append('}');
                sb.AppendLine(line.ToString());
                line.Clear();
            }

            return sb.ToString();
        }

        public static DMatrix Id(int N)
        {
            DMatrix id = new DMatrix(N);

            for(int i = 0; i < N; i++)
            {
                id[i, i] = 1;
            }

            return id;
        }

        public static DMatrix operator -(DMatrix A)
        {
            DMatrix neg = new DMatrix(A.Rows, A.Cols);

            for(int i = 0; i < A.Rows; i++)
            {
                for(int j = 0; j < A.Cols; j++)
                {
                    neg[i, j] = -1 * A[i, j];
                }
            }

            return neg;
        }

        public static DMatrix operator *(DMatrix A, DMatrix B)
        {
            if(A.Cols != B.Rows)
            {
                throw new Exception($"Cannot multiply Matrices, incompatible sizes A.cols = {A.Cols}, B.rows = {B.Rows}");
            }

            DMatrix C = new DMatrix(A.Rows, B.Cols);

            for(int i = 0; i < A.Rows; i++)
            {
                for(int j = 0; j < B.Cols; j++)
                {
                    for(int k = 0; k < A.Cols; k++)
                    {
                        C.data[i, j] += A.data[i, k] * B.data[k, j];
                    }
                }
            }

            return C;
        }

        public static DMatrix operator +(DMatrix A, DMatrix B)
        {
            if (A.Rows != B.Rows || A.Cols != B.Cols)
            {
                throw new Exception("Cannot add matrices, incompatible dimensions");
            }

            DMatrix C = new DMatrix(A.Rows, A.Cols);

            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Cols; j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }

            return C;
        }

        public static DMatrix operator -(DMatrix A, DMatrix B)
        {
            return (A + (-B));
        }

        public static DMatrix operator +(DMatrix A, double c)
        {
            DMatrix res = new DMatrix(A);

            for(int i = 0; i < res.Rows; i++)
            {
                for(int j = 0; j < res.Cols; j++)
                {
                    res.data[i, j] += c;
                }
            }

            return res;
        }
    }
}
