using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP
{
    public class RungeKutta
    {
        private int numPoints;
        private int steps;
        private float delta;

        private Vec2 rectanglePos; // Rectangular Area to generate Seedpoints
        private Vec2 rectanglePos2;
        private VectorField field;

        private Random random;

        public List<Line> lines;

        private float timePerStep;

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

        private bool isOutside(float x, float y)
        {
            if (x < 0 || y < 0)
            {
                return true;
            }

            if (x >= (field.size - 2) || y >= (field.size - 2))
            {
                return true;
            }

            return false;
        }

        public Vec2 generateSeedPoint()
        {            
            return new Vec2(
                random.Next(((int)rectanglePos.X), ((int)rectanglePos2.X)),
                random.Next(((int)rectanglePos.Y), ((int)rectanglePos2.Y))
            );
        }

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
