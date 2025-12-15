using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    public class Triangulation
    {
        private readonly List<Point> mPoints = new List<Point>();
        private readonly List<Point> mNormals = new List<Point>();
        private readonly List<Triangle> mTriangles = new List<Triangle>();
        private readonly Dictionary<Point, int> pointIndex = new Dictionary<Point, int>();

        public List<Point> getPoints() {  return mPoints; }
        public List<Point> getNormals() { return mNormals; }
        public List<Triangle> getTriangles() { return mTriangles; }
        public int addPoint(Point p)
        {
            if (pointIndex.TryGetValue(p, out int existingIndex)) return existingIndex;
            int index = mPoints.Count;
            mPoints.Add(p);
            pointIndex[p] = index;

            return index;
        }
        private Point calculateNormal(Triangle tri)
        {
            Point u = mPoints[tri.m2] - mPoints[tri.m1];
            Point v = mPoints[tri.m3] - mPoints[tri.m1];

            double nx = u.getY() * v.getZ() - u.getZ() * v.getY();
            double ny = u.getZ() * v.getX() - u.getX() * v.getZ();
            double nz = u.getX() * v.getY() - u.getY() * v.getX();

            double len = Math.Sqrt(nx * nx + ny * ny + nz * nz);
            return new Point(nx/len, ny/len, nz/len);
        }
        public void addTriangle(int a, int b, int c, Point normal = null)
        {
            if(normal == null)
            {
                Triangle t = new Triangle(a, b, c);
                mTriangles.Add(t);
                mNormals.Add(calculateNormal(t));
            }
            else
            {
                Triangle t = new Triangle(a, b, c, normal);
                mTriangles.Add(t);
                mNormals.Add(calculateNormal(t));
            }
        }
        public List<float> getDataForOpenGl()
        {
            List<float> oglData = new List<float>();

            foreach (Triangle t in mTriangles)
            {
                oglData.Add((float)mPoints[t.m1].getX());
                oglData.Add((float)mPoints[t.m1].getY());
                oglData.Add((float)mPoints[t.m1].getZ());
                                  
                oglData.Add((float)mPoints[t.m2].getX());
                oglData.Add((float)mPoints[t.m2].getY());
                oglData.Add((float)mPoints[t.m2].getZ());
                                  
                oglData.Add((float)mPoints[t.m3].getX());
                oglData.Add((float)mPoints[t.m3].getY());
                oglData.Add((float)mPoints[t.m3].getZ());
            }

            return oglData;
        }
        public List<float> getNormalForOpenGl()
        {
            List<float> oglData = new List<float>();

            foreach (Triangle t in mTriangles)
            {
                Point pt = calculateNormal(t);

                oglData.Add((float)pt.getX());
                oglData.Add((float)pt.getY());
                oglData.Add((float)pt.getZ());
            }

            return oglData;
        }
    }
}
