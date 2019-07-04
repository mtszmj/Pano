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
        [Test]
        public void ReadJson_InfoHotSpot_ReturnCorrectObject()
        {
            throw new NotImplementedException();
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
