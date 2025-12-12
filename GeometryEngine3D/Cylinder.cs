using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Cylinder : Shape
    {
        private double mRadius;
        private double mHeight;
        public Cylinder(string name, double radius, double height) : base("Cylinder", name)
        {
            mRadius = radius;
            mHeight = height;
            build();
        }
        protected override void build()
        {
            double x = 0;
            double y = 0;
            double z = 0;
            Point baseCenter = new Point(x, y, z);
            Point topCenter = new Point(x, y, mHeight);

            List<int> bPtsIndex = new List<int>();
            List<int> tPtsIndex = new List<int>();
            int baseCenterInd = mTriag.addPoint(baseCenter);
            int topCenterInd = mTriag.addPoint(topCenter);

            bPtsIndex.Add(mTriag.addPoint(new Point(x + mRadius * Math.Cos(0), y + mRadius * Math.Sin(0), z)));
            tPtsIndex.Add(mTriag.addPoint(new Point(x + mRadius * Math.Cos(0), y + mRadius * Math.Sin(0), z + mHeight)));

            int number = 72;
            double dTheta = 2 * Math.PI / number;

            for (int i = 1; i <= number; i++)
            {
                double theta = i * dTheta;
                double x_ = mRadius * Math.Cos(theta);
                double y_ = mRadius * Math.Sin(theta);

                bPtsIndex.Add(mTriag.addPoint(new Point(x + x_, y + y_, z)));
                tPtsIndex.Add(mTriag.addPoint(new Point(x + x_, y + y_, z + mHeight)));

                // each 5 degree section has 4 triangles.
                mTriag.addTriangle(bPtsIndex[i], bPtsIndex[i - 1], baseCenterInd);      // Base circle center, two points on it's circumference
                mTriag.addTriangle(bPtsIndex[i - 1], bPtsIndex[i], tPtsIndex[i]);       // Cylinder surface triangle: b1, t1, b0 
                mTriag.addTriangle(bPtsIndex[i - 1], tPtsIndex[i], tPtsIndex[i - 1]);   // Cylinder surface triangle: b0, t1, t0
                mTriag.addTriangle(topCenterInd, tPtsIndex[i - 1], tPtsIndex[i]);       // Top circle center, two points on it's circumference
            }
        }
    }
}
