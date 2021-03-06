# GaussNewtonAlgorithm
Exploration of the Gauss Newton Algorithm for least squares fitting of non linear functions. The original goal of this project was to start building out a linear algebra tool box that I could use for various machine learning applications. Upon further reading, I learned that optimizing linear algebra operations, such as matrix multiplication, is a nontrivial pursuit. As such, I will likely end up using something like [Math.NET](https://numerics.mathdotnet.com/) in the future. Be warned, the DMatrix class uses LU decomposition for inverting and determinant calculations, but it implements a naive approach to matrix multiplication. This could lead to very slow computation times for large matrices, so heads up.

Sources:
- [Gauss Newton Algorithm](https://en.wikipedia.org/wiki/Gauss%E2%80%93Newton_algorithm)
- [Python Implementation](https://omyllymaki.medium.com/gauss-newton-algorithm-implementation-from-scratch-55ebe56aac2e)
- [DMatrix Class](https://codereview.stackexchange.com/questions/230515/matrix-class-in-c)
- [LU Decomposition](https://en.wikipedia.org/wiki/LU_decomposition)

To test the algorithm's performance, I decided I would model a sigmoidal function since I have seen use for it's application in my work. A minor goal for this project was to further explore this sigmoidal function. The equation I specifically used is shown below:

<p align="center">
    <img src="https://latex.codecogs.com/svg.latex?f(x)&space;=&space;\frac{A}{1&space;&plus;&space;e^{-B(x&space;-&space;C))})}" title="f(x) = \frac{A}{1 + e^{-B(x - C))})}" />
</p>

The goal was to estimate the coefficients A, B, and C given a set of data. The form of this sigmoid was used since each coefficient has a clear impact on the resulting shape of the "S" curve. "A" controls the peak or extreme value of the sigmoid, "B" controls how quickly the sigmoid approaches the extreme value, and "C" controls the location of the halfway point between zero and A. All of these coefficients are easily translated to real world meaning depending on your application. Drop the function into [Desmos](https://www.desmos.com/calculator) to really visualize the impacts of A, B, and C.

For testing the performance of the algorithm, I made a few utility methods to generate random data to fit my models to. 100 data points were generated using an ideal sigmoid method following the function shown above. Random noise was added to all the x and y values for each data point to better simulate real world data. The following figures illustrate the performance of the algorithm.

![RMSE](GaussNewtonAlgorithm/rmseTrainingFig.png)

Plot of root mean squared error (RMSE) over each iteration of the algorithm.

![IterationZero](GaussNewtonAlgorithm/iterationZeroFig.png)

Plot of the testing data and the initially guessed sigmoid before fitting.

![IterationOne](GaussNewtonAlgorithm/iterationOneFig.png)

Plot of testing data and the fit sigmoid after a single iteration.

![IterationFinal](GaussNewtonAlgorithm/iterationFinalFig.png)

Plot of the final sigmoid after 14 iterations.

I was surprised at how quickly the algorithm converged, although this could be due to the initial guesses I fed the algorithm being heavily influenced by the fact that I knew how the testing data was generated. Overall, convergence was achieved within the first 20 iterations with RMSE values not changing significantly enough between iterations to warrant continuing the algorithm. The coefficients used to generate the noisy data were A = 20, B = 1, and C = 5. The final estimations for A, B, and C were 20.1132, 0.8604, and 4.6855 respectively. Some final notes, this algorithm would likely be faster and more computationally stable if the partial derivatives for each coefficient were calculated analytically. However, I used this approach since it allows for any arbitrary function with an arbitrary number of coefficients.
