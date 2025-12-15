using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryEngine3D;

namespace Sketcher3D
{
    public class ShapeManager
    {
        private readonly List<Shape> mShapes = new List<Shape>();
        public void AddShape(Shape s) => mShapes.Add(s);
        public IReadOnlyList<Shape> GetShapes() => mShapes;
        public void Clear() => mShapes.Clear();
        public Shape GetLastShape() => mShapes.Count > 0 ? mShapes[mShapes.Count - 1] : null;
    }
}
