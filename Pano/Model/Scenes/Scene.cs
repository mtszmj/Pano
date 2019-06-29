using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model
{
    public abstract class Scene : IScene
    {
        private Lazy<string> _Guid { get; } = new Lazy<string>(() => Guid.NewGuid().ToString());

        public string Id {
            get
            {
                if (Title == null)
                    return _Guid.Value.ToString();

                return System.Text.RegularExpressions.Regex.Replace(Title, @"[^0-9a-zA-Z]+", "");
            }
        }

        #region Properties

        /// <summary>
        /// This specifies the panorama type. Can be equirectangular, cubemap, or multires. 
        /// Defaults to equirectangular.
        /// </summary>
        public abstract PanoramaType Type { get; }

        /// <summary>
        /// If set, the value is displayed as the panorama’s title. If no title is desired, 
        /// don’t set this parameter.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// If set, the value is displayed as the panorama’s author. If no author is desired, 
        /// don’t set this parameter.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Allows user-facing strings to be changed / translated. See defaultConfig.strings 
        /// definition in pannellum.js for more details.
        /// </summary>
        public Dictionary<string, string> Strings { get; } = new Dictionary<string, string>();

        /// <summary>
        /// This specifies a base path to load the images from.
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// When set to true, the panorama will automatically load. When false, the user 
        /// needs to click on the load button to load the panorama. Defaults to false.
        /// </summary>
        public bool? AutoLoad { get; set; }

        /// <summary>
        /// Setting this parameter causes the panorama to automatically rotate when loaded. 
        /// The value specifies the rotation speed in degrees per second. Positive is 
        /// counter-clockwise, and negative is clockwise.
        /// </summary>
        public float? AutoRotate { get; set; }

        /// <summary>
        /// Sets the delay, in milliseconds, to start automatically rotating the panorama 
        /// after user activity ceases. This parameter only has an effect if the autoRotate 
        /// parameter is set.
        /// </summary>
        public int? AutoRotateInactivityDelay { get; set; }

        /// <summary>
        /// Sets the delay, in milliseconds, to stop automatically rotating the panorama 
        /// after it is loaded. This parameter only has an effect if the autoRotate parameter 
        /// is set.
        /// </summary>
        public int? AutoRotateStopDelay { get; set; }

        /// <summary>
        /// If set, the value is used as a URL for a fallback viewer in case Pannellum is not 
        /// supported by the user’s device. The user will be given the option to click a link 
        /// and visit this URL if Pannellum fails to work.
        /// </summary>
        public string Fallback { get; set; }

        /// <summary>
        /// If set to true, device orientation control will be used when the panorama is loaded, 
        /// if the device supports it. If false, device orientation control needs to be activated 
        /// by pressing a button. Defaults to false.
        /// </summary>
        public bool? OrientationOnByDefault { get; set; }

        /// <summary>
        /// If set to false, the zoom controls will not be displayed. Defaults to true.
        /// </summary>
        public bool? ShowZoomCtrl { get; set; }

        /// <summary>
        /// If set to false, zooming with keyboard will be disabled. Defaults to true.
        /// </summary>
        public bool? KeyboardZoom { get; set; }

        /// <summary>
        /// If set to false, zooming with mouse wheel will be disabled. Defaults to true. 
        /// Can also be set to fullscreenonly, in which case it is only enabled when the 
        /// viewer is fullscreen.
        /// </summary>
        public bool? MouseZoom { get; set; }

        /// <summary>
        /// If set to false, mouse and touch dragging is disabled. Defaults to true.
        /// </summary>
        public bool? Draggable { get; set; }

        /// <summary>
        /// If set to true, keyboard controls are disabled. Defaults to false.
        /// </summary>
        public bool? DisableKeyboardCtrl { get; set; }

        /// <summary>
        /// If set to false, the fullscreen control will not be displayed. Defaults to 
        /// true. The fullscreen button will only be displayed if the browser supports 
        /// the fullscreen API.
        /// </summary>
        public bool? ShowFullscreenCtrl { get; set; }

        /// <summary>
        /// If set to false, no controls are displayed. Defaults to true.
        /// </summary>
        public bool? ShowControls { get; set; }

        /// <summary>
        /// Sets the panorama’s starting yaw position in degrees. Defaults to 0.
        /// </summary>
        public int? Yaw { get; set; }

        /// <summary>
        /// Sets the panorama’s starting pitch position in degrees. Defaults to 0.
        /// </summary>
        public int? Pitch { get; set; }

        /// <summary>
        /// Sets the panorama’s starting horizontal field of view in degrees. 
        /// Defaults to 100.
        /// </summary>
        public int? Hfov { get; set; }

        /// <summary>
        /// Sets the minimum yaw the viewer edge can be at, in degrees. 
        /// Defaults to -180, i.e. no limit.
        /// </summary>
        public int? MinYaw { get; set; }

        /// <summary>
        /// Sets the maximum yaw the viewer edge can be at, in degrees. 
        /// Defaults to 180, i.e. no limit.
        /// </summary>
        public int? MaxYaw { get; set; }

        /// <summary>
        /// Sets the minimum pitch the viewer edge can be at, in degrees. 
        /// Defaults to undefined, so the viewer center can reach -90.
        /// </summary>
        public int? MinPitch { get; set; }

        /// <summary>
        /// Sets the maximum pitch the viewer edge can be at, in degrees. 
        /// Defaults to undefined, so the viewer center can reach 90.
        /// </summary>
        public int? MaxPitch { get; set; }

        /// <summary>
        /// Sets the minimum horizontal field of view, in degrees, that the viewer 
        /// can be set to. Defaults to 50.
        /// </summary>
        public int? MinHfov { get; set; }

        /// <summary>
        /// Sets the maximum horizontal field of view, in degrees, that the viewer 
        /// can be set to. Defaults to 50 / 120.
        /// </summary>
        public int? MaxHfov { get; set; }

        /// <summary>
        /// If true, a compass is displayed. Normally defaults to false; defaults to true 
        /// if heading information is present in Photo Sphere XMP metadata.
        /// </summary>
        public bool? Compass { get; set; }

        /// <summary>
        /// Set the offset, in degrees, of the center of the panorama from North. As this 
        /// affects the compass, it only has an effect if compass is set to true.
        /// </summary>
        public int? NorthOffset { get; set; }

        /// <summary>
        /// Specifies a URL for a preview image to display before the panorama is loaded.
        /// </summary>
        public string Preview { get; set; }

        /// <summary>
        /// Specifies the title to be displayed while the load button is displayed.
        /// </summary>
        public string PreviewTitle { get; set; }

        /// <summary>
        /// Specifies the author to be displayed while the load button is displayed.
        /// </summary>
        public string PreviewAuthor { get; set; }

        /// <summary>
        /// Specifies pitch of image horizon, in degrees (for correcting non-leveled panoramas).
        /// </summary>
        public int? HorizonPitch { get; set; }

        /// <summary>
        /// Specifies roll of image horizon, in degrees (for correcting non-leveled panoramas).
        /// </summary>
        public int? HorizonRoll { get; set; }

        //This specifies a timing function to be used for animating movements such as when 
        //the lookAt method is called. The default timing function is easeInOutQuad. If a 
        //custom function is specified, it should take a number [0, 1] as its only argument 
        //and return a number [0, 1].
        //animationTimingFunction (funtion) [API only]

        /// <summary>
        /// When true, HTML is escaped from configuration strings to help mitigate possible 
        /// DOM XSS attacks. This is always true when using the standalone viewer since the 
        /// configuration is provided via the URL; it defaults to false but can be set to 
        /// true when using the API.
        /// </summary>
        public bool? EscapeHTML { get; set; }

        /// <summary>
        /// This specifies the type of CORS request used and can be set to either anonymous 
        /// or use-credentials. Defaults to anonymous.
        /// </summary>
        public string CrossOrigin { get; set; }

        /// <summary>
        /// This specifies an array of hot spots that can be links to other scenes, 
        /// information, or external links. Each array element has the following properties.
        /// </summary>
        public List<IHotSpot> HotSpots { get; } = new List<IHotSpot>();

        /// <summary>
        /// When true, the mouse pointer’s pitch and yaw are logged to the console when 
        /// the mouse button is clicked. Defaults to false.
        /// </summary>
        public bool? HotSpotDebug { get; set; }

        /// <summary>
        /// Specifies the fade duration, in milliseconds, when transitioning between scenes. 
        /// Not defined by default. Only applicable for tours. Only works with WebGL renderer.
        /// </summary>
        public int? SceneFadeDuration { get; set; }

        /// <summary>
        /// The panorama source is considered dynamic when this is set to true. Defaults to false. 
        /// This should be set to true for video.
        /// </summary>
        /// <remarks>
        /// Currently, only equirectangular dynamic content is supported.
        /// </remarks>
        public bool? Dynamic { get; set; }

        #endregion

        #region Methods
        public void AddSceneHotSpot(IScene scene, int pitch = 0, int yaw = 0, int pitchBack = 0, int yawBack = 0)
        {
            var spot = new SceneHotSpot(Id, scene.Id);
            spot.Text = scene.Title;
            spot.Pitch = pitch;
            spot.Yaw = yaw;
            AddHotSpot(spot);

            spot = new SceneHotSpot(Id, this.Id);
            spot.Text = this.Title;
            spot.Pitch = pitchBack;
            spot.Yaw = yawBack;
            scene.AddHotSpot(spot);
        }

        public bool AddHotSpot(IHotSpot spot)
        {
            if (spot == null || HotSpots.Contains(spot))
                return false;

            HotSpots.Add(spot);
            return true;
        }

        public virtual bool Equals(IScene other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return other.Type == Type
                && other.Title == Title
                && other.Author == Author
                && other.BasePath == BasePath
                && other.AutoLoad == AutoLoad
                && other.AutoRotate == AutoRotate
                && other.AutoRotateInactivityDelay == AutoRotateInactivityDelay
                && other.AutoRotateStopDelay == AutoRotateStopDelay
                && other.Fallback == Fallback
                && other.OrientationOnByDefault == OrientationOnByDefault
                && other.ShowZoomCtrl == ShowZoomCtrl
                && other.KeyboardZoom == KeyboardZoom
                && other.MouseZoom == MouseZoom
                && other.Draggable == Draggable
                && other.DisableKeyboardCtrl == DisableKeyboardCtrl
                && other.ShowFullscreenCtrl == ShowFullscreenCtrl
                && other.ShowControls == ShowControls
                && other.Yaw == Yaw
                && other.Pitch == Pitch
                && other.Hfov == Hfov
                && other.MinYaw == MinYaw
                && other.MaxYaw == MaxYaw
                && other.MinPitch == MinPitch
                && other.MaxPitch == MaxPitch
                && other.MinHfov == MinHfov
                && other.MaxHfov == MaxHfov
                && other.Compass == Compass
                && other.NorthOffset == NorthOffset
                && other.Preview == Preview
                && other.PreviewTitle == PreviewTitle
                && other.PreviewAuthor == PreviewAuthor
                && other.HorizonPitch == HorizonPitch
                && other.HorizonRoll == HorizonRoll
                && other.EscapeHTML == EscapeHTML
                && other.CrossOrigin == CrossOrigin
                && other.HotSpotDebug == HotSpotDebug
                && other.SceneFadeDuration == SceneFadeDuration
                && other.Dynamic == Dynamic
                && other.Strings.OrderBy(kvp => kvp.Key)
                                .SequenceEqual(Strings.OrderBy(kvp => kvp.Key))
                && other.HotSpots.OrderBy(x => x.Id).SequenceEqual(HotSpots.OrderBy(x => x.Id), HotSpot.GetDefaultEqualityComparer())
                ;       
        }
        public static bool operator ==(Scene left, Scene right)
        {
            if (left is null)
            {
                return (right is null);
            }

            return (left.Equals(right));
        }

        public static bool operator !=(Scene left, Scene right)
        {
            return !(left == right);
        }
        #endregion
    }
}
