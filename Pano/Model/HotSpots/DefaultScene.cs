using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model
{
    public class DefaultScene : Scene
    {
        public DefaultScene()
        {
            Hfov = 100;
            AutoLoad = true;
            AutoRotate = -1;
            AutoRotateInactivityDelay = 10000;
        }

        [JsonIgnore]
        public Scene FirstSceneRef { get; set; }
        public string FirstScene => FirstSceneRef?.Id;
        public override PanoramaType Type => FirstSceneRef?.Type ?? PanoramaType.Equirectangular;

    }
}
