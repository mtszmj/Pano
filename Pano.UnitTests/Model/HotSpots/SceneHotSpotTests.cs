using NUnit.Framework;
using Pano.Model;
using Pano.Serialization.Model.HotSpots;

namespace Pano.UnitTests.Model.HotSpots
{
    [TestFixture]
    public class SceneHotSpotTests
    {
        [Test]
        [Combinatorial]
        public void CompareSceneHotSpots_TheSameValues_AreEqual(
            [Values(-720, -180, 0, 180, 720)] int pitch,
            [Values(-720, -180, 0, 180, 720)] int yaw,
            [Values("test_text")] string text,
            [Values("id")] string id,
            [Values("css")] string cssClass,
            [Values("sceneId")] string sceneId,
            [Values(-720, -180, 0, 180, 720)] int targetPitch,
            [Values(-720, -180, 0, 180, 720)] int targetYaw,
            [Values(-10, 0, 10, 100)] int targetHfov
            )
        {
            SceneHotSpot sceneHotSpot1 = new SceneHotSpot(id, sceneId)
            {
                Pitch = pitch,
                Yaw = yaw,
                Text = text,
                CssClass = cssClass,
                TargetPitch = targetPitch,
                TargetYaw = targetYaw,
                TargetHfov = targetHfov,
            };

            SceneHotSpot sceneHotSpot2 = new SceneHotSpot(id, sceneId)
            {
                Pitch = pitch,
                Yaw = yaw,
                Text = text,
                CssClass = cssClass,
                TargetPitch = targetPitch,
                TargetYaw = targetYaw,
                TargetHfov = targetHfov,
            };

            HotSpot hotSpot1 = sceneHotSpot1;
            HotSpot hotSpot2 = sceneHotSpot2;

            IHotSpot iHotSpot1 = sceneHotSpot1;
            IHotSpot iHotSpot2 = sceneHotSpot2;

            Assert.Multiple(() =>
            {
                Assert.That(sceneHotSpot1, Is.EqualTo(sceneHotSpot2));
                Assert.That(sceneHotSpot1 == sceneHotSpot2, Is.True);
                Assert.That(sceneHotSpot1 != sceneHotSpot2, Is.False);
                Assert.That(hotSpot1, Is.EqualTo(hotSpot2));
                Assert.That(hotSpot1 == hotSpot2, Is.True);
                Assert.That(hotSpot1 != hotSpot2, Is.False);
                Assert.That(iHotSpot1, Is.EqualTo(iHotSpot2));
            });
        }

        [TestCase(1, 0, 2, 2, "text", "text", "id", "id", "css", "css", "sceneId", "sceneId", 3, 3, 4, 4, 5, 5)]
        [TestCase(1, 1, 2, 0, "text", "text", "id", "id", "css", "css", "sceneId", "sceneId", 3, 3, 4, 4, 5, 5)]
        [TestCase(1, 1, 2, 2, "text", "____", "id", "id", "css", "css", "sceneId", "sceneId", 3, 3, 4, 4, 5, 5)]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "__", "css", "css", "sceneId", "sceneId", 3, 3, 4, 4, 5, 5)]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "id", "css", "___", "sceneId", "sceneId", 3, 3, 4, 4, 5, 5)]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "id", "css", "css", "sceneId", "_______", 3, 3, 4, 4, 5, 5)]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "id", "css", "css", "sceneId", "sceneId", 3, 0, 4, 4, 5, 5)]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "id", "css", "css", "sceneId", "sceneId", 3, 3, 4, 0, 5, 5)]
        [TestCase(1, 1, 2, 2, "text", "text", "id", "id", "css", "css", "sceneId", "sceneId", 3, 3, 4, 4, 5, 0)]
        public void CompareSceneHotSpots_DifferentValues_AreNotEqual(
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
            string sceneId1,
            string sceneId2,
            int targetPitch1,
            int targetPitch2,
            int targetYaw1,
            int targetYaw2,
            int targetHfov1,
            int targetHfov2
            )
        {
            SceneHotSpot sceneHotSpot1 = new SceneHotSpot(id1, sceneId1)
            {
                Pitch = pitch1,
                Yaw = yaw1,
                Text = text1,
                CssClass = cssClass1,
                TargetPitch = targetPitch1,
                TargetYaw = targetYaw1,
                TargetHfov = targetHfov1,
            };

            SceneHotSpot sceneHotSpot2 = new SceneHotSpot(id2, sceneId2)
            {
                Pitch = pitch2,
                Yaw = yaw2,
                Text = text2,
                CssClass = cssClass2,
                TargetPitch = targetPitch2,
                TargetYaw = targetYaw2,
                TargetHfov = targetHfov2,
            };

            HotSpot hotSpot1 = sceneHotSpot1;
            HotSpot hotSpot2 = sceneHotSpot2;

            IHotSpot iHotSpot1 = sceneHotSpot1;
            IHotSpot iHotSpot2 = sceneHotSpot2;

            Assert.Multiple(() =>
            {
                Assert.That(sceneHotSpot1, Is.Not.EqualTo(sceneHotSpot2));
                Assert.That(sceneHotSpot1 == sceneHotSpot2, Is.False);
                Assert.That(sceneHotSpot1 != sceneHotSpot2, Is.True);
                Assert.That(hotSpot1, Is.Not.EqualTo(hotSpot2));
                Assert.That(hotSpot1 == hotSpot2, Is.False);
                Assert.That(hotSpot1 != hotSpot2, Is.True);
                Assert.That(iHotSpot1, Is.Not.EqualTo(iHotSpot2));
            });
        }

        [Test]
        public void CompareSceneHotSpots_ComparedIsNull_AreNotEqual(
            [Values("id")] string id,
            [Values("sceneId")] string sceneId)
        {
            var spot = new SceneHotSpot(id, sceneId);

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
            [Values("id")] string id,
            [Values("sceneId")] string sceneId)
        {
            var spot = new SceneHotSpot(id, sceneId);
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

