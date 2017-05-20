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
        public ArrayList Points{get; set;}

        public Line()
        {
            Points = new ArrayList();
        }

        public void add(Vec2 point)
        {
            Points.Add(point);
        }
    }
}
