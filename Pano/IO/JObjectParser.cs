using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pano.IO
{
    public class JObjectParser : IJObjectParser
    {
        public TEnum TryParseEnum<TEnum>(JsonReader reader, string property) where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            JObject jo = JObject.Load(reader);
            var value = jo[property].Value<string>().Normalize();
            var hotSpotType = Enum.TryParse<TEnum>(value, out var result);

            return result;
        }

        public T ToObject<T>(JsonReader reader, Newtonsoft.Json.JsonSerializer serializer) where T : class
        {
            JObject jo = JObject.Load(reader);
            return jo.ToObject<T>(serializer);
        }
    }
}
