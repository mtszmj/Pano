using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.Scenes;

namespace Pano.Factories
{
    public class ScenesFactory
    {
        public SceneDto CreateDefaultScene()
        {
            var spotsFactory = new HotSpotFactory();
            var scene = new EquirectangularDto() { Title = "xyz" };
            scene.HotSpots.Add(spotsFactory.CreateSceneHotSpot(90, 90, "Spot1", "Scene1"));
            scene.HotSpots.Add(spotsFactory.CreateSceneHotSpot(-90, -90, "Spot2", "Scene2"));

            return scene;
        }
    }
}
