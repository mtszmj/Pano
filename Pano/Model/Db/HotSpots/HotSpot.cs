using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model.HotSpots;

namespace Pano.Model.Db.HotSpots
{
    public abstract class HotSpot : IHotSpot
    {
        protected HotSpot() { }

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
        public abstract HotSpotType Type { get; protected set; }

        /// <summary>
        /// This specifies the text that is displayed when the user hovers over the hot spot.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Specifies hot spot ID, for use with API’s removeHotSpot function.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// If specified, string is used as the CSS class for the hot spot instead of the default CSS classes.
        /// </summary>
        public string CssClass { get; set; }

        public int SceneId { get; set; }
        public Model.Db.Scenes.Scene Scene { get; set; }

        //createTooltipFunc(function) and createTooltipArgs(object)
        //If createTooltipFunc is specified, this function is used to create the hot spot tooltip 
        //DOM instead of the default function.The contents of createTooltipArgs are passed to the 
        //function as arguments.
        //clickHandlerFunc(function) and clickHandlerArgs(object)
        //If clickHandlerFunc is specified, this function is added as an event handler for the 
        //hot spot’s click event. The event object and the contents of clickHandlerArgs are passed 
        //to the function as arguments.

        //public static IEqualityComparer<IHotSpot> GetDefaultEqualityComparer() => new HotSpotEqualityComparer();

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((IHotSpot)obj);
        }
        public virtual bool Equals(IHotSpot obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            //if (GetHashCode() != obj.GetHashCode())
            //{
            //    return false;
            //}

            //if (!base.Equals(obj))
            //{
            //    return false;
            //}

            return Pitch == obj.Pitch
                && Yaw == obj.Yaw
                && Type == obj.Type
                && Text == obj.Text
                && Id == obj.Id
                && CssClass == obj.CssClass;
        }

        public static bool operator ==(HotSpot left, HotSpot right)
        {
            if (left is null)
            {
                return (right is null);
            }

            return (left.Equals(right));
        }

        public static bool operator !=(HotSpot left, HotSpot right)
        {
            return !(left == right);
        }
        #endregion

    }
}
