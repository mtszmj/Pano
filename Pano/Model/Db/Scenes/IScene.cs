using System;
using System.Collections.Generic;
using Pano.Serialization.Model.HotSpots;

namespace Pano.Model.Db
{
    public interface IScene : IEquatable<IScene>
    {
        string Author { get; set; }
        bool? AutoLoad { get; set; }
        float? AutoRotate { get; set; }
        int? AutoRotateInactivityDelay { get; set; }
        int? AutoRotateStopDelay { get; set; }
        string BasePath { get; set; }
        bool? Compass { get; set; }
        string CrossOrigin { get; set; }
        bool? DisableKeyboardCtrl { get; set; }
        bool? Draggable { get; set; }
        bool? Dynamic { get; set; }
        bool? EscapeHTML { get; set; }
        string Fallback { get; set; }
        int? Hfov { get; set; }
        int? HorizonPitch { get; set; }
        int? HorizonRoll { get; set; }
        bool? HotSpotDebug { get; set; }
        ICollection<HotSpotDto> HotSpots { get; }
        int SceneId { get; set; }
        bool? KeyboardZoom { get; set; }
        int? MaxHfov { get; set; }
        int? MaxPitch { get; set; }
        int? MaxYaw { get; set; }
        int? MinHfov { get; set; }
        int? MinPitch { get; set; }
        int? MinYaw { get; set; }
        bool? MouseZoom { get; set; }
        int? NorthOffset { get; set; }
        bool? OrientationOnByDefault { get; set; }
        int? Pitch { get; set; }
        string Preview { get; set; }
        string PreviewAuthor { get; set; }
        string PreviewTitle { get; set; }
        int? SceneFadeDuration { get; set; }
        bool? ShowControls { get; set; }
        bool? ShowFullscreenCtrl { get; set; }
        bool? ShowZoomCtrl { get; set; }
        //Dictionary<string, string> Strings { get; } // TODO zamienic na klase
        string Title { get; set; }
        PanoramaType Type { get; }
        int? Yaw { get; set; }

        //void AddSceneHotSpot(IScene scene, int pitch = 0, int yaw = 0, int pitchBack = 0, int yawBack = 0);
        //bool AddHotSpot(IHotSpot spot);
    }
}