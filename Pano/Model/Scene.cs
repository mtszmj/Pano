using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model
{
    public class Scene
    {
        public string Id {
            get
            {
                if (Title == null)
                    return null;

                return System.Text.RegularExpressions.Regex.Replace(Title, @"[^0-9a-zA-Z]+", "");
            }
        }
        public PanoramaType Type { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Dictionary<string, string> Strings { get; } = new Dictionary<string, string>();
        public string BasePath { get; set; }
        public bool? AutoLoad { get; set; }
        public int? AutoRotate { get; set; }
        public int? AutoRotateInactivityDelay { get; set; }
        public int? AutoRotateStopDelay { get; set; }
        public string Fallback { get; set; }
        public string OrientationOnByDefault { get; set; }
        public bool? ShowZoomCtrl { get; set; }
        public bool? KeyboardZoom { get; set; }
        public bool? MouseZoom { get; set; }
        public bool? Draggable { get; set; }
        public bool? DisableKeyboardCtrl { get; set; }
        public bool? ShowFullscreenCtrl { get; set; }
        public bool? ShowControls { get; set; }
        public int? Yaw { get; set; }
        public int? Pitch { get; set; }
        public int? Hfov { get; set; }
        public int? MinYaw { get; set; }
        public int? MaxYaw { get; set; }
        public int? MinPitch { get; set; }
        public int? MaxPitch { get; set; }
        public int? MinHfov { get; set; }
        public int? MaxHfov { get; set; }
        public bool? Compass { get; set; }
        public int? NorthOffset { get; set; }
        public string Preview { get; set; }
        public string PreviewTitle { get; set; }
        public string PreviewAuthor { get; set; }
        public string HorizonPitch { get; set; }
        public string HorizonRoll { get; set; }
        public bool? EscapeHTML { get; set; }
        //public string CrossOrigin 
        public List<HotSpot> HotSpots { get; } = new List<HotSpot>();
        public bool? HotSpotDebug { get; set; }
        public int? SceneFadeDuration { get; set; }

        public void AddHotSpot(Scene scene, int pitch = 0, int yaw = 0, int pitchBack = 0, int yawBack = 0)
        {
            //TODO check if connection exists
            var spot = new SceneHotSpot(scene.Id);
            //spot.SceneId = scene.Id;
            spot.Text = scene.Title;
            spot.Pitch = pitch;
            spot.Yaw = yaw;
            this.HotSpots.Add(spot);

            spot = new SceneHotSpot(this.Id);
            //spot.SceneId = this.Id;
            spot.Text = this.Title;
            spot.Pitch = pitchBack;
            spot.Yaw = yawBack;
            scene.HotSpots.Add(spot);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return this.Equals(obj as Scene);
            }
        }

        public bool Equals(Scene other)
        {
            return this.Id == other.Id;
        }
    }
}
