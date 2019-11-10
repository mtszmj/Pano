using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using Pano.Model.Db.Helpers;

namespace Pano.Model.Db.Scenes
{
    public abstract class Scene : ObservableObject //: IScene
    {
        public int SceneId { get; set; }

        public int TourId { get; set; }
        public TourForDb Tour { get; set; } // navigation property

        /// <summary>
        /// This specifies the panorama type. Can be equirectangular, cubemap, or multires. 
        /// Defaults to equirectangular.
        /// </summary>
        public virtual PanoramaType Type { get; protected set; }

        protected string _title;

        /// <summary>
        /// If set, the value is displayed as the panorama’s title. If no title is desired, 
        /// don’t set this parameter.
        /// </summary>
        public virtual string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        /// <summary>
        /// If set, the value is displayed as the panorama’s author. If no author is desired, 
        /// don’t set this parameter.
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// Allows user-facing strings to be changed / translated. See defaultConfig.strings 
        /// definition in pannellum.js for more details.
        /// </summary>
        public ObservableCollection<StringDictionaryEntry> Strings { get; set; } = new ObservableCollection<StringDictionaryEntry>();

        /// <summary>
        /// This specifies a base path to load the images from.
        /// </summary>
        public virtual string BasePath { get; set; }

        /// <summary>
        /// When set to true, the panorama will automatically load. When false, the user 
        /// needs to click on the load button to load the panorama. Defaults to false.
        /// </summary>
        public virtual bool? AutoLoad { get; set; }

        /// <summary>
        /// Setting this parameter causes the panorama to automatically rotate when loaded. 
        /// The value specifies the rotation speed in degrees per second. Positive is 
        /// counter-clockwise, and negative is clockwise.
        /// </summary>
        public virtual float? AutoRotate { get; set; }

        /// <summary>
        /// Sets the delay, in milliseconds, to start automatically rotating the panorama 
        /// after user activity ceases. This parameter only has an effect if the autoRotate 
        /// parameter is set.
        /// </summary>
        public virtual int? AutoRotateInactivityDelay { get; set; }

        /// <summary>
        /// Sets the delay, in milliseconds, to stop automatically rotating the panorama 
        /// after it is loaded. This parameter only has an effect if the autoRotate parameter 
        /// is set.
        /// </summary>
        public virtual int? AutoRotateStopDelay { get; set; }

        /// <summary>
        /// If set, the value is used as a URL for a fallback viewer in case Pannellum is not 
        /// supported by the user’s device. The user will be given the option to click a link 
        /// and visit this URL if Pannellum fails to work.
        /// </summary>
        public virtual string Fallback { get; set; }

        /// <summary>
        /// If set to true, device orientation control will be used when the panorama is loaded, 
        /// if the device supports it. If false, device orientation control needs to be activated 
        /// by pressing a button. Defaults to false.
        /// </summary>
        public virtual bool? OrientationOnByDefault { get; set; }

        /// <summary>
        /// If set to false, the zoom controls will not be displayed. Defaults to true.
        /// </summary>
        public virtual bool? ShowZoomCtrl { get; set; }

        /// <summary>
        /// If set to false, zooming with keyboard will be disabled. Defaults to true.
        /// </summary>
        public virtual bool? KeyboardZoom { get; set; }

        /// <summary>
        /// If set to false, zooming with mouse wheel will be disabled. Defaults to true. 
        /// Can also be set to fullscreenonly, in which case it is only enabled when the 
        /// viewer is fullscreen.
        /// </summary>
        public virtual bool? MouseZoom { get; set; }

        /// <summary>
        /// If set to false, mouse and touch dragging is disabled. Defaults to true.
        /// </summary>
        public virtual bool? Draggable { get; set; }

        /// <summary>
        /// If set to true, keyboard controls are disabled. Defaults to false.
        /// </summary>
        public virtual bool? DisableKeyboardCtrl { get; set; }

        /// <summary>
        /// If set to false, the fullscreen control will not be displayed. Defaults to 
        /// true. The fullscreen button will only be displayed if the browser supports 
        /// the fullscreen API.
        /// </summary>
        public virtual bool? ShowFullscreenCtrl { get; set; }

        /// <summary>
        /// If set to false, no controls are displayed. Defaults to true.
        /// </summary>
        public virtual bool? ShowControls { get; set; }

        /// <summary>
        /// Sets the panorama’s starting yaw position in degrees. Defaults to 0.
        /// </summary>
        public virtual int? Yaw { get; set; }

        /// <summary>
        /// Sets the panorama’s starting pitch position in degrees. Defaults to 0.
        /// </summary>
        public virtual int? Pitch { get; set; }

        /// <summary>
        /// Sets the panorama’s starting horizontal field of view in degrees. 
        /// Defaults to 100.
        /// </summary>
        public virtual int? Hfov { get; set; }

        /// <summary>
        /// Sets the minimum yaw the viewer edge can be at, in degrees. 
        /// Defaults to -180, i.e. no limit.
        /// </summary>
        public virtual int? MinYaw { get; set; }

        /// <summary>
        /// Sets the maximum yaw the viewer edge can be at, in degrees. 
        /// Defaults to 180, i.e. no limit.
        /// </summary>
        public virtual int? MaxYaw { get; set; }

        /// <summary>
        /// Sets the minimum pitch the viewer edge can be at, in degrees. 
        /// Defaults to undefined, so the viewer center can reach -90.
        /// </summary>
        public virtual int? MinPitch { get; set; }

        /// <summary>
        /// Sets the maximum pitch the viewer edge can be at, in degrees. 
        /// Defaults to undefined, so the viewer center can reach 90.
        /// </summary>
        public virtual int? MaxPitch { get; set; }

        /// <summary>
        /// Sets the minimum horizontal field of view, in degrees, that the viewer 
        /// can be set to. Defaults to 50.
        /// </summary>
        public virtual int? MinHfov { get; set; }

        /// <summary>
        /// Sets the maximum horizontal field of view, in degrees, that the viewer 
        /// can be set to. Defaults to 50 / 120.
        /// </summary>
        public virtual int? MaxHfov { get; set; }

        /// <summary>
        /// If true, a compass is displayed. Normally defaults to false; defaults to true 
        /// if heading information is present in Photo Sphere XMP metadata.
        /// </summary>
        public virtual bool? Compass { get; set; }

        /// <summary>
        /// Set the offset, in degrees, of the center of the panorama from North. As this 
        /// affects the compass, it only has an effect if compass is set to true.
        /// </summary>
        public virtual int? NorthOffset { get; set; }

        /// <summary>
        /// Specifies a URL for a preview image to display before the panorama is loaded.
        /// </summary>
        public virtual string Preview { get; set; }

        /// <summary>
        /// Specifies the title to be displayed while the load button is displayed.
        /// </summary>
        public virtual string PreviewTitle { get; set; }

        /// <summary>
        /// Specifies the author to be displayed while the load button is displayed.
        /// </summary>
        public virtual string PreviewAuthor { get; set; }

        /// <summary>
        /// Specifies pitch of image horizon, in degrees (for correcting non-leveled panoramas).
        /// </summary>
        public virtual int? HorizonPitch { get; set; }

        /// <summary>
        /// Specifies roll of image horizon, in degrees (for correcting non-leveled panoramas).
        /// </summary>
        public virtual int? HorizonRoll { get; set; }

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
        public virtual bool? EscapeHTML { get; set; }

        /// <summary>
        /// This specifies the type of CORS request used and can be set to either anonymous 
        /// or use-credentials. Defaults to anonymous.
        /// </summary>
        public virtual string CrossOrigin { get; set; }

        /// <summary>
        /// This specifies an array of hot spots that can be links to other scenes, 
        /// information, or external links. Each array element has the following properties.
        /// </summary>
        public ObservableCollection<Model.Db.HotSpots.HotSpot> HotSpots { get; } = new ObservableCollection<Model.Db.HotSpots.HotSpot>();

        /// <summary>
        /// When true, the mouse pointer’s pitch and yaw are logged to the console when 
        /// the mouse button is clicked. Defaults to false.
        /// </summary>
        public virtual bool? HotSpotDebug { get; set; }

        /// <summary>
        /// Specifies the fade duration, in milliseconds, when transitioning between scenes. 
        /// Not defined by default. Only applicable for tours. Only works with WebGL renderer.
        /// </summary>
        public virtual int? SceneFadeDuration { get; set; }

        /// <summary>
        /// The panorama source is considered dynamic when this is set to true. Defaults to false. 
        /// This should be set to true for video.
        /// </summary>
        /// <remarks>
        /// Currently, only equirectangular dynamic content is supported.
        /// </remarks>
        public virtual bool? Dynamic { get; set; }

        //public void AddSceneHotSpot(IScene scene, int pitch = 0, int yaw = 0, int pitchBack = 0, int yawBack = 0)
        //{
        //    if(scene == null)
        //    {
        //        throw new ArgumentNullException(nameof(scene));
        //    }

        //    var spot = new SceneHotSpot(Id, scene.Id)
        //    {
        //        Text = scene.Title,
        //        Pitch = pitch,
        //        Yaw = yaw
        //    };
        //    AddHotSpot(spot);

        //    spot = new SceneHotSpot(Id, this.Id)
        //    {
        //        Text = this.Title,
        //        Pitch = pitchBack,
        //        Yaw = yawBack
        //    };
        //    scene.AddHotSpot(spot);
        //}

        public bool AddHotSpot(Model.Db.HotSpots.HotSpot spot)
        {
            if (spot == null || HotSpots.Contains(spot))
                return false;

            HotSpots.Add(spot);
            return true;
        }

        public bool DeleteHotSpot(HotSpots.HotSpot spot)
        {
            return HotSpots.Remove(spot);
        }

        public virtual bool Equals(Scene other)
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
                //&& other.Strings.OrderBy(kvp => kvp.Key)
                //                .SequenceEqual(Strings.OrderBy(kvp => kvp.Key)) //TODO
                //&& other.HotSpots.OrderBy(x => x.Id).SequenceEqual(HotSpots.OrderBy(x => x.Id), HotSpot.GetDefaultEqualityComparer())
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
    }
}
