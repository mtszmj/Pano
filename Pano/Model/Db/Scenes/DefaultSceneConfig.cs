using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.Db.Scenes
{
    public class DefaultSceneConfig
    {
        public DefaultSceneConfig()
        {
            Hfov = 100;
            AutoLoad = true;
            AutoRotate = -0.5f;
            HotSpotDebug = true;
        }
        public int Id { get; set; }

        public TourForDb Tour { get; set; }

        public int? FirstSceneId { get; set; }
        public virtual Scene FirstScene { get; set; }
        
        public int? Hfov { get; set; }
        public bool? AutoLoad { get; set; }
        public float? AutoRotate { get; set; }
        public bool? HotSpotDebug { get; set; }
        public string Title { get; set; }
        
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
