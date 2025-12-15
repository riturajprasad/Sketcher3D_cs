using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    public class Cuboid : Shape
    {
        private readonly double mLength;
        private readonly double mWidth;
        private readonly double mHeight;
        public Cuboid(string name, double length, double width, double height) : base("Cuboid", name)
        {
            mLength = length;
            mWidth = width;
            mHeight = height;
            build();
        }
        protected override void build()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            int p0Ind = mTriag.addPoint(new Point(x, y, z));
            int p1Ind = mTriag.addPoint(new Point(x + mLength, y, z));
            int p2Ind = mTriag.addPoint(new Point(x + mLength, y + mWidth, z));
            int p3Ind = mTriag.addPoint(new Point(x, y + mWidth, z));

            mTriag.addTriangle(p0Ind, p2Ind, p1Ind); // front
            mTriag.addTriangle(p0Ind, p3Ind, p2Ind); // front

            int p4Ind = mTriag.addPoint(new Point(x, y, z + mHeight));
            int p5Ind = mTriag.addPoint(new Point(x + mLength, y, z + mHeight));
            int p6Ind = mTriag.addPoint(new Point(x + mLength, y + mWidth, z + mHeight));
            int p7Ind = mTriag.addPoint(new Point(x, y + mWidth, z + mHeight));

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
        public override void Save(TextWriter outw)
        {
            outw.WriteLine($"{getType()} {getName()} L {mLength} W {mWidth} H {mHeight}");
        }
        public override void SaveForGnu(TextWriter outw)
        {
            List<List<Point>> vec = new List<List<Point>>();
            List<Point> pts = new List<Point>();
            double x = 0, y = 0, z = 0;

            pts.Add(new Point(x, y, z));
            pts.Add(new Point(x + mLength, y, z));
            pts.Add(new Point(x + mLength, y + mWidth, z));
            pts.Add(new Point(x, y + mWidth, z));
            pts.Add(new Point(x, y, z));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x, y, z + mHeight));
            pts.Add(new Point(x + mLength, y, z + mHeight));
            pts.Add(new Point(x + mLength, y + mWidth, z + mHeight));
            pts.Add(new Point(x, y + mWidth, z + mHeight));
            pts.Add(new Point(x, y, z + mHeight));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x, y, z));
            pts.Add(new Point(x, y, z + mHeight));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x + mLength, y, z));
            pts.Add(new Point(x + mLength, y, z + mHeight));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x + mLength, y + mWidth, z));
            pts.Add(new Point(x + mLength, y + mWidth, z + mHeight));
            vec.Add(new List<Point>(pts)); pts.Clear();

            pts.Add(new Point(x, y + mWidth, z));
            pts.Add(new Point(x, y + mWidth, z + mHeight));
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
