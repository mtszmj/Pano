﻿using Moq;
using Newtonsoft.Json;
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
    public class SceneJsonConverterTests
    {
        [TestCase("test_id")]
        public void ReadJson_CubemapPanorama_ReturnCorrectObject(string id)
        {
            var mock = new Mock<IJObjectParser>();
            mock.Setup(x => x.TryParseEnum<PanoramaType>(It.IsAny<JsonReader>(), It.IsAny<string>()))
                .Returns(PanoramaType.Cubemap);
            mock.Setup(x => x.ToObject<Cubemap>(It.IsAny<JsonReader>(), It.IsAny<Newtonsoft.Json.JsonSerializer>()))
                .Returns(new Cubemap());

            var converter = new SceneJsonConverter(mock.Object);
            var obj = converter.ReadJson(null, null, null, null);

            Assert.Multiple(() =>
            {
                Assert.That(obj.GetType() == typeof(Cubemap), Is.True);
                Assert.That(obj is null, Is.False);
            });
        }

        [TestCase("test_id", "test_scene_id")]
        public void ReadJson_EquirectangularPanorama_ReturnCorrectObject(string id, string sceneId)
        {
            var mock = new Mock<IJObjectParser>();
            mock.Setup(x => x.TryParseEnum<PanoramaType>(It.IsAny<JsonReader>(), It.IsAny<string>()))
                .Returns(PanoramaType.Equirectangular);
            mock.Setup(x => x.ToObject<Equirectangular>(It.IsAny<JsonReader>(), It.IsAny<Newtonsoft.Json.JsonSerializer>()))
                .Returns(new Equirectangular());

            var converter = new SceneJsonConverter(mock.Object);
            var obj = converter.ReadJson(null, null, null, null);

            Assert.Multiple(() =>
            {
                Assert.That(obj.GetType() == typeof(Equirectangular), Is.True);
                Assert.That(obj is null, Is.False);
            });
        }

        [TestCase("test_id", "test_scene_id")]
        public void ReadJson_MultiresPanorama_ReturnCorrectObject(string id, string sceneId)
        {
            var mock = new Mock<IJObjectParser>();
            mock.Setup(x => x.TryParseEnum<PanoramaType>(It.IsAny<JsonReader>(), It.IsAny<string>()))
                .Returns(PanoramaType.Multires);
            mock.Setup(x => x.ToObject<Multires>(It.IsAny<JsonReader>(), It.IsAny<Newtonsoft.Json.JsonSerializer>()))
                .Returns(new Multires());

            var converter = new SceneJsonConverter(mock.Object);
            var obj = converter.ReadJson(null, null, null, null);

            Assert.Multiple(() =>
            {
                Assert.That(obj.GetType() == typeof(Multires), Is.True);
                Assert.That(obj is null, Is.False);
            });
        }

        [Test]
        public void ReadJson_UnknownHotSpot_ReturnException()
        {
            var mock = new Mock<IJObjectParser>();
            mock.Setup(x => x.TryParseEnum<PanoramaType>(It.IsAny<JsonReader>(), It.IsAny<string>()))
                .Returns(null);

            var converter = new SceneJsonConverter(mock.Object);

            Assert.Throws<ArgumentException>(() => converter.ReadJson(null, null, null, null));
        }

        [Test]
        public void CanConvert_IsIScene_ReturnsTrue()
        {
            var mock = new Mock<IScene>();
            IScene spot = mock.Object;

            var converter = new SceneJsonConverter(null);

            Assert.That(converter.CanConvert(spot.GetType()), Is.True);
        }

        [Test]
        public void CanConvert_IsNotIScene_ReturnsFalse()
        {
            var mock = new Mock<object>();

            var converter = new SceneJsonConverter(null);

            Assert.That(converter.CanConvert(mock.Object.GetType()), Is.False);
        }

        [Test]
        public void CanWrite_AlwaysFalse()
        {
            var converter = new SceneJsonConverter(null);

            Assert.That(converter.CanWrite, Is.False);
        }

        [Test]
        public void WriteJson_NotImplementedException()
        {
            var converter = new SceneJsonConverter(null);

            Assert.Throws<NotImplementedException>(() => converter.WriteJson(null, null, null));
        }
    }
}
