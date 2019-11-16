using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Serialization.Model.HotSpots
{
    public class HotSpotEqualityComparer : IEqualityComparer<IHotSpotDto>
    {
        public bool Equals(IHotSpotDto x, IHotSpotDto y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(IHotSpotDto obj)
        {
            if (ReferenceEquals(obj, null))
                return 0;

            return obj.GetHashCode();
        }
    }
}
