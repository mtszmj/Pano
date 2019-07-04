using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Pano.IO;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO.Tests
{
    [TestClass()]
    public class JsonSerializerTests
    {
        [TestMethod()]
        public void HotSpotSerializeTest()
        {
            HotSpot spot = new SceneHotSpot("zyx", "idid");
            spot.Text = "Hot Spot Title";
            spot.Pitch = 90;
            spot.Yaw = 10;

            var serializer = new JsonSerializer(new JsonConverter[] { new HotSpotJsonConverter(new JObjectParser()), new SceneJsonConverter() });
            var json = serializer.Serialize(spot);

            HotSpot spotDes = //JsonConvert.DeserializeObject<HotSpot>(json, new HotSpotJsonConverter());
            serializer.Deserialize<HotSpot>(json);

            Assert.AreEqual(spot, spotDes);
        }
    }
}