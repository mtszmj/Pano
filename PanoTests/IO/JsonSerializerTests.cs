using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Pano.IO;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.HotSpots;

namespace Pano.IO.Tests
{
    [TestClass()]
    public class JsonSerializerTests
    {
        [TestMethod()]
        public void HotSpotSerializeTest()
        {
            HotSpotDto spot = new SceneHotSpotDto("zyx", "idid");
            spot.Text = "Hot Spot Title";
            spot.Pitch = 90;
            spot.Yaw = 10;

            var serializer = new JsonSerializer(new JsonConverter[] { new HotSpotJsonConverter(new JObjectParser()), new SceneJsonConverter(new JObjectParser()) });
            var json = serializer.Serialize(spot);

            HotSpotDto spotDes = //JsonConvert.DeserializeObject<HotSpotDto>(json, new HotSpotJsonConverter());
            serializer.Deserialize<HotSpotDto>(json);

            Assert.AreEqual(spot, spotDes);
        }
    }
}