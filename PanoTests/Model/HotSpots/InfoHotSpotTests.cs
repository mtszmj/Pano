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
            InfoHotSpot spot1 = new InfoHotSpot("id")
            {
                CssClass = "css",
                Pitch = 1,
                Text = "text",
                Yaw = 5,
                URL = "url"
            };

            InfoHotSpot spot2 = new InfoHotSpot("id")
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
            InfoHotSpot spot1 = new InfoHotSpot("id")
            {
                CssClass = "csscss",
                Pitch = 10,
                Text = "texttext",
                Yaw = 50,
                URL = "url"
            };

            InfoHotSpot spot2 = new InfoHotSpot("id")
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
            InfoHotSpot spot1 = new InfoHotSpot("id");
            InfoHotSpot spot2 = new InfoHotSpot("id");

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void EqualsTest3()
        {
            HotSpot spot1 = new InfoHotSpot("id");
            InfoHotSpot spot2 = new InfoHotSpot("id");

            Assert.IsTrue(spot1 == spot2);
        }

        [TestMethod()]
        public void NotEqualsTest()
        {
            InfoHotSpot spot1 = new InfoHotSpot("id1");
            InfoHotSpot spot2 = new InfoHotSpot("id2");

            Assert.IsTrue(spot1 != spot2);
        }

        [TestMethod()]
        public void NotEqualsTest1()
        {
            InfoHotSpot spot1 = new InfoHotSpot("iddd")
            {
                CssClass = "test1",
                Pitch = 10,
                Text = "texttext",
                Yaw = 50,
                URL = "url"
            };

            InfoHotSpot spot2 = new InfoHotSpot("iddd")
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