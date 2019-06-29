﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pano.Model.HotSpots.Tests
{
    [TestClass()]
    public class HotSpotEqualityComparerTests
    {
        private SceneHotSpot SceneHotSpot1_0 => new SceneHotSpot("id", "sceneId")
        {
            CssClass = "css",
            Pitch = 1,
            TargetHfov = 2,
            TargetPitch = 3,
            TargetYaw = 4,
            Text = "text",
            Yaw = 5
        };

        private HotSpot SceneHotSpot1_1 => new SceneHotSpot("id", "sceneId")
        {
            CssClass = "css",
            Pitch = 1,
            TargetHfov = 2,
            TargetPitch = 3,
            TargetYaw = 4,
            Text = "text",
            Yaw = 5
        };

        private SceneHotSpot SceneHotSpot2_0 => new SceneHotSpot("id", "sceneId");

        private HotSpot SceneHotSpot3_0 => new SceneHotSpot("id", "sceneId")
        {
            CssClass = "css",
            Pitch = 1,
            TargetHfov = 2,
            TargetPitch = 3,
            TargetYaw = 40,
            Text = "text",
            Yaw = 5
        };

        private InfoHotSpot InfoHotSpot2_0 => new InfoHotSpot("id");


        [TestMethod()]
        public void EqualsTest()
        {
            var comparer = new HotSpotEqualityComparer();
            Assert.IsTrue(comparer.Equals(SceneHotSpot1_0, SceneHotSpot1_1));
        }

        [TestMethod()]
        public void NotEqualsTest()
        {
            var comparer = new HotSpotEqualityComparer();
            Assert.IsFalse(comparer.Equals(SceneHotSpot2_0, InfoHotSpot2_0));
        }

        [TestMethod()]
        public void NotEqualsTest1()
        {
            var comparer = new HotSpotEqualityComparer();
            Assert.IsFalse(comparer.Equals(SceneHotSpot3_0, SceneHotSpot1_1));
        }
    }
}