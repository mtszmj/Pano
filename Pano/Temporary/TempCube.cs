using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Temporary
{
    public class TempCube : TempScene
    {
        public override PanoramaType Type => PanoramaType.Cubemap;
        string[] CubeMap { get; set; } = new string[6];
    }
}
