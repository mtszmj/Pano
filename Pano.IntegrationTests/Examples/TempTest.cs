using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Pano.IO;
using Pano.Temporary;

namespace Pano.IntegrationTests.Examples
{
    [TestFixture]
    public class TempTest
    {
        [Test]
        public void TemporaryTest()
        {
            var equi = new TempEqui
            {
                Title = "title1",
                Author = "author1",
                Panorama = "panorama1"
            };

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() },
                    new TempJsonConverter()
                },
            };
            var json = JsonConvert.SerializeObject(equi, Formatting.Indented, settings);
            var deser = JsonConvert.DeserializeObject<TempEqui>(json, settings);

            TempScene equi2 = equi;
            json = JsonConvert.SerializeObject(equi2, Formatting.Indented, settings);
            TempScene deser2 = JsonConvert.DeserializeObject<TempScene>(json, settings);

        }
    }
}
