using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model
{
    internal interface IPoint
    {
        int Pitch { get; set; }
        int Yaw { get; set; }
    }
}
