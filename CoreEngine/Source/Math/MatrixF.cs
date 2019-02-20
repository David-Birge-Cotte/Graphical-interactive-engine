using System;

namespace CoreEngine
{
    /// <summary>
    /// Small Matrix class made to be used with neural networks
    /// Can do basic math and has a few useful built-in functions
    /// </summary>
    public class MatrixF
    {
        #region Members
        public int n { get; private set; } // number of lines
        public int m { get; private set; } // number of columns
        public float[,] data;
        #endregion

        #region Constructors
        public MatrixF(int _n, int _m)
        {
            n = _n; m = _m;
            data = new float[n, m]; // initialized at 0 cause C#
        }
        #endregion

        #region Matric Operations
        public void Add(int _val)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    data[i, j] += _val;
        }

        public void Add(MatrixF _m2)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    data[i, j] += _m2.data[i, j];
        }

        public void Scale(int _scale)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    data[i, j] *= _scale;
        }

        public static MatrixF DotProduct(MatrixF _m1, MatrixF _m2)
        {
            if (_m1.m != _m2.n)
                return null;

            MatrixF m = new MatrixF(_m1.n, _m2.m);
            for (int i = 0; i < m.n; i++)
            {
                for (int j = 0; j < m.m; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < _m1.m; k++)
                        sum += _m1.data[i, k] * _m2.data[k, j];
                    m.data[i, j] = sum;
                }
            }
            return m;
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Transposes the matrix [n, m] to [m, n]
        /// </summary>
        public void Transpose()
        {
            float[,] mNew = new float[n, m];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    mNew[j, i] = data[i, j];
            int m_ = m; m = n; n = m_;
            data = mNew;
        }

        /// <summary>
        /// Sets all the matrix cells to 0
        /// </summary>
        public void SetToZero()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    data[i, j] = 0;
        }

        /// <summary>
        /// Sets all matrix cells to a random value
        /// </summary>
        public void Randomize()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    data[i, j] = Noise.Generate(-10f, 10f);
        }
        #endregion

        #region Static functions
        public static MatrixF operator +(MatrixF _m1, MatrixF _m2)
        {
            if (_m1.n != _m2.n || _m1.m != _m2.m)
                return null;

            // Add the two matrices
            MatrixF m = new MatrixF(_m1.n, _m1.m);
            for (int i = 0; i < _m1.n; i++)
                for (int j = 0; j < _m1.m; j++)
                    m.data[i, j] = _m1.data[i, j] + _m2.data[i, j];
            return (m);
        }

        /// <summary>
        /// Creates a new Matrix with random values
        /// </summary>
        /// <param name="_n">number of lines</param>
        /// <param name="_m">number of columns</param>
        /// <returns></returns>
        public static MatrixF RandomMatrix(int _n, int _m)
        {
            MatrixF m = new MatrixF(_n, _m);
            m.Randomize();
            return (m);
        }

        /// <summary>
        /// Creates a new instance of a matrix containing the same data as the original matrix
        /// </summary>
        /// <param name="_m">original matrix to copy</param>
        /// <returns></returns>
        public static MatrixF Copy(MatrixF _m)
        {
            MatrixF ma = new MatrixF(_m.n, _m.m);
            for (int i = 0; i < ma.n; i++)
                for (int j = 0; j < ma.m; j++)
                    ma.data[i, j] = _m.data[i, j];
            return ma;
        }

        /// <summary>
        /// Creates an instance of a copy of matrix then applies a little randomization on the data
        /// </summary>
        /// <param name="_m">Original matrix</param>
        /// <returns></returns>
        public static MatrixF Mutate(MatrixF _m, float _percentage)
        {
            MatrixF ma = Copy(_m);
            for (int i = 0; i < ma.n; i++)
                for (int j = 0; j < ma.m; j++)
                    ma.data[i, j] += Noise.Gaussian(-_percentage / 2f, _percentage / 2f);
            return ma;
        }

        /// <summary>
        /// Creates a matrix from an array of data [data.lenght, 1]
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public static MatrixF FromArray(float[] _data)
        {
            MatrixF m = new MatrixF(_data.Length, 1);
            for (int i = 0; i < _data.Length; i++)
                m.data[i, 0] = _data[i];
            return m;
        }

        public static float[] ToArray(MatrixF _m)
        {
            float[] data = new float[_m.n];
            for (int i = 0; i < _m.n; i++)
                data[i] = _m.data[i, 0];
            return data;
        }

        public static void PrintMatrix(MatrixF _m)
        {
            for (int i = 0; i < _m.n; i++)
            {
                for (int j = 0; j < _m.m; j++)
                    Console.Write($"{_m.data[i, j]} ");
                Console.Write('\n');
            }
        }
        #endregion
    }
}
