using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Pano.Factories;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.HotSpots;

namespace Pano.Factories.Tests
{
    [TestClass()]
    public class HotSpotFactoryTests
    {
        [TestMethod()]
        public void CreateDefaultHotSpotTest()
        {
            var factory = new HotSpotFactory();
            var hotSpot = factory.CreateDefaultHotSpot();

            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = contractResolver,
                Converters = new List<JsonConverter> { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },

            };

            var dict = new Dictionary<string, HotSpotDto>
            {
                ["pierwsza"] = hotSpot,
                ["druga"] = factory.CreateDefaultHotSpot()
            };

            string json = JsonConvert.SerializeObject(dict, Formatting.Indented, jsonSerializerSettings);
            Debug.Write(json);


            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void CreateSceneHotSpot()
        {
            var factory = new HotSpotFactory();

            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = contractResolver,
                Converters = new List<JsonConverter> { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },

            };

            var list = new List<HotSpotDto>
            {
                factory.CreateSceneHotSpot(90, 90, "test", "testScena"),
                factory.CreateSceneHotSpot(90, 90, "test2", "testScena")
            };

            string json = JsonConvert.SerializeObject(list, Formatting.Indented, jsonSerializerSettings);
            Debug.Write(json);


            Assert.IsTrue(true);
        }
    }
}