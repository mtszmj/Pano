using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pano.IO
{
    public class JObjectParser : IJObjectParser
    {
        private JObject jo;

        public JObject Load(JsonReader reader)
        {
            return jo = JObject.Load(reader);
        }

        public TEnum TryParseEnum<TEnum>(JObject jo, string property)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
//            JObject jo = JObject.Load(reader);
            var value = jo[property].Value<string>().Normalize();
            Enum.TryParse<TEnum>(value, true, out var result);

            return result;
        }

        public T CreateAndPopulateObject<T>(JsonReader reader, Newtonsoft.Json.JsonSerializer serializer) where T : class, new()
        {
            var obj = new T();
            serializer.Populate(jo.CreateReader(), obj);
            return obj;

            //            JObject joo = JObject.Load(reader);
//            return jo.CreateAndPopulateObject<T>(serializer);
//            return serializer.Deserialize<T>(reader);

        }

        public T TryParse<T>(JObject jo, string property)
        {
//            JObject jo = JObject.Load(reader);
            var value = jo[property]?.Value<string>()?.Normalize();
            if (value is null)
            {
                return default;
            }

            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T) converter.ConvertFromString(value);
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }
    }
}