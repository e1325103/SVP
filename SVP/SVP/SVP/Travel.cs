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
    /// The travel class for the Clustering of the travels from Vienna implementing the IClusterObject interface.
    /// </summary>
    public class Travel : IClusterObject
    {
        /// <summary>
        /// This variable stores the Lines of the corresponding travels
        /// </summary>
        public List<Line> lines { get; set; }

        /// <summary>
        /// This method executes the necessary inital matlab command specific for the travels.
        /// </summary>
        /// <param name="matlab">The instance of the Matlab connection</param>
        public void executeMatlab(MLApp.MLApp matlab)
        {
            matlab.Execute("highNumberSamples = 1;");

            matlab.Execute("streamlines = connections';");
        }

        /// <summary>
        /// This method transformes the lines for the travels. No transformation is needed for this class.
        /// </summary>
        /// <param name="lines">The lines to be transformed</param>
        /// <returns>The transformed lines</returns>
        public List<Line> transformLines(List<Line> lines)
        {
            return lines;
        }
    }
}
