using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Sphere : Shape
    {
        private double mRadius;
        public Sphere(string name, double radius) : base("Sphere", name) { mRadius = radius; build(); }
        protected override void build()
        {
            double x = 0;
            double y = 0;
            double z = 0;
            Point  center = new Point(x, y, z);

            int stacks = 36;
            int number = 72;

            for (int i = 0; i < stacks; i++)
            {
                double iLatitude1 = Math.PI * (-0.5 + (double)i / stacks);
                double iLatitude2 = Math.PI * (-0.5 + (double)(i + 1) / stacks);

                double z1 = mRadius * Math.Sin(iLatitude1);
                double r1 = mRadius * Math.Cos(iLatitude1);

                double z2 = mRadius * Math.Sin(iLatitude2);
                double r2 = mRadius * Math.Cos(iLatitude2);

                for (int j = 0; j < number; j++)
                {
                    double jLatitude1 = 2 * Math.PI * (double)j / number;
                    double jLatitude2 = 2 * Math.PI * (double)(j + 1) / number;

                    // First ring
                    int idx1 = mTriag.addPoint(new Point(r1 * Math.Cos(jLatitude1), r1 * Math.Sin(jLatitude1), z1));
                    int idx2 = mTriag.addPoint(new Point(r1 * Math.Cos(jLatitude2), r1 * Math.Sin(jLatitude2), z1));

                    // Second ring
                    int idx3 = mTriag.addPoint(new Point(r2 * Math.Cos(jLatitude1), r2 * Math.Sin(jLatitude1), z2));
                    int idx4 = mTriag.addPoint(new Point(r2 * Math.Cos(jLatitude2), r2 * Math.Sin(jLatitude2), z2));

                    // Triangle 1
                    mTriag.addTriangle(idx1, idx2, idx3);
                    // Triangle 2
                    mTriag.addTriangle(idx2, idx4, idx3);
                }
            }
        }
    }
}
