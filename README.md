# GaussNewtonAlgorithm
Exploration of the Gauss Newton Algorithm for least squares fitting of non linear functions. The original goal of this project was to start building out a linear algebra tool box that I could use for various machine learning applications. Upon further reading, I learned that optimizing linear algebra operations, such as matrix multiplication, is a nontrivial pursuit. As such, I will likely end up using something like [Math.NET](https://numerics.mathdotnet.com/) in the future. Be warned, the DMatrix class uses LU decomposition for inverting and determinant calculations, but it implements a naive approach to matrix multiplication. This could lead to very slow computation times for large matrices, so heads up.

Sources:
- [Gauss Newton Algorithm](https://en.wikipedia.org/wiki/Gauss%E2%80%93Newton_algorithm)
- [Python Implementation](https://omyllymaki.medium.com/gauss-newton-algorithm-implementation-from-scratch-55ebe56aac2e)
- [DMatrix Class](https://codereview.stackexchange.com/questions/230515/matrix-class-in-c)
- [LU Decomposition](https://en.wikipedia.org/wiki/LU_decomposition)
