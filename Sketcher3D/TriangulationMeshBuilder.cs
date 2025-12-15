using GeometryEngine3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Sketcher3D
{
    public static class TriangulationMeshBuilder
    {
        public static MeshGeometry3D ToMesh(Triangulation tri)
        {
            var mesh = new MeshGeometry3D();
            foreach (var p in tri.getPoints())
                mesh.Positions.Add(new Point3D(p.getX(), p.getY(), p.getZ()));
            foreach (var t in tri.getTriangles())
            {
                mesh.TriangleIndices.Add(t.m1);
                mesh.TriangleIndices.Add(t.m2);
                mesh.TriangleIndices.Add(t.m3);
            }
            // normals optional in WPF; positions+indices are enough
            return mesh;
        }
    }
}
