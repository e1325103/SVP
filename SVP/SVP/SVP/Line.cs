using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP
{
    public class Line
    {
        public List<Vec2> Points{get; set;}

        public Line()
        {
            Points = new List<Vec2>();
        }

        public void add(Vec2 point)
        {
            Points.Add(point);
        }
    }
}
