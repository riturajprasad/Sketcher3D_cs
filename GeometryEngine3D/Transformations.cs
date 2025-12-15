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
        public static List<float> translate(List<float> vec, double transX, double transY, double transZ)
        {
            List<Point> vertices = new List<Point>();
            for (int i = 0; i < vec.Count(); i += 3)
            {
                Point vecPt = new Point(vec[i], vec[i+1], vec[i+2]);
                vertices.Add(vecPt);
            }
            Matrix transMat = Matrix.getTranslationMatrix(transX, transY, transZ);
            List<Point> transformedPts = applyTransform(vertices, transMat);

            for (int i = 0; i < transformedPts.Count(); i++)
            {
                vec.Add((float)transformedPts[i].getX());
                vec.Add((float)transformedPts[i].getY());
                vec.Add((float)transformedPts[i].getZ());
            }
            return vec;
        }
        public static List<float> scale(List<float> vec, double scaleX, double scaleY, double scaleZ)
        {
            List<Point> vertices = new List<Point>();
            for (int i = 0; i < vec.Count(); i += 3)
            {
                Point vecPt = new Point(vec[i], vec[i+1], vec[i+2]);
                vertices.Add(vecPt);
            }
            Point pivot = new Point();
            Matrix translate1 = Matrix.getTranslationMatrix(-pivot.getX(), -pivot.getY(), -pivot.getY());
            Matrix scaleMat = Matrix.getScalingMatrix(scaleX, scaleY, scaleZ);
            Matrix translate2 = Matrix.getTranslationMatrix(pivot.getX(), pivot.getY(), pivot.getY());

            Matrix transformed = translate2 * scaleMat * translate1;
            List<Point> transformedPts = applyTransform(vertices, transformed);

            for (int i = 0; i < transformedPts.Count(); i++)
            {
                vec.Add((float)transformedPts[i].getX());
                vec.Add((float)transformedPts[i].getY());
                vec.Add((float)transformedPts[i].getZ());
            }
            return vec;
        }
        public static List<float> rotationX(List<float> vec, double degreeX)
        {
            List<Point> vertices = new List<Point>();
            for (int i = 0; i < vec.Count(); i += 3)
            {
                Point vecPt = new Point(vec[i], vec[i+1], vec[i+2]);
                vertices.Add(vecPt);
            }
            Point pivot = new Point();
            Matrix translate1 = Matrix.getTranslationMatrix(-pivot.getX(), -pivot.getY(), -pivot.getY());
            Matrix rotateXMat = Matrix.getRotationXMatrix(degreeX);
            Matrix translate2 = Matrix.getTranslationMatrix(pivot.getX(), pivot.getY(), pivot.getY());

            Matrix transformed = translate2 * rotateXMat * translate1;
            List<Point> transformedPts = applyTransform(vertices, transformed);

            for (int i = 0; i < transformedPts.Count(); i++)
            {
                vec.Add((float)transformedPts[i].getX());
                vec.Add((float)transformedPts[i].getY());
                vec.Add((float)transformedPts[i].getZ());
            }
            return vec;
        }
        public static List<float> rotationY(List<float> vec, double degreeY)
        {
            List<Point> vertices = new List<Point>();
            for (int i = 0; i < vec.Count(); i += 3)
            {
                Point vecPt = new Point(vec[i], vec[i+1], vec[i+2]);
                vertices.Add(vecPt);
            }
            Point pivot = new Point();
            Matrix translate1 = Matrix.getTranslationMatrix(-pivot.getX(), -pivot.getY(), -pivot.getY());
            Matrix rotateYMat = Matrix.getRotationYMatrix(degreeY);
            Matrix translate2 = Matrix.getTranslationMatrix(pivot.getX(), pivot.getY(), pivot.getY());

            Matrix transformed = translate2 * rotateYMat * translate1;
            List<Point> transformedPts = applyTransform(vertices, transformed);

            for (int i = 0; i < transformedPts.Count(); i++)
            {
                vec.Add((float)transformedPts[i].getX());
                vec.Add((float)transformedPts[i].getY());
                vec.Add((float)transformedPts[i].getZ());
            }
            return vec;
        }
        public static List<float> rotationZ(List<float> vec, double degreeZ)
        {
            List<Point> vertices = new List<Point>();
            for (int i = 0; i < vec.Count(); i += 3)
            {
                Point vecPt = new Point(vec[i], vec[i+1], vec[i+2]);
                vertices.Add(vecPt);
            }
            Point pivot = new Point();
            Matrix translate1 = Matrix.getTranslationMatrix(-pivot.getX(), -pivot.getY(), -pivot.getY());
            Matrix rotateZMat = Matrix.getRotationZMatrix(degreeZ);
            Matrix translate2 = Matrix.getTranslationMatrix(pivot.getX(), pivot.getY(), pivot.getY());

            Matrix transformed = translate2 * rotateZMat * translate1;
            List<Point> transformedPts = applyTransform(vertices, transformed);

            for (int i = 0; i < transformedPts.Count(); i++)
            {
                vec.Add((float)transformedPts[i].getX());
                vec.Add((float)transformedPts[i].getY());
                vec.Add((float)transformedPts[i].getZ());
            }
            return vec;
        }
    }
}
