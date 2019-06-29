using System;

namespace Pano.Model
{
    public interface IHotSpot : IEquatable<IHotSpot>
    {
        string CssClass { get; set; }
        string Id { get; }
        int Pitch { get; set; }
        string Text { get; set; }
        HotSpotType Type { get; }
        int Yaw { get; set; }
    }
}