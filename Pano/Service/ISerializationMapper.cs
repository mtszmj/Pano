using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Serialization.Model;

namespace Pano.Service
{
    public interface ISerializationMapper
    {
        TDest Map<TSource, TDest>(TSource tour);
    }
}
