using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model
{
    /// <summary>
    /// This specifies an array of hot spots that can be links to other scenes, information, or external links.
    /// Each array element has the following properties. 
    /// </summary>
    public class HotSpot : IPoint
    {
        /// <summary>
        /// Specifies the pitch portion of the hot spot’s location, in degrees.
        /// </summary>
        public int Pitch { get; set; }
        
        /// <summary>
        /// Specifies the yaw portion of the hot spot’s location, in degrees.
        /// </summary>
        public int Yaw { get; set; }

        /// <summary>
        /// Specifies the type of the hot spot. Can be scene for scene links or info for information 
        /// hot spots. A tour configuration file is required for scene hot spots.
        /// </summary>
        public HotSpotType Type { get; set; }

        /// <summary>
        /// This specifies the text that is displayed when the user hovers over the hot spot.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// If specified for an info hot spot, the hot spot links to the specified URL. Not applicable for 
        /// scene hot spots.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Specifies the ID of the scene to link to for scene hot spots. Not applicable for info hot spots.
        /// </summary>
        public string SceneId { get; set; }

        /// <summary>
        /// Specifies the pitch of the target scene, in degrees.Can also be set to same, which uses the 
        /// current pitch of the current scene as the initial pitch of the target scene.
        /// </summary>
        public int? TargetPitch { get; set; }

        /// <summary>
        /// Specifies the yaw of the target scene, in degrees.Can also be set to same or sameAzimuth. 
        /// These settings use the current yaw of the current scene as the initial yaw of the target scene; 
        /// same uses the current yaw directly, while sameAzimuth takes into account the northOffset values 
        /// of both scenes to maintain the same direction with regard to north.
        /// </summary>
        public int? TargetYaw { get; set; }

        /// <summary>
        /// Specifies the HFOV of the target scene, in degrees.
        /// </summary>
        public int? TargetHfov { get; set; }

        /// <summary>
        /// Specifies hot spot ID, for use with API’s removeHotSpot function.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// If specified, string is used as the CSS class for the hot spot instead of the default CSS classes.
        /// </summary>
        public string CssClass { get; set; }


        //createTooltipFunc(function) and createTooltipArgs(object)
        //If createTooltipFunc is specified, this function is used to create the hot spot tooltip 
        //DOM instead of the default function.The contents of createTooltipArgs are passed to the 
        //function as arguments.
        //clickHandlerFunc(function) and clickHandlerArgs(object)
        //If clickHandlerFunc is specified, this function is added as an event handler for the 
        //hot spot’s click event. The event object and the contents of clickHandlerArgs are passed 
        //to the function as arguments.
    }
}
