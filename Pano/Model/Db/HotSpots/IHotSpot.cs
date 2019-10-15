using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.Db.HotSpots
{
    public interface IHotSpot : IEquatable<IHotSpot>
    {
        string CssClass { get; set; }
        int Id { get; }
        int Pitch { get; set; }
        string Text { get; set; }
        HotSpotType Type { get; }
        int Yaw { get; set; }
    }
}
