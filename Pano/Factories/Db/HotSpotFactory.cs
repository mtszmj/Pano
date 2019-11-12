using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model.Db.HotSpots;
using Pano.Model.Db.Scenes;

namespace Pano.Factories.Db
{
    public class HotSpotFactory : IHotSpotFactory
    {
        public HotSpot NewSceneHotSpot(Scene scene, Scene targetScene)
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));

            //TODO odblokowac
            //if (targetScene == null)
            //    throw new ArgumentNullException(nameof(targetScene));

            //if (scene == targetScene)
            //    throw new ArgumentException(nameof(targetScene));

            var spot = new Model.Db.HotSpots.SceneHotSpot()
            {
                Scene = scene,
                TargetScene = targetScene
            };

            return spot;
        }
    }
}
