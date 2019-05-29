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

namespace Pano.Factories.Tests
{
    [TestClass()]
    public class ScenesFactoryTests
    {
        [TestMethod()]
        public void CreateDefaultSceneTest()
        {
            var factory = new ScenesFactory();
            var hotSpot = factory.CreateDefaultScene();

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

            var dict = new Dictionary<string, Scene>
            {
                ["pierwsza"] = hotSpot,
                ["druga"] = factory.CreateDefaultScene()
            };

            string json = JsonConvert.SerializeObject(dict, Formatting.Indented, jsonSerializerSettings);
            Debug.Write(json);


            Assert.IsTrue(true);
        }
    }
}