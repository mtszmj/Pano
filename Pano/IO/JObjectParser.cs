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
        public T TryParseEnum<T>(JsonReader reader, string property) where T : struct, IComparable, IFormattable, IConvertible
        {
            JObject jo = JObject.Load(reader);
            var value = jo["type"].Value<string>().Normalize();
            var hotSpotType = Enum.TryParse<T>(value, out var result);

            return result;
        }
    }
}
