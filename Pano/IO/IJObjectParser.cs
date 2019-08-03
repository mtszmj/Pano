using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO
{
    public interface IJObjectParser
    {
        JObject Load(JsonReader reader);
        TEnum TryParseEnum<TEnum>(JObject jo, string property) where TEnum : struct, IComparable, IFormattable, IConvertible;
        T TryParse<T>(JObject jo, string property);
        T CreateAndPopulateObject<T>(JsonReader reader, Newtonsoft.Json.JsonSerializer serializer) where T : class, new();
    }
}
