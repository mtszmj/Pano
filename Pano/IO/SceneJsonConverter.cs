using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO
{
    public class SceneJsonConverter : JsonConverter
    {
        private const string TypePropertyName = "type";
        private readonly IJObjectParser parser;

        public SceneJsonConverter(IJObjectParser parser)
        {
            this.parser = parser;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IScene).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            //JObject jo = JObject.Load(reader);

            //var value = jo["type"].Value<string>().Normalize();
            //var hotSpotType = Enum.TryParse<PanoramaType>(value, out var result);

            var result = parser.TryParseEnum<PanoramaType>(reader, TypePropertyName);

            if (result == PanoramaType.Equirectangular)
                return parser.ToObject<Equirectangular>(reader, serializer);

            if (result == PanoramaType.Multires)
                return parser.ToObject<Multires>(reader, serializer);

            if (result == PanoramaType.Cubemap)
                return parser.ToObject<Cubemap>(reader, serializer);

            throw new ArgumentException("Incorrect conversion");
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
