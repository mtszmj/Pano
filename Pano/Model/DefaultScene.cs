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
        }

        [JsonIgnore]
        public Scene Scene { get; set; }
        public string FirstScene => Scene.Id;
    }
}
