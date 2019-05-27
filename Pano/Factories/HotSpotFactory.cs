using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Factories
{
    public class HotSpotFactory
    {
        public HotSpot CreateDefaultHotSpot()
        {
            return new HotSpot();
        }
    }
}
