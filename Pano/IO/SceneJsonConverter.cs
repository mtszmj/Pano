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
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Scene);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            var value = jo["type"].Value<string>().Normalize();
            var hotSpotType = Enum.TryParse<PanoramaType>(value, out var result);

            if (result == PanoramaType.Equirectangular)
                return jo.ToObject<Equirectangular>(serializer);

            if (result == PanoramaType.Multires)
                return jo.ToObject<Multires>(serializer);

            if (result == PanoramaType.Cubemap)
                return jo.ToObject<Cubemap>(serializer);

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
