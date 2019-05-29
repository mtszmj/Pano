using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model
{
    public class InfoHotSpot : HotSpot
    {
        public override HotSpotType Type => HotSpotType.Info;


        /// <summary>
        /// If specified for an info hot spot, the hot spot links to the specified URL. Not applicable for 
        /// scene hot spots.
        /// </summary>
        public string URL { get; set; }
    }
}
