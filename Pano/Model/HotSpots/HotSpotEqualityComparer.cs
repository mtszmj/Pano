using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.HotSpots
{
    public class HotSpotEqualityComparer : IEqualityComparer<HotSpot>
    {
        public bool Equals(HotSpot x, HotSpot y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(HotSpot obj)
        {
            if (ReferenceEquals(obj, null))
                return 0;

            return obj.GetHashCode();
        }
    }
}
