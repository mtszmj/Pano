using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Factories
{
    public class HotSpotFactory
    {
        public HotSpot CreateDefaultHotSpot()
        {
            return new SceneHotSpot(GenerateDefaultId(), null);
        }

        public HotSpot CreateSceneHotSpot(int pitch, int yaw, string text, string sceneId)
        {
            return new SceneHotSpot(GenerateDefaultId(), sceneId)
            {
                Pitch = pitch,
                Yaw = yaw,
                Text = text,
            };
        }

        private string GenerateDefaultId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
