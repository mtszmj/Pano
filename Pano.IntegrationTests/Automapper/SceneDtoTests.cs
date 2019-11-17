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
    public class SceneDtoTests
    {
        private IMapper Mapper;
        [OneTimeSetUp]
        public void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HotSpotProfile>();
                cfg.AddProfile<SceneProfile>();
            });

            Mapper = config.CreateMapper();
        }

        [Test]
        public void EquirectangularWithNullRefsTest()
        {

            var scene = new Equirectangular()
            {
                Title = "title_test",
                Tour = null,
                SceneId = 1,
                Author = "author_test",
                AutoLoad = true,
                AutoRotate = 0.5f,
                AutoRotateInactivityDelay = 2,
                AutoRotateStopDelay = 3,
                BackgroundColor = new float[] { 0.1f, 0.2f, 0.3f },
                BasePath = "base_path_title",
                Compass = true,
                CrossOrigin = "cross_origin_test",
                DisableKeyboardCtrl = true,
                Draggable = true,
                Dynamic = true,
                EscapeHTML = true,
                Fallback = "fallback_test",
                Haov = 4,
                Hfov = 5,
                HorizonPitch = 6,
                HorizonRoll = 7,
                HotSpotDebug = true,
                IgnoreGPanoXMP = true,
                KeyboardZoom = true,
                MaxHfov = 8,
                MaxPitch = 9,
                MaxYaw = 10,
                MinHfov = 11,
                MinPitch = 12,
                MinYaw = 13,
                MouseZoom = true,
                NorthOffset = 14,
                OrientationOnByDefault = true,
                Panorama = "panorama_test",
                Preview = "preview_test",
                Pitch = 15,
                PreviewAuthor = "preview_author_test",
                PreviewTitle = "preview_title",
                SceneFadeDuration = 16,
                ShowControls = true,
                ShowFullscreenCtrl = true,
                ShowZoomCtrl = true,
                TourId = 17,
                VOffset = 18,
                Vaov = 19,
                Yaw = 20,
                Strings =
                {
                    new StringDictionaryEntry() { Key = "string1key", Value = "scene1value" },
                    new StringDictionaryEntry() { Key = "string2key", Value = "scene2value" }
                }
            };

            EquirectangularDto dto = new EquirectangularDto();
            Mapper.Map(scene, dto);

            Assert.Multiple(() =>
            {
                Assert.That(dto.Title, Is.EqualTo(scene.Title));
                Assert.That(dto.Author, Is.EqualTo(scene.Author));
                Assert.That(dto.AutoLoad, Is.EqualTo(scene.AutoLoad));
                Assert.That(dto.AutoRotate, Is.EqualTo(scene.AutoRotate));
                Assert.That(dto.AutoRotateInactivityDelay, Is.EqualTo(scene.AutoRotateInactivityDelay));
                Assert.That(dto.AutoRotateStopDelay, Is.EqualTo(scene.AutoRotateStopDelay));
                Assert.That(dto.BasePath, Is.EqualTo(scene.BasePath));
                Assert.That(dto.Compass, Is.EqualTo(scene.Compass));
                Assert.That(dto.CrossOrigin, Is.EqualTo(scene.CrossOrigin));
                Assert.That(dto.DisableKeyboardCtrl, Is.EqualTo(scene.DisableKeyboardCtrl));
                Assert.That(dto.Draggable, Is.EqualTo(scene.Draggable));
                Assert.That(dto.Dynamic, Is.EqualTo(scene.Dynamic));
                Assert.That(dto.Fallback, Is.EqualTo(scene.Fallback));
                Assert.That(dto.Hfov, Is.EqualTo(scene.Hfov));
                Assert.That(dto.HorizonPitch, Is.EqualTo(scene.HorizonPitch));
                Assert.That(dto.HorizonRoll, Is.EqualTo(scene.HorizonRoll));
                Assert.That(dto.HotSpotDebug, Is.EqualTo(scene.HotSpotDebug));
                Assert.That(dto.Id, Is.EqualTo(scene.Id));
                Assert.That(dto.KeyboardZoom, Is.EqualTo(scene.KeyboardZoom));
                Assert.That(dto.MaxHfov, Is.EqualTo(scene.MaxHfov));
                Assert.That(dto.MaxPitch, Is.EqualTo(scene.MaxPitch));
                Assert.That(dto.MaxYaw, Is.EqualTo(scene.MaxYaw));
                Assert.That(dto.MouseZoom, Is.EqualTo(scene.MouseZoom));
                Assert.That(dto.NorthOffset, Is.EqualTo(scene.NorthOffset));
                Assert.That(dto.OrientationOnByDefault, Is.EqualTo(scene.OrientationOnByDefault));
                Assert.That(dto.Pitch, Is.EqualTo(scene.Pitch));
                Assert.That(dto.Preview, Is.EqualTo(scene.Preview));
                Assert.That(dto.PreviewAuthor, Is.EqualTo(scene.PreviewAuthor));
                Assert.That(dto.PreviewTitle, Is.EqualTo(scene.PreviewTitle));
                Assert.That(dto.SceneFadeDuration, Is.EqualTo(scene.SceneFadeDuration));
                Assert.That(dto.ShowControls, Is.EqualTo(scene.ShowControls));
                Assert.That(dto.ShowFullscreenCtrl, Is.EqualTo(scene.ShowFullscreenCtrl));
                Assert.That(dto.ShowZoomCtrl, Is.EqualTo(scene.ShowZoomCtrl));

            });

        }

    }
}
