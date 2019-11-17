using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using Pano.Automapper;
using Pano.Model.Db.Helpers;
using Pano.Model.Db.HotSpots;
using Pano.Model.Db.Scenes;
using Pano.Serialization.Model.HotSpots;
using Pano.Serialization.Model.Scenes;

namespace Pano.IntegrationTests.Automapper
{
    [TestFixture]
    public class HotSpotDtoTests
    {
        private IMapper Mapper;
        [OneTimeSetUp]
        public void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HotSpotProfile>();
            });

            Mapper = config.CreateMapper();
        }

        [Test]
        public void SceneHotSpotWithNullRefsTest()
        {

            var spot = new SceneHotSpot()
            {
                Scene = null,
                TargetScene = null,
                Pitch = 1,
                Yaw = 2,
                TargetSceneId = 3,
                Text = "text",
                SceneId = 4,
                CssClass = "cSSclaSS",
                Id = 5,
                TargetHfov = 6,
                TargetPitch = 7,
                TargetYaw = 8
            };

            var dto = Mapper.Map<SceneHotSpotDto>(spot);

            Assert.Multiple(() =>
            {
                Assert.That(dto.SceneId, Is.EqualTo(spot.TargetSceneId.ToString()));
                Assert.That(dto.Pitch, Is.EqualTo(spot.Pitch));
                Assert.That(dto.Yaw, Is.EqualTo(spot.Yaw));
                Assert.That(dto.Text, Is.EqualTo(spot.Text));
                Assert.That(dto.CssClass, Is.EqualTo(spot.CssClass));
                Assert.That(dto.Id, Is.EqualTo(spot.Id.ToString()));
                Assert.That(dto.TargetHfov, Is.EqualTo(spot.TargetHfov));
                Assert.That(dto.TargetPitch, Is.EqualTo(spot.TargetPitch));
                Assert.That(dto.TargetYaw, Is.EqualTo(spot.TargetYaw));
                Assert.That(dto.Type, Is.EqualTo(spot.Type));
            });
        }
    }
}
