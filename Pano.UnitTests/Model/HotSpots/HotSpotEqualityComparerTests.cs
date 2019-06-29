using Moq;
using NUnit.Framework;
using Pano.Model;
using Pano.Model.HotSpots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.UnitTests.Model.HotSpots
{
    [TestFixture]
    public class HotSpotEqualityComparerTests
    {
        private static HotSpotEqualityComparer comparer = new HotSpotEqualityComparer();

        [Test]
        public void CompareHotSpots_ReferenceEquals()
        {
            var sceneMock1 = new Mock<IHotSpot>();

            Assert.That(comparer.Equals(sceneMock1.Object, sceneMock1.Object), Is.True);
        }

        [Test]
        public void CompareHotSpots_TwoNulls_AreEqual()
        {
            Assert.That(comparer.Equals(null, null), Is.True);
        }

        [Test]
        public void CompareHotSpots_ConcreteIsNotEqualToNull()
        {
            var mock = new Mock<IHotSpot>();

            Assert.Multiple(() =>
            {
                Assert.That(comparer.Equals(mock.Object, null), Is.False);
                Assert.That(comparer.Equals(null, mock.Object), Is.False);
            });
        }

        [Test]
        public void CompareHotSpots_AreEqual()
        {
            var mock = new Mock<IHotSpot>();
            mock.Setup(x => x.Equals(It.IsAny<IHotSpot>())).Returns(true);

            var mock2 = new Mock<IHotSpot>();

            Assert.That(comparer.Equals(mock.Object, mock2.Object), Is.True);
        }

        [Test]
        public void CompareHotSpots_AreNotEqual()
        {
            var mock = new Mock<IHotSpot>();
            mock.Setup(x => x.Equals(It.IsAny<IHotSpot>())).Returns(false);

            var mock2 = new Mock<IHotSpot>();

            Assert.That(comparer.Equals(mock.Object, mock2.Object), Is.False);
        }

        [Test]
        public void GetHashCode_Null_EqualToZero()
        {
            Assert.That(comparer.GetHashCode(null), Is.EqualTo(0));
        }

        [Test]
        public void GetHashCode_InstanceNotNull_NotEqualToZero(
            [Values(Int32.MaxValue, Int32.MinValue)] int hash)
        {
            var mock = new Mock<IHotSpot>();
            mock.Setup(x => x.GetHashCode()).Returns(hash);

            Assert.That(comparer.GetHashCode(mock.Object), Is.Not.EqualTo(0));
        }
    }
}
