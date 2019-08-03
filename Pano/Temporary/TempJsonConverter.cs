using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pano.Temporary
{
    public class TempJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            var value = jo["type"].Value<string>().Normalize();
            Enum.TryParse<PanoramaType>(value, true, out var result);

            if (result == PanoramaType.Equirectangular)
            { 
//                return jo.CreateAndPopulateObject<TempEqui>(serializer);
                var pan = new TempEqui();
                serializer.Populate(jo.CreateReader(), pan);
                return pan;
            }
            if (result == PanoramaType.Cubemap)
                return jo.ToObject<TempCube>(serializer);

            throw new ArgumentException("Incorrect conversion");
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TempScene).IsAssignableFrom(objectType);
        }

        public override bool CanWrite => false;
    }
}
