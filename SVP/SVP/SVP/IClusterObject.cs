using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP
{
    /// <summary>
    /// Interface for classes that include clustering (Vectorfield and Travel)
    /// </summary>
    public interface IClusterObject
    {
        /// <summary>
        /// This method executes the necessary individual matlab steps of the ClusterObjects (Vectorfield and Travel)
        /// </summary>
        /// <param name="matlab">Instance of the matlab connection</param>
        void executeMatlab(MLApp.MLApp matlab);

        /// <summary>
        /// This method transformes lines in order to be displayed correctly.
        /// </summary>
        /// <param name="lines">List of Lines to be transformed</param>
        /// <returns>List of transformed lines</returns>
        List<Line> transformLines(List<Line> lines);
    }
}
