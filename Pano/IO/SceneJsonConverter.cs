using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.Scenes;

namespace Pano.IO
{
    public class SceneJsonConverter : JsonConverter
    {
        private const string TypePropertyName = "type";
        private const string FirstScenePropertyName = "firstScene";
        private readonly IJObjectParser parser;

        public SceneJsonConverter(IJObjectParser parser)
        {
            this.parser = parser;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ISceneDto).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jobject = parser.Load(reader);
            var result = parser.TryParseEnum<PanoramaType>(jobject, TypePropertyName);

            if (IsOfDefaultSceneType(jobject))
                return parser.CreateAndPopulateObject<DefaultSceneDto>(reader, serializer);

            if (result == PanoramaType.Equirectangular)
                return parser.CreateAndPopulateObject<EquirectangularDto>(reader, serializer);

            if (result == PanoramaType.Multires)
                return parser.CreateAndPopulateObject<MultiresDto>(reader, serializer);

            if (result == PanoramaType.Cubemap)
                return parser.CreateAndPopulateObject<CubemapDto>(reader, serializer);

            throw new ArgumentException("Incorrect conversion");
        }

        private bool IsOfDefaultSceneType(JObject jo)
        {
            var firstScene = parser.TryParse<string>(jo, FirstScenePropertyName);
            return !(firstScene is null);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
