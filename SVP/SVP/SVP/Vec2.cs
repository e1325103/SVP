using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP
{
    public class Vec2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vec2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vec2 operator *(Vec2 v, float scalar)
        {
            return new Vec2(v.X * scalar, v.Y * scalar);
        }

        public static Vec2 operator +(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vec2 operator /(Vec2 v, float scalar)
        {
            return new Vec2(v.X / scalar, v.Y / scalar);
        }

        public Vec2 normalize()
        {
            float rootsum = ((float)Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2)));
            return this / rootsum;
        }

        public Vec2 dot(Vec2 other)
        {
            return new Vec2(this.X * other.X, this.Y * other.Y);
        }
    }
}
