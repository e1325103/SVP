using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SVP
{
    public class Util
    {
        public static byte[,] colors = new byte[,] {  {255, 0, 0},
                                                {0, 255, 0},
                                                {0, 0, 255},
                                                {255, 255, 0},
                                                {0, 255, 255}};

        private static double darkerPercentage = 0.7;

        public static int numSteps { get; set; }

        public static List<Polyline> getPolyLines(List<Line> lines, Color color, double stroke)
        {
            List<Polyline> polLines = new List<Polyline>();

            foreach (Line line in lines)
            {
                polLines.Add(getPolyLine(line, color, stroke));
            }

            return polLines;
        }

        public static Polyline getPolyLine(Line line, Color color, double stroke)
        {
            Polyline polLine = new Polyline();
            polLine.Stroke = new SolidColorBrush(color);
            polLine.StrokeThickness = stroke;

            PointCollection pointCol = new PointCollection();

            foreach (Vec2 point in line.Points)
            {
                pointCol.Add(new Point(point.X, point.Y));
            }

            polLine.Points = pointCol;

            return polLine;
        }

        public static List<Line> getLines(double[,] centerLines)
        {
            List<Line> lines = new List<Line>();

            Line line;

            for (int i = 0; i < centerLines.GetLength(0); i++)
            {
                Vec2 point;

                line = new Line();

                for (int j = 0; j < (centerLines.GetLength(1) / 2); j++)
                {
                    point = new Vec2((float)centerLines[i, j], (float)centerLines[i, (j + (centerLines.GetLength(1) / 2))]);
                    line.add(point);
                }

                lines.Add(line);
            }

            return lines;
        }

        public static List<Polyline> getPolyLinesWithPercentage(List<Line> lines, double[] percentCluster, bool isCenter)
        {
            List<Polyline> polLines = new List<Polyline>();

            for (int i = 0; i < lines.Count; i++)
            {
                Color color; 

                if (isCenter)
                {
                    color = Color.FromRgb((byte)(colors[i, 0] * darkerPercentage), (byte)(colors[i, 1] * darkerPercentage), (byte)(colors[i, 2] * darkerPercentage));
                }
                else
                {
                    color = Color.FromRgb(colors[i, 0], colors[i, 1], colors[i, 2]);
                }

                polLines.Add(getPolyLine(lines[i], color, (10 * percentCluster[i])));
            }

            return polLines;
        }

        public static string getLineMatrix(Line line)
        {
            /* double[,] lineArray = new double[numSteps * 2, numSeeds];

             int currentSeed = 0;

             foreach (Line line in lines)
             {
                 int currentStep = 0;

                 foreach (Vec2 point in line.Points)
                 {
                     lineArray[currentStep, currentSeed] = point.X;
                     lineArray[currentStep + numSteps, currentSeed] = point.Y;

                     currentStep++;
                 }

                 currentSeed++;
             }*/

            // return lineArray;

            int columns = numSteps;

            int i = 0;

            string returnString = "";

            string xValues = "";
            string yValues = "";

            foreach (Vec2 point in line.Points)
            {
                xValues += point.X + " ";
                yValues += point.Y + " ";
                i++;
            }

            while (i < columns)
            {
                xValues += line.Points[line.Points.Count - 1].X + " ";
                yValues += line.Points[line.Points.Count - 1].Y + " ";
                i++;
            }

            returnString += xValues;
            returnString += yValues;
            returnString += "]";

            returnString = returnString.Replace(',', '.');

            return returnString;
        }

        public static double[] Make1Dimensional(double[,] array2D)
        {
            double[] array1D = new double[array2D.GetLength(0)];

            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                array1D[i] = array2D[i, 0];
            }

            return array1D;
        }

        public static Rectangle[] getRects(int numClusters, double[] percentCluster, double width, double height)
        {
            Rectangle[] rects = new Rectangle[percentCluster.Length];
            
            for (int i = 1; i <= numClusters; i++)
            {
                double percent = percentCluster[i - 1];

                Rectangle rect = new Rectangle();
                rect.Width = width;
                rect.Height = height * percent;
                rect.Fill = new SolidColorBrush(Color.FromArgb(125, colors[i - 1, 0], colors[i - 1, 1], colors[i - 1, 2]));

                rects[i - 1] = rect;
            }

            Array.Sort(percentCluster, rects);

            return rects;
        }

        public static List<Polyline> getBorders(double[,] coastlon, double[,] coastlat, Color color, double stroke)
        {
            List<Polyline> borderLines = new List<Polyline>();

            PointCollection pointCol = new PointCollection();

            for (int i = 0; i < coastlat.GetLength(0); i++)
            {
                double x = (coastlon[i, 0] + 20) * 14;
                double y = (coastlat[i, 0] - 80) * 14;

                if (double.IsNaN(x))
                {
                    Polyline polLine = new Polyline();
                    polLine.Stroke = new SolidColorBrush(color);
                    polLine.StrokeThickness = stroke;

                    polLine.Points = pointCol;

                    borderLines.Add(polLine);

                    pointCol = new PointCollection();
                }
                else
                {
                    pointCol.Add(new Point(x, y * -1));
                }
            }

            Polyline lastPolLine = new Polyline();
            lastPolLine.Stroke = new SolidColorBrush(color);
            lastPolLine.StrokeThickness = stroke;
            lastPolLine.Points = pointCol;

            borderLines.Add(lastPolLine);

            return borderLines;
        }
    }
}
