using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    abstract class Shape
    {
        private string mType;
        private string mName;

        protected Triangulation mTriag;
        protected abstract void build();

        public Shape(string type, string name) { mType = type; mName = name; }
        public string getType() { return mType; }
        public string getName() { return mName; }
        public Triangulation getTriangulation() { return mTriag; }

        public abstract void Save(TextWriter w);
        public abstract void SaveForGnu(TextWriter w);
    }
}
