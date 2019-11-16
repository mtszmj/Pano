using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;

namespace Pano.Serialization.Model.Scenes
{
    public class DefaultScene : Scene
    {
        [JsonIgnore] private string _firstScene = "";
        [JsonIgnore] private PanoramaType _type = PanoramaType.Unknown;

        public DefaultScene()
        {
            Hfov = 100;
            AutoLoad = true;
            AutoRotate = -0.5f;
            HotSpotDebug = true;
        }

        [JsonIgnore] public Scene FirstSceneRef { get; set; }

        public string FirstScene
        {
            get => FirstSceneRef?.Id ?? _firstScene;
            set
            {
                if (FirstSceneRef == null)
                    _firstScene = value;
            }
        }

        public override PanoramaType Type
        {
            get => FirstSceneRef?.Type ?? _type;
            protected set => _type = value;
        }

        public override bool Equals(IScene other)
        {
            if (!base.Equals(other))
                return false;

            if (!(other is DefaultScene scene))
                return false;

            return FirstScene?.Equals(scene.FirstScene) ?? false;
        }

    }
}