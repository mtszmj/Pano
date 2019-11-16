using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.HotSpots;

namespace Pano.Factories
{
    public class HotSpotFactory
    {
        public HotSpotDto CreateDefaultHotSpot()
        {
            return new SceneHotSpotDto(GenerateDefaultId(), null);
        }

        public HotSpotDto CreateSceneHotSpot(int pitch, int yaw, string text, string sceneId)
        {
            return new SceneHotSpotDto(GenerateDefaultId(), sceneId)
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
