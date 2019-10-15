using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.Db.Scenes
{
    public class DefaultScene //: Scene
    {
        public DefaultScene()
        {
            Hfov = 100;
            AutoLoad = true;
            AutoRotate = -0.5f;
            HotSpotDebug = true;
        }

        public virtual int DefaultSceneId { get; set; }
        public virtual Scene FirstSceneRef { get; set; }

        public virtual int? Hfov { get; set; }
        public virtual bool? AutoLoad { get; set; }
        public virtual float? AutoRotate { get; set; }
        public virtual bool? HotSpotDebug { get; set; }
        public virtual string Title { get; set; }

        // Navigation property
        public virtual TourForDb Tour { get; set; }

        //public override bool Equals(Scene other)
        //{
        //    if (!base.Equals(other))
        //        return false;

        //    if (!(other is DefaultScene scene))
        //        return false;

        //    return FirstSceneRef?.Equals(scene.FirstSceneRef) ?? false;
        //}
    }
}
