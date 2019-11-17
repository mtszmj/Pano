using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Serialization.Model.Scenes
{
    public class ScenesEqualityComparer : IEqualityComparer<ISceneDto>
    {
        public bool Equals(ISceneDto x, ISceneDto y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(ISceneDto obj)
        {
            if (ReferenceEquals(obj, null))
                return 0;

            return obj.GetHashCode();
        }
    }
}
