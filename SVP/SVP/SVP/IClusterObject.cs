using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP
{
    public interface IClusterObject
    {
        void executeMatlab(MLApp.MLApp matlab);
        List<Line> transformLines(List<Line> lines);
    }
}
