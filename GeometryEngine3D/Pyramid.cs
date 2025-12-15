using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    public class Pyramid : Shape
    {
        private readonly double mBaseLength;
        private readonly double mBaseWidth;
        private readonly double mHeight;
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
        public override void Save(TextWriter outw)
        {
            outw.WriteLine($"{getType()} {getName()} L {mBaseLength} W {mBaseWidth} H {mHeight}");
        }
        public override void SaveForGnu(TextWriter outw)
        {
            List<List<Point>> vec = new List<List<Point>>();
            List<Point> pts = new List<Point>();
            double x = 0, y = 0, z = 0;

            double halfL = mBaseLength / 2.0;
            double halfW = mBaseWidth / 2.0;

            pts.Add(new Point(x + halfL, y + halfW, z));
            pts.Add(new Point(x + halfL, y - halfW, z));
            pts.Add(new Point(x - halfL, y - halfW, z));
            pts.Add(new Point(x - halfL, y + halfW, z));
            pts.Add(new Point(x + halfL, y + halfW, z));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x + halfL, y + halfW, z));
            pts.Add(new Point(x, y, z + mHeight));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x + halfL, y - halfW, z));
            pts.Add(new Point(x, y, z + mHeight));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x - halfL, y - halfW, z));
            pts.Add(new Point(x, y, z + mHeight));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x - halfL, y + halfW, z));
            pts.Add(new Point(x, y, z + mHeight));
            vec.Add(new List<Point>(pts)); pts.Clear();

            foreach (List<Point> list in vec)
            {
                foreach (Point p in list) p.WriteXYZ(outw);
                outw.WriteLine(); outw.WriteLine();
            }
            outw.WriteLine(); outw.WriteLine();
        }
    }
}
