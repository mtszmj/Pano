using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pano.IO;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.UnitTests.IO
{
    [TestFixture]
    public class HotSpotJsonConverterTests
    {
        [TestCase("test_id")]
        public void ReadJson_InfoHotSpot_ReturnCorrectObject(string id)
        {
            var mock = new Mock<IJObjectParser>();
            mock.Setup(x => x.TryParseEnum<HotSpotType>(It.IsAny<JObject>(), It.IsAny<string>()))
                .Returns(HotSpotType.Info);
            mock.Setup(x => x.CreateAndPopulateObject<InfoHotSpot>(It.IsAny<JsonReader>(), It.IsAny<Newtonsoft.Json.JsonSerializer>()))
                .Returns(new InfoHotSpot(id));

            var converter = new HotSpotJsonConverter(mock.Object);
            var obj = converter.ReadJson(null, null, null, null);

            Assert.Multiple(() =>
            {
                Assert.That(obj.GetType() == typeof(InfoHotSpot), Is.True);
                Assert.That(obj is null, Is.False);
            });
        }

        [TestCase("test_id", "test_scene_id")]
        public void ReadJson_SceneHotSpot_ReturnCorrectObject(string id, string sceneId)
        {
            var mock = new Mock<IJObjectParser>();
            mock.Setup(x => x.TryParseEnum<HotSpotType>(It.IsAny<JObject>(), It.IsAny<string>()))
                .Returns(HotSpotType.Scene);
            mock.Setup(x => x.CreateAndPopulateObject<SceneHotSpot>(It.IsAny<JsonReader>(), It.IsAny<Newtonsoft.Json.JsonSerializer>()))
                .Returns(new SceneHotSpot(id, sceneId));

            var converter = new HotSpotJsonConverter(mock.Object);
            var obj = converter.ReadJson(null, null, null, null);

            Assert.Multiple(() =>
            {
                Assert.That(obj.GetType() == typeof(SceneHotSpot));
                Assert.That(obj is null, Is.False);
            });
        }

        [Test]
        public void ReadJson_UnknownHotSpot_ReturnException()
        {
            var mock = new Mock<IJObjectParser>();
            mock.Setup(x => x.TryParseEnum<HotSpotType>(It.IsAny<JObject>(), It.IsAny<string>()))
                .Returns(null);

            var converter = new HotSpotJsonConverter(mock.Object);

            Assert.Throws<ArgumentException>(() => converter.ReadJson(null, null, null, null));
        }

        [Test]
        public void CanConvert_IsIHotSpot_ReturnsTrue()
        {
            var mock = new Mock<IHotSpot>();
            IHotSpot spot = mock.Object;

            var converter = new HotSpotJsonConverter(null);

            Assert.That(converter.CanConvert(spot.GetType()), Is.True);
        }

        [Test]
        public void CanConvert_IsNotIHotSpot_ReturnsFalse()
        {
            var mock = new Mock<object>();

            var converter = new HotSpotJsonConverter(null);

            Assert.That(converter.CanConvert(mock.Object.GetType()), Is.False);
        }

        [Test]
        public void CanWrite_AlwaysFalse()
        {
            var converter = new HotSpotJsonConverter(null);

            Assert.That(converter.CanWrite, Is.False);
        }

        [Test]
        public void WriteJson_NotImplementedException()
        {
            var converter = new HotSpotJsonConverter(null);

            Assert.Throws<NotImplementedException>(() => converter.WriteJson(null, null, null));
        }
    }
}
