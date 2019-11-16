using Moq;
using NUnit.Framework;
using Pano.Serialization.Model.HotSpots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Serialization.Model.HotSpots;

namespace Pano.UnitTests.Model.HotSpots
{
    [TestFixture]
    public class HotSpotEqualityComparerTests
    {
        private static HotSpotEqualityComparer comparer = new HotSpotEqualityComparer();

        [Test]
        public void CompareHotSpots_ReferenceEquals()
        {
            var sceneMock1 = new Mock<IHotSpotDto>();

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
            var mock = new Mock<IHotSpotDto>();

            Assert.Multiple(() =>
            {
                Assert.That(comparer.Equals(mock.Object, null), Is.False);
                Assert.That(comparer.Equals(null, mock.Object), Is.False);
            });
        }

        [Test]
        public void CompareHotSpots_AreEqual()
        {
            var mock = new Mock<IHotSpotDto>();
            mock.Setup(x => x.Equals(It.IsAny<IHotSpotDto>())).Returns(true);

            var mock2 = new Mock<IHotSpotDto>();

            Assert.That(comparer.Equals(mock.Object, mock2.Object), Is.True);
        }

        [Test]
        public void CompareHotSpots_AreNotEqual()
        {
            var mock = new Mock<IHotSpotDto>();
            mock.Setup(x => x.Equals(It.IsAny<IHotSpotDto>())).Returns(false);

            var mock2 = new Mock<IHotSpotDto>();

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
            var mock = new Mock<IHotSpotDto>();
            mock.Setup(x => x.GetHashCode()).Returns(hash);

            Assert.That(comparer.GetHashCode(mock.Object), Is.Not.EqualTo(0));
        }
    }
}
