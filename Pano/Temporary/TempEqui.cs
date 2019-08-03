using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Temporary
{
    public class TempEqui : TempScene
    {
        public override PanoramaType Type => PanoramaType.Equirectangular;

        public string Panorama { get; set; }
    }
}
