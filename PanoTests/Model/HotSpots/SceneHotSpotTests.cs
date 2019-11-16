using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.HotSpots;

namespace Pano.Model.Tests
{
    [TestClass()]
    public class SceneHotSpotTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            SceneHotSpot spot1 = new SceneHotSpot("id", "sceneId")
            {
                CssClass = "css",
                Pitch = 1,
                TargetHfov = 2,
                TargetPitch = 3,
                TargetYaw = 4,
                Text = "text",
                Yaw = 5
            };

            SceneHotSpot spot2 = new SceneHotSpot("id", "sceneId")
            {
                CssClass = "css",
                Pitch = 1,
                TargetHfov = 2,
                TargetPitch = 3,
                TargetYaw = 4,
                Text = "text",
                Yaw = 5
            };

            Assert.AreEqual(spot1, spot2);
        }

        [TestMethod()]
        public void EqualsTest1()
        {
            SceneHotSpot spot1 = new SceneHotSpot("id", "sceneIddd")
            {
                CssClass = "csscss",
                Pitch = 10,
                TargetHfov = 20,
                TargetPitch = 30,
                TargetYaw = 40,
                Text = "texttext",
                Yaw = 50
            };

            SceneHotSpot spot2 = new SceneHotSpot("id", "sceneIddd")
            {
                CssClass = "csscss",
                Pitch = 10,
                TargetHfov = 20,
                TargetPitch = 30,
                TargetYaw = 40,
                Text = "texttext",
                Yaw = 50
            };

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void EqualsTest2()
        {
            SceneHotSpot spot1 = new SceneHotSpot("id", "sceneId");
            SceneHotSpot spot2 = new SceneHotSpot("id", "sceneId");

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void EqualsTest3()
        {
            HotSpot spot1 = new SceneHotSpot("id", "sceneId");
            SceneHotSpot spot2 = new SceneHotSpot("id", "sceneId");

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void NotEqualsTest()
        {
            SceneHotSpot spot1 = new SceneHotSpot("id", "sceneId");
            SceneHotSpot spot2 = new SceneHotSpot("id", "sceneId2");

            Assert.IsTrue(spot1 != spot2);
        }

        [TestMethod()]
        public void NotEqualsTest1()
        {
            SceneHotSpot spot1 = new SceneHotSpot("id", "sceneIddd")
            {
                CssClass = "test1",
                Pitch = 10,
                TargetHfov = 20,
                TargetPitch = 30,
                TargetYaw = 40,
                Text = "texttext",
                Yaw = 50
            };

            SceneHotSpot spot2 = new SceneHotSpot("id", "sceneIddd")
            {
                CssClass = "test2",
                Pitch = 10,
                TargetHfov = 20,
                TargetPitch = 30,
                TargetYaw = 40,
                Text = "texttext",
                Yaw = 50
            };

            Assert.IsFalse(spot1 == spot2);
        }
    }
}