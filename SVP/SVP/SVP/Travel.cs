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
    public class Travel
    {
        public List<Polyline> drawLines(double[,] travelLines, Color color, double stroke)
        {
            List<Polyline> polLines = new List<Polyline>(); 

            for (int i = 0; i < travelLines.GetLength(0); i++)
            {
                Polyline polLine = new Polyline();
                polLine.Stroke = new SolidColorBrush(color);
                polLine.StrokeThickness = stroke;

                PointCollection pointCol = new PointCollection();

                for (int j = 0; j < (travelLines.GetLength(1) / 2); j++)
                {
                    Point point = new Point((float)travelLines[i, j], (float)travelLines[i, (j + (travelLines.GetLength(1) / 2))]);
                    pointCol.Add(point);
                }

                polLine.Points = pointCol;
                polLines.Add(polLine);
            }

            return polLines;
        }
    }
}
