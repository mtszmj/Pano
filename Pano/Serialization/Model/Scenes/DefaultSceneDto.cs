using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;

namespace Pano.Serialization.Model.Scenes
{
    public class DefaultSceneDto : SceneDto
    {
        [JsonIgnore] private string _firstScene = "";
        [JsonIgnore] private PanoramaType _type = PanoramaType.Unknown;

        public DefaultSceneDto()
        {
            Hfov = 100;
            AutoLoad = true;
            AutoRotate = -0.5f;
            HotSpotDebug = true;
        }

        [JsonIgnore] public SceneDto FirstSceneDtoRef { get; set; }

        public string FirstScene
        {
            get => FirstSceneDtoRef?.Id ?? _firstScene;
            set
            {
                if (FirstSceneDtoRef == null)
                    _firstScene = value;
            }
        }

        public override PanoramaType Type
        {
            get => FirstSceneDtoRef?.Type ?? _type;
            protected set => _type = value;
        }

        public override bool Equals(ISceneDto other)
        {
            if (!base.Equals(other))
                return false;

            if (!(other is DefaultSceneDto scene))
                return false;

            return FirstScene?.Equals(scene.FirstScene) ?? false;
        }

    }
}