using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Triangle
    {
        public int m1;
        public int m2;
        public int m3;
        public Point mNormal;
        public Triangle(int m1, int m2, int m3, Point mNormal = default)
        {
            this.m1 = m1;
            this.m2 = m2;
            this.m3 = m3;
            this.mNormal = mNormal;
        }
    }
}
