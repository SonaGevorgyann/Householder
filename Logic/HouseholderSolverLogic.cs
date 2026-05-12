using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholderSolver.Logic
{
    public static class HouseholderSolverLogic
    {
        public static double[] Solve(double[][] A, double[] b)
        {
            int n = A.Length;

            double[][] R = A.Select(row => row.ToArray()).ToArray();

            double[] y = b.ToArray();

            for (int k = 0; k < n - 1; k++)
            {
                double norm = 0;

                for (int i = k; i < n; i++)
                    norm += R[i][k] * R[i][k];

                norm = Math.Sqrt(norm);

                if (norm == 0)
                    continue;

                double[] v = new double[n];

                for (int i = k; i < n; i++)
                    v[i] = R[i][k];

                v[k] += (v[k] >= 0) ? norm : -norm;

                double vNorm = 0;

                for (int i = k; i < n; i++)
                    vNorm += v[i] * v[i];

                vNorm = Math.Sqrt(vNorm);

                if (vNorm == 0)
                    continue;

                for (int i = k; i < n; i++)
                    v[i] /= vNorm;

                // Apply to R
                for (int j = k; j < n; j++)
                {
                    double dot = 0;

                    for (int i = k; i < n; i++)
                        dot += v[i] * R[i][j];

                    for (int i = k; i < n; i++)
                        R[i][j] -= 2 * v[i] * dot;
                }

                // Apply to y
                double dotB = 0;

                for (int i = k; i < n; i++)
                    dotB += v[i] * y[i];

                for (int i = k; i < n; i++)
                    y[i] -= 2 * v[i] * dotB;
            }

            // Back substitution
            double[] x = new double[n];

            for (int i = n - 1; i >= 0; i--)
            {
                double sum = y[i];

                for (int j = i + 1; j < n; j++)
                    sum -= R[i][j] * x[j];

                if (Math.Abs(R[i][i]) < 1e-12)
                {
                    throw new Exception("Matrix is singular.");
                }

                x[i] = sum / R[i][i];
            }

            return x;
        }

        public static double ComputeResidual(double[][] A, double[] x, double[] b)
        {
            int n = A.Length;

            double error = 0;

            for (int i = 0; i < n; i++)
            {
                double sum = 0;

                for (int j = 0; j < n; j++)
                {
                    sum += A[i][j] * x[j];
                }

                error += Math.Pow(sum - b[i], 2);
            }

            return Math.Sqrt(error);
        }


        public static double ComputeDeterminant(double[][] A)
        {
            int n = A.Length;

            double[][] R = A.Select(row => row.ToArray()).ToArray();

            int reflections = 0;

            for (int k = 0; k < n - 1; k++)
            {
                double norm = 0;

                for (int i = k; i < n; i++)
                    norm += R[i][k] * R[i][k];

                norm = Math.Sqrt(norm);

                if (norm == 0)
                    continue;

                reflections++;

                double[] v = new double[n];

                for (int i = k; i < n; i++)
                    v[i] = R[i][k];

                v[k] += (v[k] >= 0) ? norm : -norm;

                double vNorm = 0;

                for (int i = k; i < n; i++)
                    vNorm += v[i] * v[i];

                vNorm = Math.Sqrt(vNorm);

                for (int i = k; i < n; i++)
                    v[i] /= vNorm;

                for (int j = k; j < n; j++)
                {
                    double dot = 0;

                    for (int i = k; i < n; i++)
                        dot += v[i] * R[i][j];

                    for (int i = k; i < n; i++)
                        R[i][j] -= 2 * v[i] * dot;
                }
            }

            double det = 1;

            for (int i = 0; i < n; i++)
                det *= R[i][i];

            if (reflections % 2 != 0)
                det *= -1;

            return det;
        }
    }
}


