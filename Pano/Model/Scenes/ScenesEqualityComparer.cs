using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.Scenes
{
    public class ScenesEqualityComparer : IEqualityComparer<Scene>
    {
        public bool Equals(Scene x, Scene y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(Scene obj)
        {
            if (ReferenceEquals(obj, null))
                return 0;

            return obj.GetHashCode();
        }
    }
}
