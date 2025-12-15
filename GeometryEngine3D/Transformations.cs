using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryEngine3D
{
    internal class Transformations
    {
        public static List<Point> applyTransform(List<Point> vertices, Matrix matrix)
        {
            List<Point> transformedPts = new List<Point>();
            foreach (Point p in vertices)
            {
                Matrix pt = new Matrix(4, 1);
                pt[0, 0] = p.getX();
                pt[1, 0] = p.getY();
                pt[2, 0] = p.getZ();
                pt[3, 0] = 1.0;

                Matrix result = matrix * pt;
                double X = result[0, 0];
                double Y = result[1, 0];
                double Z = result[2, 0];
                Point resultPt = new Point(X, Y, Z);
                transformedPts.Add(resultPt);
            }
            return transformedPts;
        }
        private static List<Point> FloatsToPoints(List<float> vec)
        {
            List<Point> pts = new List<Point>(vec.Count / 3);
            for (int i = 0; i + 2 < vec.Count; i += 3)
                pts.Add(new Point(vec[i], vec[i + 1], vec[i + 2]));
            return pts;
        }

        private static void AppendPoints(List<float> vec, List<Point> pts)
        {
            for (int i = 0; i < pts.Count; i++)
            {
                vec.Add((float)pts[i].getX());
                vec.Add((float)pts[i].getY());
                vec.Add((float)pts[i].getZ());
            }
        }
        public static List<float> translate(List<float> vec, double transX = 0, double transY = 0, double transZ = 0)
        {
            List<Point> vertices = FloatsToPoints(vec);

            Matrix transMat = Matrix.getTranslationMatrix(transX, transY, transZ);
            List<Point> transformedPts = applyTransform(vertices, transMat);

            AppendPoints(vec, transformedPts);
            return vec;
        }
        public static List<float> scale(List<float> vec, double scaleX, double scaleY, double scaleZ)
        {
            List<Point> vertices = FloatsToPoints(vec);

            Point pivot = new Point();
            Matrix translate1 = Matrix.getTranslationMatrix(-pivot.getX(), -pivot.getY(), -pivot.getY());
            Matrix scaleMat = Matrix.getScalingMatrix(scaleX, scaleY, scaleZ);
            Matrix translate2 = Matrix.getTranslationMatrix(pivot.getX(), pivot.getY(), pivot.getY());

            Matrix transformed = translate2 * scaleMat * translate1;
            List<Point> transformedPts = applyTransform(vertices, transformed);

            AppendPoints(vec, transformedPts);
            return vec;
        }
        public static List<float> rotationX(List<float> vec, double degreeX)
        {
            List<Point> vertices = FloatsToPoints(vec);

            Point pivot = new Point();
            Matrix translate1 = Matrix.getTranslationMatrix(-pivot.getX(), -pivot.getY(), -pivot.getY());
            Matrix rotateXMat = Matrix.getRotationXMatrix(degreeX);
            Matrix translate2 = Matrix.getTranslationMatrix(pivot.getX(), pivot.getY(), pivot.getY());

            Matrix transformed = translate2 * rotateXMat * translate1;
            List<Point> transformedPts = applyTransform(vertices, transformed);

            AppendPoints(vec, transformedPts);
            return vec;
        }
        public static List<float> rotationY(List<float> vec, double degreeY)
        {
            List<Point> vertices = FloatsToPoints(vec);

            Point pivot = new Point();
            Matrix translate1 = Matrix.getTranslationMatrix(-pivot.getX(), -pivot.getY(), -pivot.getY());
            Matrix rotateYMat = Matrix.getRotationYMatrix(degreeY);
            Matrix translate2 = Matrix.getTranslationMatrix(pivot.getX(), pivot.getY(), pivot.getY());

            Matrix transformed = translate2 * rotateYMat * translate1;
            List<Point> transformedPts = applyTransform(vertices, transformed);

            AppendPoints(vec, transformedPts);
            return vec;
        }
        public static List<float> rotationZ(List<float> vec, double degreeZ)
        {
            List<Point> vertices = FloatsToPoints(vec);

            Point pivot = new Point();
            Matrix translate1 = Matrix.getTranslationMatrix(-pivot.getX(), -pivot.getY(), -pivot.getY());
            Matrix rotateZMat = Matrix.getRotationZMatrix(degreeZ);
            Matrix translate2 = Matrix.getTranslationMatrix(pivot.getX(), pivot.getY(), pivot.getY());

            Matrix transformed = translate2 * rotateZMat * translate1;
            List<Point> transformedPts = applyTransform(vertices, transformed);

            AppendPoints(vec, transformedPts);
            return vec;
        }
    }
}
