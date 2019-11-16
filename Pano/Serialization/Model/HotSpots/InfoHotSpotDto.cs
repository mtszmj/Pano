using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Serialization.Model.HotSpots
{
    public class InfoHotSpotDto : HotSpotDto
    {
        public InfoHotSpotDto() : base() { }

        public InfoHotSpotDto(string id) : base(id) { }

        public override HotSpotType Type => HotSpotType.Info;

        /// <summary>
        /// If specified for an info hot spot, the hot spot links to the specified URL. Not applicable for 
        /// scene hot spots.
        /// </summary>
        public string URL { get; set; }

        public override bool Equals(IHotSpotDto obj)
        {
            if (!base.Equals(obj))
                return false;

            if (!(obj is InfoHotSpotDto scene))
                return false;

            return URL == scene.URL;
        }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((InfoHotSpotDto)obj);
        }
        public bool Equals(InfoHotSpotDto obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //if (GetHashCode() != obj.GetHashCode())
            //{
            //    return false;
            //}

            if (!base.Equals(obj))
            {
                return false;
            }

            return URL == obj.URL;
        }

        public static bool operator ==(InfoHotSpotDto left, InfoHotSpotDto right)
        {
            if (left is null)
            {
                return (right is null);
            }

            return (left.Equals(right));
        }

        public static bool operator !=(InfoHotSpotDto left, InfoHotSpotDto right)
        {
            return !(left == right);
        }
        #endregion
    }
}
