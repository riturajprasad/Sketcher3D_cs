using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Cube : Shape
    {
        private double mSide;
        public Cube(string name, double side) : base("Cube", name) { mSide = side; build(); }
        protected override void build()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            int p0Ind = mTriag.addPoint(new Point(x, y, z));
            int p1Ind = mTriag.addPoint(new Point(x + mSide, y, z));
            int p2Ind = mTriag.addPoint(new Point(x + mSide, y + mSide, z));
            int p3Ind = mTriag.addPoint(new Point(x, y + mSide, z));

            mTriag.addTriangle(p0Ind, p2Ind, p1Ind); // front
            mTriag.addTriangle(p0Ind, p3Ind, p2Ind); // front

            int p4Ind = mTriag.addPoint(new Point(x, y, z + mSide));
            int p5Ind = mTriag.addPoint(new Point(x + mSide, y, z + mSide));
            int p6Ind = mTriag.addPoint(new Point(x + mSide, y + mSide, z + mSide));
            int p7Ind = mTriag.addPoint(new Point(x, y + mSide, z + mSide));

            mTriag.addTriangle(p4Ind, p5Ind, p6Ind); // back
            mTriag.addTriangle(p4Ind, p6Ind, p7Ind); // back

            mTriag.addTriangle(p7Ind, p6Ind, p2Ind); // top
            mTriag.addTriangle(p7Ind, p2Ind, p3Ind); // top

            mTriag.addTriangle(p0Ind, p1Ind, p5Ind); // bottom
            mTriag.addTriangle(p0Ind, p5Ind, p4Ind); // bottom

            mTriag.addTriangle(p5Ind, p1Ind, p2Ind); // right
            mTriag.addTriangle(p5Ind, p2Ind, p6Ind); // right

            mTriag.addTriangle(p0Ind, p4Ind, p7Ind); // left
            mTriag.addTriangle(p0Ind, p7Ind, p3Ind); // left
        }
    }
}
