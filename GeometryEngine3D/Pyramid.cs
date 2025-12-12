using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Pyramid : Shape
    {
        private double mBaseLength;
        private double mBaseWidth;
        private double mHeight;
        public Pyramid(string name, double baseLength, double baseWidth, double height) : base("Pyramid", name)
        {
            mBaseLength = baseLength;
            mBaseWidth = baseWidth;
            mHeight = height;
            build();
        }
        protected override void build()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            double halfL = mBaseLength / 2.0; // center as origin
            double halfW = mBaseWidth / 2.0;

            int p0Ind = mTriag.addPoint(new Point(x + halfL, y + halfW, z)); //b1 base points
            int p1Ind = mTriag.addPoint(new Point(x + halfL, y - halfW, z)); //b2
            int p2Ind = mTriag.addPoint(new Point(x - halfL, y - halfW, z)); //b3
            int p3Ind = mTriag.addPoint(new Point(x - halfL, y + halfW, z)); //b4

            mTriag.addTriangle(p0Ind, p2Ind, p3Ind);//base
            mTriag.addTriangle(p2Ind, p0Ind, p1Ind);//base

            int apexInd = mTriag.addPoint(new Point(x, y, z + mHeight)); //Apex

            mTriag.addTriangle(p1Ind, p0Ind, apexInd);
            mTriag.addTriangle(p2Ind, p1Ind, apexInd);
            mTriag.addTriangle(p3Ind, p2Ind, apexInd);
            mTriag.addTriangle(p0Ind, p3Ind, apexInd);
        }
    }
}
