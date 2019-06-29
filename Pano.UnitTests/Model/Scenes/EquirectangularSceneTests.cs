using Moq;
using NUnit.Framework;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.UnitTests.Model.Scenes
{
    [TestFixture]
    public class EquirectangularSceneTests
    {
        [TestCase(
            "title","author","key1","value1","key2","value2","basePath",true,0.09F,10,
            11,"fallback",true,true,true, true, true, true, true, true, 
            21, 22, 23, 24, 25, 26, 27, 28, 29, true,
            31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40, 
            true,
            "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
            "id1","id2"
            )]
        public void CompareScenes_TheSameValues_AreEqual(
            string Title, //1
            string Author,
            string StringsKey1,
            string StringsValue1,
            string StringsKey2,
            string StringsValue2,
            string BasePath,
            bool? AutoLoad,
            float? AutoRotate,
            int? AutoRotateInactivityDelay,
            int? AutoRotateStopDelay, //11
            string Fallback,
            bool? OrientationOnByDefault,
            bool? ShowZoomCtrl,
            bool? KeyboardZoom,
            bool? MouseZoom,
            bool? Draggable,
            bool? DisableKeyboardCtrl,
            bool? ShowFullscreenCtrl,
            bool? ShowControls,
            int? Yaw, //21
            int? Pitch,
            int? Hfov,
            int? MinYaw,
            int? MaxYaw,
            int? MinPitch,
            int? MaxPitch,
            int? MinHfov,
            int? MaxHfov,
            bool? Compass,
            int? NorthOffset, //31
            string Preview,
            string PreviewTitle,
            string PreviewAuthor,
            int? HorizonPitch,
            int? HorizonRoll,
            bool? EscapeHTML,
            string CrossOrigin,
            bool? HotSpotDebug,
            int? SceneFadeDuration,
            bool? Dynamic, //41

            string Panorama,
            int? Haov,
            int? Vaov,
            int? VOffset,
            bool? IgnoreGPanoXMP,
            float BackgroundColor1,
            float BackgroundColor2,
            float BackgroundColor3,

            string hotSpotId1,
            string hotSpotId2
            )
        {
            // Arrange hot spots mocks
            var spot1Mock = new Mock<IHotSpot>();
            spot1Mock.Setup(x => x.Id).Returns(hotSpotId1);
            //spot1Mock.Setup(x => x.Equals(It.IsAny<IHotSpot>())).Returns((IHotSpot x, IHotSpot y) => ReferenceEquals(x,y));
            spot1Mock.Setup(x => x.Equals(It.IsAny<IHotSpot>())).Returns(true);

            var spot2Mock = new Mock<IHotSpot>();
            spot2Mock.Setup(x => x.Id).Returns(hotSpotId2);
            //spot2Mock.Setup(x => x.Equals(It.Is<IHotSpot>(y => ReferenceEquals(x, y)))).Returns(true);
            spot2Mock.Setup(x => x.Equals(It.IsAny<IHotSpot>())).Returns(true);

            // Arrange first scene
            var scene = new Equirectangular()
            {
                Title = Title,
                Author = Author,

                BasePath = BasePath,
                AutoLoad = AutoLoad,
                AutoRotate = AutoRotate,
                AutoRotateInactivityDelay = AutoRotateInactivityDelay,
                AutoRotateStopDelay = AutoRotateStopDelay,
                Fallback = Fallback,
                OrientationOnByDefault = OrientationOnByDefault,
                ShowZoomCtrl = ShowZoomCtrl,
                KeyboardZoom = KeyboardZoom,
                MouseZoom = MouseZoom,
                Draggable = Draggable,
                DisableKeyboardCtrl = DisableKeyboardCtrl,
                ShowFullscreenCtrl = ShowFullscreenCtrl,
                ShowControls = ShowControls,
                Yaw = Yaw, //21
                Pitch = Pitch,
                Hfov = Hfov,
                MinYaw = MinYaw,
                MaxYaw = MaxYaw,
                MinPitch = MinPitch,
                MaxPitch = MaxPitch,
                MinHfov = MinHfov,
                MaxHfov = MaxHfov,
                Compass = Compass,
                NorthOffset = NorthOffset, //31
                Preview = Preview,
                PreviewTitle = PreviewTitle,
                PreviewAuthor = PreviewAuthor,
                HorizonPitch = HorizonPitch,
                HorizonRoll = HorizonRoll,
                EscapeHTML = EscapeHTML,
                CrossOrigin = CrossOrigin,
                HotSpotDebug = HotSpotDebug,
                SceneFadeDuration = SceneFadeDuration,
                Dynamic = Dynamic, //41

                Panorama = Panorama,
                Haov = Haov,
                Vaov = Vaov,
                VOffset = VOffset,
                IgnoreGPanoXMP = IgnoreGPanoXMP,
                BackgroundColor = new[] { BackgroundColor1, BackgroundColor2, BackgroundColor3 }
            };

            scene.Strings[StringsKey1] = StringsValue1;
            scene.Strings[StringsKey2] = StringsValue2;

            scene.HotSpots.Add(spot1Mock.Object);
            scene.HotSpots.Add(spot2Mock.Object);

            // Arrange second scene
            var scene2 = new Equirectangular()
            {
                Title = Title,
                Author = Author,

                BasePath = BasePath,
                AutoLoad = AutoLoad,
                AutoRotate = AutoRotate,
                AutoRotateInactivityDelay = AutoRotateInactivityDelay,
                AutoRotateStopDelay = AutoRotateStopDelay,
                Fallback = Fallback,
                OrientationOnByDefault = OrientationOnByDefault,
                ShowZoomCtrl = ShowZoomCtrl,
                KeyboardZoom = KeyboardZoom,
                MouseZoom = MouseZoom,
                Draggable = Draggable,
                DisableKeyboardCtrl = DisableKeyboardCtrl,
                ShowFullscreenCtrl = ShowFullscreenCtrl,
                ShowControls = ShowControls,
                Yaw = Yaw, //21
                Pitch = Pitch,
                Hfov = Hfov,
                MinYaw = MinYaw,
                MaxYaw = MaxYaw,
                MinPitch = MinPitch,
                MaxPitch = MaxPitch,
                MinHfov = MinHfov,
                MaxHfov = MaxHfov,
                Compass = Compass,
                NorthOffset = NorthOffset, //31
                Preview = Preview,
                PreviewTitle = PreviewTitle,
                PreviewAuthor = PreviewAuthor,
                HorizonPitch = HorizonPitch,
                HorizonRoll = HorizonRoll,
                EscapeHTML = EscapeHTML,
                CrossOrigin = CrossOrigin,
                HotSpotDebug = HotSpotDebug,
                SceneFadeDuration = SceneFadeDuration,
                Dynamic = Dynamic, //41

                Panorama = Panorama,
                Haov = Haov,
                Vaov = Vaov,
                VOffset = VOffset,
                IgnoreGPanoXMP = IgnoreGPanoXMP,
                BackgroundColor = new[] { BackgroundColor1, BackgroundColor2, BackgroundColor3 }
            };

            scene2.Strings[StringsKey1] = StringsValue1;
            scene2.Strings[StringsKey2] = StringsValue2;

            scene2.HotSpots.Add(spot1Mock.Object);
            scene2.HotSpots.Add(spot2Mock.Object);

            Scene baseScene1 = scene;
            Scene baseScene2 = scene2;
            IScene iScene1 = scene;
            IScene iScene2 = scene2;

            Assert.Multiple(() =>
            {
                Assert.That(scene, Is.EqualTo(scene2));
                Assert.That(scene == scene2, Is.True);
                Assert.That(scene != scene2, Is.False);
                Assert.That(baseScene1, Is.EqualTo(baseScene2));
                Assert.That(baseScene1 == baseScene2, Is.True);
                Assert.That(baseScene1 != baseScene2, Is.False);
                Assert.That(iScene1, Is.EqualTo(iScene2));
            });
        }
    }
}
