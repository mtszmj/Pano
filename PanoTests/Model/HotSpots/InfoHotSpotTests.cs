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
    public class InfoHotSpotTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            InfoHotSpotDto spot1 = new InfoHotSpotDto("id")
            {
                CssClass = "css",
                Pitch = 1,
                Text = "text",
                Yaw = 5,
                URL = "url"
            };

            InfoHotSpotDto spot2 = new InfoHotSpotDto("id")
            {
                CssClass = "css",
                Pitch = 1,
                Text = "text",
                Yaw = 5,
                URL = "url"
            };

            Assert.AreEqual(spot1, spot2);
        }

        [TestMethod()]
        public void EqualsTest1()
        {
            InfoHotSpotDto spot1 = new InfoHotSpotDto("id")
            {
                CssClass = "csscss",
                Pitch = 10,
                Text = "texttext",
                Yaw = 50,
                URL = "url"
            };

            InfoHotSpotDto spot2 = new InfoHotSpotDto("id")
            {
                CssClass = "csscss",
                Pitch = 10,
                Text = "texttext",
                Yaw = 50,
                URL = "url"
            };

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void EqualsTest2()
        {
            InfoHotSpotDto spot1 = new InfoHotSpotDto("id");
            InfoHotSpotDto spot2 = new InfoHotSpotDto("id");

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void EqualsTest3()
        {
            HotSpotDto spot1 = new InfoHotSpotDto("id");
            InfoHotSpotDto spot2 = new InfoHotSpotDto("id");

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void NotEqualsTest()
        {
            InfoHotSpotDto spot1 = new InfoHotSpotDto("id1");
            InfoHotSpotDto spot2 = new InfoHotSpotDto("id2");

            Assert.IsTrue(spot1 != spot2);
        }

        [TestMethod()]
        public void NotEqualsTest1()
        {
            InfoHotSpotDto spot1 = new InfoHotSpotDto("iddd")
            {
                CssClass = "test1",
                Pitch = 10,
                Text = "texttext",
                Yaw = 50,
                URL = "url"
            };

            InfoHotSpotDto spot2 = new InfoHotSpotDto("iddd")
            {
                CssClass = "test2",
                Pitch = 10,
                Text = "texttext",
                Yaw = 50,
                URL = "url"
            };

            Assert.IsFalse(spot1 == spot2);
        }
    }
}