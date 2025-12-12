using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Cone : Shape
    {
        private double mRadius;
        private double mHeight;
        public Cone(string name, double radius, double height) : base("Cone", name)
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
            Point origin = new Point(x, y, z);
            Point apex = new Point(x, y, mHeight);

            List<int> bPtsIndex = new List<int>();
            int originInd = mTriag.addPoint(origin);
            int apexInd = mTriag.addPoint(apex);

            bPtsIndex.Add(mTriag.addPoint(new Point(x + mRadius * Math.Cos(0), y + mRadius * Math.Sin(0), z)));
            int number = 72;
            double dTheta = 2 * Math.PI / number;
            for (int i = 1; i <= number; i++)
            {
                double theta = i * dTheta;
                double x_ = mRadius * Math.Cos(theta);
                double y_ = mRadius * Math.Sin(theta);

                bPtsIndex.Add(mTriag.addPoint( new Point(x + x_, y + y_, z)));

                // each 5 degree section has 4 triangles.
                mTriag.addTriangle(bPtsIndex[i - 1], originInd, bPtsIndex[i]);      // Base circle center, two points on it's circumference
                mTriag.addTriangle(bPtsIndex[i - 1], bPtsIndex[i], apexInd);        // Cone surface triangle: b1, apex, b0 
            }
        }
    }
}
