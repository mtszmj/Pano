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
            SceneHotSpotDto spot1 = new SceneHotSpotDto("id", "sceneId")
            {
                CssClass = "css",
                Pitch = 1,
                TargetHfov = 2,
                TargetPitch = 3,
                TargetYaw = 4,
                Text = "text",
                Yaw = 5
            };

            SceneHotSpotDto spot2 = new SceneHotSpotDto("id", "sceneId")
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
            SceneHotSpotDto spot1 = new SceneHotSpotDto("id", "sceneIddd")
            {
                CssClass = "csscss",
                Pitch = 10,
                TargetHfov = 20,
                TargetPitch = 30,
                TargetYaw = 40,
                Text = "texttext",
                Yaw = 50
            };

            SceneHotSpotDto spot2 = new SceneHotSpotDto("id", "sceneIddd")
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
            SceneHotSpotDto spot1 = new SceneHotSpotDto("id", "sceneId");
            SceneHotSpotDto spot2 = new SceneHotSpotDto("id", "sceneId");

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void EqualsTest3()
        {
            HotSpotDto spot1 = new SceneHotSpotDto("id", "sceneId");
            SceneHotSpotDto spot2 = new SceneHotSpotDto("id", "sceneId");

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void NotEqualsTest()
        {
            SceneHotSpotDto spot1 = new SceneHotSpotDto("id", "sceneId");
            SceneHotSpotDto spot2 = new SceneHotSpotDto("id", "sceneId2");

            Assert.IsTrue(spot1 != spot2);
        }

        [TestMethod()]
        public void NotEqualsTest1()
        {
            SceneHotSpotDto spot1 = new SceneHotSpotDto("id", "sceneIddd")
            {
                CssClass = "test1",
                Pitch = 10,
                TargetHfov = 20,
                TargetPitch = 30,
                TargetYaw = 40,
                Text = "texttext",
                Yaw = 50
            };

            SceneHotSpotDto spot2 = new SceneHotSpotDto("id", "sceneIddd")
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