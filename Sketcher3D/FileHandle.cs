using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryEngine3D;

namespace Sketcher3D
{
    public static class FileHandle
    {
        // ------------------------ Save .skt (your custom text) ------------------------
        public static bool SaveToFile(string fileName, List<Shape> shapes)
        {
            try
            {
                using (var w = new StreamWriter(fileName))
                {
                    foreach (var s in shapes)
                        s.Save(w);                   // calls engine Save(TextWriter)
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ------------------------ Save for GNUPlot (.dat) ------------------------
        public static bool SaveToFileGNUPlot(string fileName, List<Shape> shapes)
        {
            try
            {
                using (var w = new StreamWriter(fileName))
                {
                    foreach (var s in shapes)
                    {
                        w.WriteLine("#" + s.getType());
                        w.WriteLine("#" + s.getName());
                        s.SaveForGnu(w);            // calls engine SaveForGnu(TextWriter)
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ------------------------ Read ASCII STL -> Triangulation ------------------------
        public static void ReadSTL(string fileName, Triangulation triangulation)
        {
            using (var reader = new StreamReader(fileName))
            {
                string line;
                var pts = new List<Point>(3);
                var inv = CultureInfo.InvariantCulture;

                // Default normal in case STL is malformed
                Point normal = new Point(0, 0, 1);

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.StartsWith("facet normal", StringComparison.OrdinalIgnoreCase))
                    {
                        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 5)
                        {
                            double nx, ny, nz;
                            double.TryParse(parts[2], NumberStyles.Float, inv, out nx);
                            double.TryParse(parts[3], NumberStyles.Float, inv, out ny);
                            double.TryParse(parts[4], NumberStyles.Float, inv, out nz);
                            normal = new Point(nx, ny, nz);
                        }
                    }
                    else if (line.StartsWith("vertex", StringComparison.OrdinalIgnoreCase))
                    {
                        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 4)
                        {
                            double x = 0, y = 0, z = 0;
                            double.TryParse(parts[1], NumberStyles.Float, inv, out x);
                            double.TryParse(parts[2], NumberStyles.Float, inv, out y);
                            double.TryParse(parts[3], NumberStyles.Float, inv, out z);

                            pts.Add(new Point(x, y, z));

                            if (pts.Count == 3)
                            {
                                int a = triangulation.addPoint(pts[0]);
                                int b = triangulation.addPoint(pts[1]);
                                int c = triangulation.addPoint(pts[2]);
                                triangulation.addTriangle(a, b, c);   // normal is computed by engine
                                pts.Clear();
                            }
                        }
                    }
                }
            }
        }

        // ------------------------ Write ASCII STL from shapes ------------------------
        public static bool WriteSTL(string fileName, List<Shape> shapes)
        {
            try
            {
                var inv = CultureInfo.InvariantCulture;

                using (var w = new StreamWriter(fileName))
                {
                    foreach (var shape in shapes)
                    {
                        w.WriteLine($"Start {shape.getType()} mesh");
                        var tri = shape.getTriangulation();
                        var points = tri.getPoints();
                        var triangles = tri.getTriangles();
                        var normals = tri.getNormals();

                        int ni = 0;
                        foreach (var t in triangles)
                        {
                            // Use stored normal if available; otherwise compute quickly
                            Point n;
                            if (ni < normals.Count) n = normals[ni++];
                            else
                            {
                                var u = points[t.m2] - points[t.m1];
                                var v = points[t.m3] - points[t.m1];
                                double nx = u.getY() * v.getZ() - u.getZ() * v.getY();
                                double ny = u.getZ() * v.getX() - u.getX() * v.getZ();
                                double nz = u.getX() * v.getY() - u.getY() * v.getX();
                                double len = Math.Sqrt(nx * nx + ny * ny + nz * nz); if (len == 0) len = 1;
                                n = new Point(nx / len, ny / len, nz / len);
                            }

                            var p1 = points[t.m1];
                            var p2 = points[t.m2];
                            var p3 = points[t.m3];

                            w.WriteLine($"  facet normal {n.getX().ToString(inv)} {n.getY().ToString(inv)} {n.getZ().ToString(inv)}");
                            w.WriteLine("    outer loop");
                            w.WriteLine($"      vertex {p1.getX().ToString(inv)} {p1.getY().ToString(inv)} {p1.getZ().ToString(inv)}");
                            w.WriteLine($"      vertex {p2.getX().ToString(inv)} {p2.getY().ToString(inv)} {p2.getZ().ToString(inv)}");
                            w.WriteLine($"      vertex {p3.getX().ToString(inv)} {p3.getY().ToString(inv)} {p3.getZ().ToString(inv)}");
                            w.WriteLine("    endloop");
                            w.WriteLine("  endfacet");
                        }
                        w.WriteLine($"End {shape.getType()} mesh");
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
