using System;

namespace Pano.Serialization.Model.HotSpots
{
    public interface IHotSpotDto : IEquatable<IHotSpotDto>
    {
        string CssClass { get; set; }
        string Id { get; }
        int Pitch { get; set; }
        string Text { get; set; }
        HotSpotType Type { get; }
        int Yaw { get; set; }
    }
}