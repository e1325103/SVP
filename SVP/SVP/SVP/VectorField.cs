using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SVP
{
    /// <summary>
    /// The vector field class containing the vectors of the stream implementing the IClusterObject interface.
    /// </summary>
    public class VectorField : IClusterObject
    {
        private byte[] backgroundImage;

        public int fieldSize = 500;

        /// <summary>
        /// This parameter is used to check, whether the streamlines have changed between clustering step. If this flag is
        /// set, the streamlines do not get handed to matlab again because they were already handed before.
        /// </summary>
        public bool freshySimulated { get; set; }

        private double transform;

        private Vec2[, ,] field;

        public List<Line> rungeLines { get; set; }

        /// <summary>
        /// The constructor of the vectorfield initializes the object and sets the width parameter of the
        /// image, the stream- and pathlines are painted onto.
        /// </summary>
        /// <param name="width"></param>
        public VectorField(double width)
        {
            transform = (width / (fieldSize * 2));
        }
        
        /// <summary>
        /// This method imports a hurricane dataset from a csv file and stores it in the 3 dim field array.
        /// </summary>
        /// <param name="path"></param>
        public void import(string path)
        {

            field = new Vec2[500, 500, 48];
            using (StreamReader uReader = new StreamReader(new FileStream(path + "\\u.csv", FileMode.Open)))
            {
                using (StreamReader vReader = new StreamReader(new FileStream(path + "\\v.csv", FileMode.Open)))
                {
                    for (int i = 0; i < 48; i++)
                    {
                        for (int y = 0; y < 500; y++)
                        {
                            string[] uParts = uReader.ReadLine().Split(';');
                            string[] vParts = vReader.ReadLine().Split(';');
                            for (int x = 0; x < 500; x++)
                            {
                                field[x, y, i] = new Vec2(float.Parse(vParts[x]) * -1, float.Parse(uParts[x]) * 1);
                            }
                        }
                    }
                }
            }
        }      

        /// <summary>
        /// This method transformes the lines to be displayed correctly.
        /// </summary>
        /// <param name="lines">The lines to be displayed</param>
        /// <returns></returns>
        public List<Line> transformLines(List<Line> lines)
        {
            List<Line> transformedLines = new List<Line>();

            foreach (Line line in lines)
            {
                Line transformedLine = new Line();

                foreach (Vec2 point in line.Points)
                {
                    double x = (point.X - 1) * 2;
                    double y = (point.Y - 1) * 2;


                    if (x < 1000 && y < 1000 && x >= 0 && y >= 0)
                    {
                        transformedLine.add(new Vec2((int)Math.Round(y * transform, 0), (int)Math.Round(x * transform, 0)));
                    }
                }

                transformedLines.Add(transformedLine);
            }

            return transformedLines;
        }

        /// <summary>
        /// This method is used for trilinear interpolation of positions contained in the vectorfield.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="t">The time</param>
        /// <returns>A vector specifying the flow for the position x, y at time t</returns>
        public Vec2 interpolateTrilinear(float x, float y, float t)
        {
            int topX = (int)Math.Ceiling(x);
            int topY = (int)Math.Ceiling(y);
            int topT = (int)Math.Ceiling(t);
            int bottomX = (int)Math.Floor(x);
            int bottomY = (int)Math.Floor(y);
            int bottomT = (int)Math.Floor(t);

            float deltaX = (float)Math.Abs(topX - x);
            float deltaY = (float)Math.Abs(topY - y);
            float deltaT = (float)Math.Abs(topT - t);


            if (bottomX < 0)
            {
                Console.WriteLine();
            }

            Vec2 v1 = field[bottomX, bottomY, bottomT] * (1.0f - deltaY) + field[bottomX, topY, bottomT] * deltaY;
            Vec2 v2 = field[topX, bottomY, bottomT] * (1.0f - deltaY) + field[topX, topY, bottomT] * deltaY;

            Vec2 v3 = field[bottomX, bottomY, topT] * (1.0f - deltaY) + field[bottomX, topY, topT] * deltaY;
            Vec2 v4 = field[topX, bottomY, topT] * (1.0f - deltaY) + field[topX, topY, topT] * deltaY;

            Vec2 v5 = v1 * (1.0f - deltaT) + v3 * deltaT;
            Vec2 v6 = v2 * (1.0f - deltaT) + v4 * deltaT;

            return v5 * (1.0f - deltaX) + v6 * deltaX;
        }

        /// <summary>
        /// This method creates the background image of the hurricane dataset. The area above land is plottet green while the area
        /// above the sea is plotted blue. The vectorfield contains (0, 0) vectors above the land.
        /// </summary>
        /// <returns>The background image as a Bitmap with green and blue pixels</returns>
        public ImageSource createImage()
        {
            backgroundImage = new byte[1000 * 1000 * 4];
            int pixelCount = 0;

            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    Vec2 v = field[x / 2, y / 2, 0];

                    byte intensityR = (byte)202;
                    byte intensityG = (byte)226;
                    byte intensityB = (byte)197;

                    if (v.X != 0 || v.Y != 0)
                    {
                        intensityR = (byte)163;
                        intensityG = (byte)204;
                        intensityB = (byte)255;
                    }

                    backgroundImage[pixelCount++] = intensityB;
                    backgroundImage[pixelCount++] = intensityG;
                    backgroundImage[pixelCount++] = intensityR;
                    backgroundImage[pixelCount++] = (byte)255;
                }
            }

            int stride = 1000 * PixelFormats.Bgr32.BitsPerPixel / 8;
            return BitmapSource.Create(1000, 1000, 96, 96, PixelFormats.Bgr32, null, backgroundImage, stride);
        }

        /// <summary>
        /// This method executes the specific matlab code of the vectorfield. The stream- or pathlines are handed to matlab.
        /// </summary>
        /// <param name="matlab">The instance of the matlab connection</param>
        public void executeMatlab(MLApp.MLApp matlab)
        {
            matlab.Execute("highNumberSamples = 0;");

            if (freshySimulated)
            {

                bool first = true;

                foreach (Line line in rungeLines)
                {
                    if (line.Points.Count > 0)
                    {

                        string t = Util.getLineMatrix(line);

                        if (first)
                        {
                            matlab.Execute("connections = [" + Util.getLineMatrix(line));
                            first = false;
                        }
                        else
                        {
                            matlab.Execute("connections = [connections; " + Util.getLineMatrix(line));
                        }
                    }
                }

                freshySimulated = false;
            }
        }
    }
}
