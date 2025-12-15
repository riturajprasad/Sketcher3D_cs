using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    public class Triangle
    {
        public int m1;
        public int m2;
        public int m3;
        public Point mNormal;
        public Triangle(int m1, int m2, int m3, Point mNormal = null)
        {
            if (mNormal == null) mNormal = new Point();
            else this.mNormal = mNormal;
            this.m1 = m1;
            this.m2 = m2;
            this.m3 = m3;
        }
    }
}
