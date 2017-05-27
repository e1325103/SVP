using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP
{
    /// <summary>
    /// This class represents a 2 dim vector with x and y coordinates.
    /// </summary>
    public class Vec2
    {
        public float X { get; set; }
        public float Y { get; set; }

        /// <summary>
        /// The constructor simply stores the x and y coorindates.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public Vec2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// This method multiplies a 2 dim vector with a scalar.
        /// </summary>
        /// <param name="v">The vector to multiply</param>
        /// <param name="scalar">The scalar, which the vector gets multiplied with</param>
        /// <returns>Vector times scalar</returns>
        public static Vec2 operator *(Vec2 v, float scalar)
        {
            return new Vec2(v.X * scalar, v.Y * scalar);
        }

        /// <summary>
        /// This method sums up two vectors.
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <returns>Vector 1 + Vector 2</returns>
        public static Vec2 operator +(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// This method divides a 2 dim vector by a scalar.
        /// </summary>
        /// <param name="v">The vector to divide</param>
        /// <param name="scalar">The scalar, which the vector gets divided by</param>
        /// <returns>Vector times scalar</returns>
        public static Vec2 operator /(Vec2 v, float scalar)
        {
            return new Vec2(v.X / scalar, v.Y / scalar);
        }

        /// <summary>
        /// This method normalizes the vector.
        /// </summary>
        /// <returns>The normalized vector</returns>
        public Vec2 normalize()
        {
            float rootsum = ((float)Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2)));
            return this / rootsum;
        }

        /// <summary>
        /// This method calculates the dot product of the instance and another vector.
        /// </summary>
        /// <param name="other">The other vector for the calculation of the dot product</param>
        /// <returns>The dot product of this and the other vector</returns>
        public Vec2 dot(Vec2 other)
        {
            return new Vec2(this.X * other.X, this.Y * other.Y);
        }
    }
}
