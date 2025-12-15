using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Matrix
    {
        private readonly int mRows;
        private readonly int mCols;
        private readonly double[,] mData;

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
        public static Matrix getIdentity(int n = 4)
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
        public static Matrix getTranslationMatrix(double tx = 0, double ty = 0, double tz = 0)
        {
            Matrix transMat = getIdentity();
            transMat[0, 3] += tx;
            transMat[1, 3] += ty;
            transMat[2, 3] -= tz;
            return transMat;
        }
        public static Matrix getScalingMatrix(double sx = 1, double sy = 1, double sz = 1)
        {
            Matrix scaleMat = getIdentity();
            scaleMat[0, 0] = sx;
            scaleMat[1, 1] = sy;
            scaleMat[2, 2] = sz;
            return scaleMat;
        }
        public static Matrix getRotationXMatrix(double degreeX)
        {
            Matrix rotXMat = getIdentity();
            double radAngX = degreeX * Math.PI / 180.0;
            double cosX = Math.Cos(radAngX);
            double sinX = Math.Sin(radAngX);

            rotXMat[1, 1] = cosX; rotXMat[1, 2] = -sinX;
            rotXMat[2, 1] = sinX; rotXMat[2, 2] = cosX;
            return rotXMat;
        }
        public static Matrix getRotationYMatrix(double degreeY)
        {
            Matrix rotYMat = getIdentity();
            double radAngY = degreeY * Math.PI / 180.0;
            double cosY = Math.Cos(radAngY);
            double sinY = Math.Sin(radAngY);

            rotYMat[0, 0] = cosY; rotYMat[0, 2] = sinY;
            rotYMat[2, 0] = -sinY; rotYMat[2, 2] = cosY;
            return rotYMat;
        }
        public static Matrix getRotationZMatrix(double degreeZ)
        {
            Matrix rotZMat = getIdentity();
            double radAngZ = degreeZ * Math.PI / 180.0;
            double cosZ = Math.Cos(radAngZ);
            double sinZ = Math.Sin(radAngZ);

            rotZMat[0, 0] = cosZ; rotZMat[0, 1] = -sinZ;
            rotZMat[1, 0] = sinZ; rotZMat[1, 1] = cosZ;
            return rotZMat;
        }
    }
}
