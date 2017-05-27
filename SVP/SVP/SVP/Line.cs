using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP
{
    /// <summary>
    /// A line class containing the points of it.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// The points of the line
        /// </summary>
        public List<Vec2> Points{get; set;}

        /// <summary>
        /// The default constructor initializes the list of points
        /// </summary>
        public Line()
        {
            Points = new List<Vec2>();
        }

        /// <summary>
        /// This method takes a point and adds it to the Line to the list of points
        /// </summary>
        /// <param name="point">Point to be added to the line</param>
        public void add(Vec2 point)
        {
            Points.Add(point);
        }
    }
}
