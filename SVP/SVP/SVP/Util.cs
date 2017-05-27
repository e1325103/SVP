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
    /// <summary>
    /// This class offers several usefull functions like the conversion from lines to drawable polylines, the conversion of a streamline to a
    /// format that is handed to matlab, the generation of coloured rectangles, showing how many lines lie in a specific cluster, a line generator
    /// for the borders of the countries and other line operations.
    /// </summary>
    public class Util
    {

        /// <summary>
        /// 5 different colours for the clusters
        /// </summary>
        public static byte[,] colors = new byte[,] {  {255, 0, 0},
                                                {0, 255, 0},
                                                {0, 0, 255},
                                                {255, 255, 0},
                                                {0, 255, 255}};

        /// <summary>
        /// A double value showing, how much darker the streamline medians shall be plotted.
        /// </summary>
        private static double darkerPercentage = 0.7;

        public static int numSteps { get; set; }

        /// <summary>
        /// This method converts several lines to drawable polylines by using the getPolyline function for each element.
        /// </summary>
        /// <param name="lines">The list of lines to be transformed</param>
        /// <param name="color">The color the polylines shall be plotted with</param>
        /// <param name="stroke">The stroke of the brush for the polylines</param>
        /// <returns>A list of drawable polylines</returns>
        public static List<Polyline> getPolyLines(List<Line> lines, Color color, double stroke)
        {
            List<Polyline> polLines = new List<Polyline>();

            foreach (Line line in lines)
            {
                polLines.Add(getPolyLine(line, color, stroke));
            }

            return polLines;
        }

        /// <summary>
        /// This method converts a single line into a drawable polyline.
        /// </summary>
        /// <param name="line">The line to be transformed into a drawable polyline</param>
        /// <param name="color">The color the polylines shall be plotted with</param>
        /// <param name="stroke">The stroke of the brush for the polylines</param>
        /// <returns>A drawable polyline</returns>
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

        /// <summary>
        /// This method converts a 2 dim double array handed by matlab into Lines.
        /// </summary>
        /// <param name="centerLines">The 2 dim double array containing lines</param>
        /// <returns>A list of lines</returns>
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

        /// <summary>
        /// This method converts a list of lines into drawable polylines. The percentCluster shows, how many of the clustered streamlins are containd in the 
        /// specific cluster. Each cluster has a percnetage in this array. If the plottet lines are median lines (if isCenter is true) the respective colour
        /// of the cluster gets darkened.
        /// </summary>
        /// <param name="lines">The list of lines to be transformed to drawable polylines</param>
        /// <param name="percentCluster">The array contaning a percentage value for each cluster saying, how many of the clustered streamlines lie within the cluster</param>
        /// <param name="isCenter">The variable shows, if the lines are centerliens (medians) or not, if median => color gets darker</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method transforms a line into a format that is handed to matlab.
        /// </summary>
        /// <param name="line">THe line to be transformed</param>
        /// <returns>The respective matlab format of a line</returns>
        public static string getLineMatrix(Line line)
        {
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

        /// <summary>
        /// Matlab hands over 2 dim arrays even if the content is actually a 1 dim array. This function
        /// converts the 2 dim array into a 1 dim one.
        /// </summary>
        /// <param name="array2D">The 2 dim array to be transformed</param>
        /// <returns>The respective 1 dim array</returns>
        public static double[] Make1Dimensional(double[,] array2D)
        {
            double[] array1D = new double[array2D.GetLength(0)];

            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                array1D[i] = array2D[i, 0];
            }

            return array1D;
        }

        /// <summary>
        /// This method is used for ghe generation of rectangles for the stack panel on the right side of the image in the GUI, showing how many
        /// of the clustered streamlines are contained in a specific cluster. The height of the rectangles is determined by this percentage
        /// multiplied by the height of the stack panel. 
        /// </summary>
        /// <param name="numClusters">The number of Clusters</param>
        /// <param name="percentCluster">The array contaning a percentage value for each cluster saying, how many of the clustered streamlines lie within the cluster</param>
        /// <param name="width">The width of the stack panel</param>
        /// <param name="height">The height of the stack panel</param>
        /// <returns>An array containing the colored rectangles of each cluster</returns>
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

        /// <summary>
        /// This method transforms the double array of the matlab coastlines dataset into drawable polyliens (border of the countries).
        /// </summary>
        /// <param name="coastlon">The X-Values of the borders</param>
        /// <param name="coastlat">The Y-Values of the borders</param>
        /// <param name="color">The color for the polylines</param>
        /// <param name="stroke">The stroke for the brush of the polylines</param>
        /// <returns>The drawable polylines of the matlab coastlines dataset</returns>
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
