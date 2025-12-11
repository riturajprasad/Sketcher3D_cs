using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    public class Point
    {
        private double mX;
        private double mY;
        private double mZ;

        public Point()
        {
            mX = 0;
            mY = 0;
            mZ = 0;
        }
        public Point(double x, double y, double z)
        {
            mX = x;
            mY = y;
            mZ = z;
        }
        public double getX() { return mX; }
        public double getY() { return mY; }
        public double getZ() { return mZ; }
        public void setX(double x) { mX = x; }
        public void setY(double y) { mY = y; }
        public void setZ(double z) { mZ = z; }
    }
}
