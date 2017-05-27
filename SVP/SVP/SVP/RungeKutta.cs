using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP
{
    /// <summary>
    /// Class for the Line Integration via Runge Kutta
    /// </summary>
    public class RungeKutta
    {
        private int numPoints;
        private int steps;
        private float delta;

        private Vec2 rectanglePos; 
        private Vec2 rectanglePos2;
        private VectorField field;

        private Random random;

        /// <summary>
        /// This variable stores the generated streamlines or pathlines
        /// </summary>
        public List<Line> lines;

        private float timePerStep;


        /// <summary>
        /// Constructor for the Streamline Runga Kutta generation
        /// </summary>
        /// <param name="numPoints">The number of seed that shall be considered</param>
        /// <param name="steps">The number of steps for a line - for each Seedpoints</param>
        /// <param name="delta">The number of how far a vector shall be followed</param>
        /// <param name="rectanglePos">One corner of a rectangle within the streamlines originate</param>
        /// <param name="rectanglePos2">Another corner of a rectangle within the streamlines originate</param>
        /// <param name="field">The corresponding vectorfield where the streamlines shall be integrated</param>
        public RungeKutta(int numPoints, int steps, float delta, Vec2 rectanglePos, Vec2 rectanglePos2, VectorField field)
        {
            this.numPoints = numPoints;
            this.steps = steps;
            this.delta = delta;

            this.rectanglePos = rectanglePos;
            this.rectanglePos2 = rectanglePos2;
            this.field = field;

            this.lines = new List<Line>();

            this.random = new Random();
        }

        /// <summary>
        /// Constructor for the Pathline Runga Kutta generation
        /// </summary>
        /// <param name="numPoints">The number of seed that shall be considered</param>
        /// <param name="steps">The number of steps for a line - for each Seedpoints</param>
        /// <param name="delta">The number of how far a vector shall be followed</param>
        /// <param name="rectanglePos">One corner of a rectangle within the pathlines originate</param>
        /// <param name="rectanglePos2">Another corner of a rectangle within the pathlines originate</param>
        /// <param name="field">The corresponding vectorfield where the pathlines shall be integrated</param>
        /// <param name="time">The time showing, how much time shall pass when calculating a step</param>
        public RungeKutta(int numSeeds, int steps, float delta, Vec2 rectanglePos, Vec2 rectanglePos2, VectorField field, float time)
        {
            this.numPoints = numSeeds;
            this.steps = steps;
            this.delta = delta;

            this.rectanglePos = rectanglePos;
            this.rectanglePos2 = rectanglePos2;
            this.field = field;

            this.lines = new List<Line>();

            this.random = new Random();

            this.timePerStep = time;
        }

        /// <summary>
        /// This method generates streamlines via Runge Kutta integration using the parameters handed by the constructor.
        /// The streamlines are saved in the class variable lines.
        /// </summary>
        public void generate()
        {
            bool outside = false;

            //float stepSize = 
              
            for (int i = 0; i < numPoints; i++)
            {
                Vec2 seed = generateSeedPoint();
                Line line = new Line();

                float x = seed.X;
                float y = seed.Y;

                if (isOutside(x, y))
                {
                    outside = true;
                }

                for (int j = 0; j < steps && !outside; j++)
                {
                    Vec2 lookAt = field.interpolateTrilinear(x, y, 0);

                    if (lookAt.X != 0 && lookAt.Y != 0)
                    {
                        lookAt = lookAt.normalize();

                        lookAt.X = x + lookAt.X * delta / 2;
                        lookAt.Y = y + lookAt.Y * delta / 2;

                        if (!isOutside(lookAt.X, lookAt.Y))
                        {
                            Vec2 actualDir = field.interpolateTrilinear(lookAt.X, lookAt.Y, 0);

                            if (actualDir.X != 0 && actualDir.Y != 0)
                            {
                                actualDir = actualDir.normalize();

                                x = x + actualDir.X * delta;
                                y = y + actualDir.Y * delta;

                                if (!isOutside(x, y))
                                {
                                    line.add(new Vec2(x, y));
                                }
                                else
                                {
                                    outside = true;
                                }
                            }
                            else
                            {
                                outside = true;
                            }
                        }
                        else
                        {
                            outside = true;
                        }
                    }
                    else
                    {
                        outside = true;
                    }
                }

                outside = false;

                lines.Add(line);
            }
        }

        /// <summary>
        /// This method checks, if a point lies outside of the vectorfield.
        /// </summary>
        /// <param name="x">X-Coordinate of a point</param>
        /// <param name="y">Y-Coordinate of a point</param>
        /// <returns>True or false, whether the point lies outside or inside the vectorfield</returns>
        private bool isOutside(float x, float y)
        {
            if (x < 0 || y < 0)
            {
                return true;
            }

            if (x >= (field.fieldSize - 2) || y >= (field.fieldSize - 2))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method generates a seedpoints via random placement in a rectangular area described by the two points rectanglePos and rectanglePos2
        /// </summary>
        /// <returns>A Vector containing the x and y cooridnates of the generated seedpoint</returns>
        public Vec2 generateSeedPoint()
        {            
            return new Vec2(
                random.Next(Math.Min(((int)rectanglePos.X), ((int)rectanglePos2.X)), Math.Max(((int)rectanglePos.X), ((int)rectanglePos2.X))),
                random.Next(Math.Min(((int)rectanglePos.Y), ((int)rectanglePos2.Y)), Math.Max(((int)rectanglePos.Y), ((int)rectanglePos2.Y)))
            );
        }

        /// <summary>
        /// This method generates pathlines via Runge Kutta integration using the parameters handed by the constructor.
        /// The streamlines are saved in the class variable lines.
        /// </summary>
        public  void generatePathLines()
        {
            bool outside = false;
            float currentTime = 0;

            //float stepSize = 

            for (int i = 0; i < numPoints; i++)
            {
                Vec2 seed = generateSeedPoint();
                Line line = new Line();

                currentTime = 0;

                float x = seed.X;
                float y = seed.Y;

                if (isOutside(x, y))
                {
                    outside = true;
                }

                for (int j = 0; j < steps && !outside; j++)
                {
                    Vec2 lookAt = field.interpolateTrilinear(x, y, currentTime);

                    if (lookAt.X != 0 && lookAt.Y != 0)
                    {
                        lookAt = lookAt.normalize();

                        lookAt.X = x + lookAt.X * delta / 2;
                        lookAt.Y = y + lookAt.Y * delta / 2;

                        if (!isOutside(lookAt.X, lookAt.Y))
                        {
                            Vec2 actualDir = field.interpolateTrilinear(lookAt.X, lookAt.Y, currentTime);

                            if (actualDir.X != 0 && actualDir.Y != 0)
                            {
                                actualDir = actualDir.normalize();

                                x = x + actualDir.X * delta;
                                y = y + actualDir.Y * delta;

                                currentTime += timePerStep;

                                if (currentTime > 47)
                                {
                                    outside = true;
                                }

                                if (!isOutside(x, y))
                                {
                                    line.add(new Vec2(x, y));
                                }
                                else
                                {
                                    outside = true;
                                }
                            }
                            else
                            {
                                outside = true;
                            }
                        }
                        else
                        {
                            outside = true;
                        }
                    }
                    else
                    {
                        outside = true;
                    }
                }

                outside = false;

                lines.Add(line);
            }
        }
    }
}
