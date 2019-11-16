using NUnit.Framework;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.HotSpots;

namespace Pano.UnitTests.Model.HotSpots
{
    [TestFixture]
    public class InfoHotSpotTests
    {
        [Test]
        [Combinatorial]
        public void CompareInfoHotSpots_AreEqual(
            [Values(-720, -180, 0, 180, 720)] int pitch,
            [Values(-720, -180, 0, 180, 720)] int yaw,
            [Values("test_text")] string text,
            [Values("id")] string id,
            [Values("css")] string cssClass,
            [Values("url")] string url
            )
        {
            InfoHotSpot InfoHotSpot1 = new InfoHotSpot(id)
            {
                Pitch = pitch,
                Yaw = yaw,
                Text = text,
                CssClass = cssClass,
                URL = url
            };

            InfoHotSpot InfoHotSpot2 = new InfoHotSpot(id)
            {
                Pitch = pitch,
                Yaw = yaw,
                Text = text,
                CssClass = cssClass,
                URL = url
            };

            HotSpot hotSpot1 = InfoHotSpot1;
            HotSpot hotSpot2 = InfoHotSpot2;

            IHotSpot iHotSpot1 = InfoHotSpot1;
            IHotSpot iHotSpot2 = InfoHotSpot2;

            Assert.Multiple(() =>
            {
                Assert.That(InfoHotSpot1, Is.EqualTo(InfoHotSpot2));
                Assert.That(InfoHotSpot1 == InfoHotSpot2, Is.True);
                Assert.That(InfoHotSpot1 != InfoHotSpot2, Is.False);
                Assert.That(hotSpot1, Is.EqualTo(hotSpot2));
                Assert.That(hotSpot1 == hotSpot2, Is.True);
                Assert.That(hotSpot1 != hotSpot2, Is.False);
                Assert.That(iHotSpot1, Is.EqualTo(iHotSpot2));
            });
        }

        [TestCase(1, 0, 2, 2, "text", "text", "id", "id", "css", "css", "url1", "url2")]
        [TestCase(1, 1, 2, 0, "text", "text", "id", "id", "css", "css", "url1", "url2")]
        [TestCase(1, 1, 2, 2, "text", "____", "id", "id", "css", "css", "url1", "url2")]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "__", "css", "css", "url1", "url2")]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "id", "css", "___", "url1", "url2")]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "id", "css", "css", "url1", "____")]
        public void CompareInfoHotSpots_AreNotEqual(
            int pitch1,
            int pitch2,
            int yaw1,
            int yaw2,
            string text1,
            string text2,
            string id1,
            string id2,
            string cssClass1,
            string cssClass2,
            string url1,
            string url2
            )
        {
            InfoHotSpot InfoHotSpot1 = new InfoHotSpot(id1)
            {
                Pitch = pitch1,
                Yaw = yaw1,
                Text = text1,
                CssClass = cssClass1,
                URL = url1
            };

            InfoHotSpot InfoHotSpot2 = new InfoHotSpot(id2)
            {
                Pitch = pitch2,
                Yaw = yaw2,
                Text = text2,
                CssClass = cssClass2,
                URL = url2
            };

            HotSpot hotSpot1 = InfoHotSpot1;
            HotSpot hotSpot2 = InfoHotSpot2;

            IHotSpot iHotSpot1 = InfoHotSpot1;
            IHotSpot iHotSpot2 = InfoHotSpot2;

            Assert.Multiple(() =>
            {
                Assert.That(InfoHotSpot1, Is.Not.EqualTo(InfoHotSpot2));
                Assert.That(InfoHotSpot1 == InfoHotSpot2, Is.False);
                Assert.That(InfoHotSpot1 != InfoHotSpot2, Is.True);
                Assert.That(hotSpot1, Is.Not.EqualTo(hotSpot2));
                Assert.That(hotSpot1 == hotSpot2, Is.False);
                Assert.That(hotSpot1 != hotSpot2, Is.True);
                Assert.That(iHotSpot1, Is.Not.EqualTo(iHotSpot2));
            });
        }

        [Test]
        public void CompareSceneHotSpots_ComparedIsNull_AreNotEqual(
            [Values("id")] string id)
        {
            var spot = new InfoHotSpot(id);
            HotSpot hotSpot = spot;
            IHotSpot iHotSpot = spot;

            Assert.Multiple(() => {
                Assert.That(spot.Equals(null), Is.False);
                Assert.That(spot == null, Is.False);
                Assert.That(spot != null, Is.True);
                Assert.That(spot.Equals(null), Is.False);
                Assert.That(hotSpot == null, Is.False);
                Assert.That(hotSpot != null, Is.True);
                Assert.That(iHotSpot.Equals(null), Is.False);
            });
        }

        [Test]
        public void CompareSceneHotSpots_ReferenceEquals_AreEqual(
            [Values("id")] string id)
        {
            var spot = new InfoHotSpot(id);
            var spot2 = spot;

            HotSpot hotSpot1 = spot;
            HotSpot hotSpot2 = spot2;
            IHotSpot iHotSpot1 = spot;
            IHotSpot iHotSpot2 = spot2;

            Assert.Multiple(() => {
                Assert.That(spot.Equals(spot2), Is.True);
                Assert.That(spot == spot2, Is.True);
                Assert.That(spot != spot2, Is.False);
                Assert.That(hotSpot1.Equals(hotSpot2), Is.True);
                Assert.That(hotSpot1 == hotSpot2, Is.True);
                Assert.That(hotSpot1 != hotSpot2, Is.False);
                Assert.That(iHotSpot1.Equals(iHotSpot2), Is.True);
            });
        }
    }
}
