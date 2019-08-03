using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO
{
    public class JsonSerializer : ISerializer
    {
        private const Formatting FORMATTING = Formatting.Indented;
        private readonly JsonSerializerSettings _Settings;

        //TODO zmienic na builder / factory
        public JsonSerializer(IEnumerable<JsonConverter> converters = null, JsonSerializerSettings settings = null)
        {
            _Settings = settings ?? new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()},
                Converters = new List<JsonConverter>
                    {new StringEnumConverter {NamingStrategy = new CamelCaseNamingStrategy()}},
            };

            foreach (var converter in converters ?? Enumerable.Empty<JsonConverter>())
            {
                _Settings.Converters.Add(converter);
            }
        }

        public T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str, _Settings);
        }

        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, FORMATTING, _Settings);
        }

        public static class Factory
        {
            public static JsonSerializer DefaultInstance()
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                    Converters = new List<JsonConverter>
                    {
                        new StringEnumConverter {NamingStrategy = new CamelCaseNamingStrategy()},
                        new HotSpotJsonConverter(new JObjectParser()),
                        new SceneJsonConverter(new JObjectParser())
                    },
                };

                return new JsonSerializer(null, settings);
            }
        }
    }
}