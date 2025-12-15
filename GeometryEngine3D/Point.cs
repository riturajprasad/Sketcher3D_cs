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

        public void WriteXYZ(System.IO.TextWriter w) => w.WriteLine($"{getX()} {getY()} {getZ()}");

        public static Point operator-(Point p2, Point p1)
        {
            return new Point((p2.mX - p1.mX), (p2.mY - p1.mY), (p2.mZ - p1.mZ));
        }
        public static bool operator<(Point p1, Point p2)
        {
            if (p1.mX != p2.mX) return p1.mX < p2.mX;
            if (p1.mY != p2.mY) return p1.mY < p2.mY;
            return p1.mZ < p2.mZ;
        }
        public static bool operator>(Point p1, Point p2)
        {
            if (p1.mX != p2.mX) return p1.mX > p2.mX;
            if (p1.mY != p2.mY) return p1.mY > p2.mY;
            return p1.mZ > p2.mZ;
        }
    }
}
