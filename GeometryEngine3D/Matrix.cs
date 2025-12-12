using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Matrix
    {
        private int mRows;
        private int mCols;
        private double[,] mData;

        public Matrix(int rows = 4, int cols = 4)
        {
            mRows = rows;
            mCols = cols;
            mData = new double[rows, cols];
        }
        public double this[int r, int c]
        {
            get { return mData[r, c]; }
            set { mData[r, c] = value; }
        }
        public Matrix getIdentity(int n = 4)
        {
            Matrix identity = new Matrix(n, n);
            for (int i = 0; i < n; i++) identity[i, i] = 1;
            return identity;
        }
        public static Matrix operator+(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.mRows, a.mCols);
            if (a.mRows == b.mRows && a.mCols == b.mCols)
            {
                for (int i = 0; i < a.mRows; i++)
                {
                    for (int j = 0; j < a.mCols; j++)
                    {
                        result[i, j] = a[i, j] + b[i, j];
                    }
                }
                return result;
            }
            else return null;
        }
        public static Matrix operator*(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.mRows, b.mCols);
            if (a.mCols == b.mRows)
            {
                for (int i = 0; i < a.mRows; i++)
                {
                    for (int j = 0; j < b.mCols; j++)
                    {
                        result[i, j] = a[i, j] * b[i, j];
                    }
                }
                return result;
            }
            else return null;
        }
    }
}
