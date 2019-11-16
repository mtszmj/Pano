using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.Db.HotSpots
{
    public class SceneHotSpot : HotSpot
    {
        public SceneHotSpot() : base() { }

        public override HotSpotType Type { get; protected set; } = HotSpotType.Scene;

        /// <summary>
        /// Specifies the ID of the scene to link to for scene hot spots. Not applicable for info hot spots.
        /// </summary>
        public int? TargetSceneId { get; set; }
        public Model.Db.Scenes.Scene TargetScene { get; set; }

        /// <summary>
        /// Specifies the pitch of the target scene, in degrees. Can also be set to same, which uses the 
        /// current pitch of the current scene as the initial pitch of the target scene.
        /// </summary>
        public int? TargetPitch { get; set; }

        /// <summary>
        /// Specifies the yaw of the target scene, in degrees. Can also be set to same or sameAzimuth. 
        /// These settings use the current yaw of the current scene as the initial yaw of the target scene; 
        /// same uses the current yaw directly, while sameAzimuth takes into account the northOffset values 
        /// of both scenes to maintain the same direction with regard to north.
        /// </summary>
        public int? TargetYaw { get; set; }

        /// <summary>
        /// Specifies the HFOV of the target scene, in degrees.
        /// </summary>
        public int? TargetHfov { get; set; }

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

            return Equals((SceneHotSpot)obj);
        }

        public override bool Equals(IHotSpot obj)
        {
            if (!base.Equals(obj))
                return false;

            if (!(obj is SceneHotSpot scene))
                return false;

            return Scene.SceneId == scene.Scene.SceneId
                && TargetPitch == scene.TargetPitch
                && TargetYaw == scene.TargetYaw
                && TargetHfov == scene.TargetHfov;
        }

        public bool Equals(SceneHotSpot obj)
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

            return Scene.SceneId == obj.Scene.SceneId
                && TargetPitch == obj.TargetPitch
                && TargetYaw == obj.TargetYaw
                && TargetHfov == obj.TargetHfov;
        }

        public static bool operator ==(SceneHotSpot left, SceneHotSpot right)
        {
            if (left is null)
            {
                return (right is null);
            }

            return (left.Equals(right));
        }

        public static bool operator !=(SceneHotSpot left, SceneHotSpot right)
        {
            return !(left == right);
        }
        #endregion
    }
}
