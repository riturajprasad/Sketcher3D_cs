using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Triangulation
    {
        private List<Point> mPoints = new List<Point>();
        private List<Point> mNormals = new List<Point>();
        private List<Triangle> mTriangles = new List<Triangle>();
        private Dictionary<Point, int> pointIndex = new Dictionary<Point, int>();

        public int addPoint(Point p)
        {
            if (pointIndex.TryGetValue(p, out int existingIndex)) return existingIndex;
            int index = mPoints.Count;
            mPoints.Add(p);
            pointIndex[p] = index;

            return index;
        }
    }
}
