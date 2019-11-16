using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.HotSpots;

namespace Pano.IO
{
    public class HotSpotJsonConverter : JsonConverter
    {
        private const string TypePropertyName = "type";
        private readonly IJObjectParser parser;

        public HotSpotJsonConverter(IJObjectParser parser) {
            this.parser = parser;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IHotSpotDto).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jobject = parser.Load(reader);
            var result = parser.TryParseEnum<HotSpotType>(jobject, TypePropertyName);

            if (result == HotSpotType.Info)
                return parser.CreateAndPopulateObject<InfoHotSpotDto>(reader, serializer);

            if (result == HotSpotType.Scene)
                return parser.CreateAndPopulateObject<SceneHotSpotDto>(reader, serializer);

            throw new ArgumentException("Incorrect conversion");
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
