# GaussNewtonAlgorithm
Exploration of the Gauss Newton Algorithm for least squares fitting of non linear functions. The original goal of this project was to start building out a linear algebra tool box that I could use for various machine learning applications. Upon further reading, I learned that optimizing linear algebra operations, such as matrix multiplication, is a nontrivial pursuit. As such, I will likely end up using something like [Math.NET](https://numerics.mathdotnet.com/) in the future. Be warned, the DMatrix class uses LU decomposition for inverting and determinant calculations, but it implements a naive approach to matrix multiplication. This could lead to very slow computation times for large matrices, so heads up.

Sources:
- [Gauss Newton Algorithm](https://en.wikipedia.org/wiki/Gauss%E2%80%93Newton_algorithm)
- [Python Implementation](https://omyllymaki.medium.com/gauss-newton-algorithm-implementation-from-scratch-55ebe56aac2e)
- [DMatrix Class](https://codereview.stackexchange.com/questions/230515/matrix-class-in-c)
- [LU Decomposition](https://en.wikipedia.org/wiki/LU_decomposition)

To test the algorithm's performance, I made a utility class for generating some idealized and noisy data. For training I used 100 data points generated using a sigmoidal function. Random noise was added to each x and y value to all the data points to better simulate data that would be gathered in actual applications. Below is the equation for the sigmoidal function:

<p align="center">
    f(x) = A / (1 + exp(-B * (x - C)))
</p>
