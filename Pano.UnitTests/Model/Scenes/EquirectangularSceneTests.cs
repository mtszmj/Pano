using Moq;
using NUnit.Framework;
using Pano.Model;
using System;
using System.Linq;
using Pano.Serialization.Model.HotSpots;
using Pano.Serialization.Model.Scenes;

namespace Pano.UnitTests.Model.Scenes
{
    [TestFixture]
    public class EquirectangularSceneTests
    {
        [TestFixture]
        public class EquirectangularEqualityTests
        {

            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
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
                spot1Mock.Setup(x => x.Equals(It.Is<IHotSpot>(spot => ReferenceEquals(spot, spot1Mock.Object)))).Returns(true);

                var spot2Mock = new Mock<IHotSpot>();
                spot2Mock.Setup(x => x.Id).Returns(hotSpotId2);
                spot2Mock.Setup(x => x.Equals(It.Is<IHotSpot>(spot => ReferenceEquals(spot, spot2Mock.Object)))).Returns(true);

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

                scene.AddHotSpot(spot1Mock.Object);
                scene.AddHotSpot(spot2Mock.Object);

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

                scene2.AddHotSpot(spot2Mock.Object);
                scene2.AddHotSpot(spot1Mock.Object);

                // Arrange Scenes for polimorphism           
                Scene baseScene1 = scene;
                Scene baseScene2 = scene2;
                IScene iScene1 = scene;
                IScene iScene2 = scene2;

                // Act and Assert
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

            [TestCase(
                "_____", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",

                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "______", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "____", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "______", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "____", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "______", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "________", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", false, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 99.99F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, -1000,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                -1000, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "________", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", false, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, false, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, false, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, false, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, false, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, false, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, false, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, false,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                -1000, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, -1000, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, -1000, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, -1000, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, -1000, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, -1000, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, -1000, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, -1000, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, -1000, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, false,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                -1000, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "_______", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "____________", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "_____________", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", -1000, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, -1000, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, false, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "___________", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", false, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, -1000,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                false,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "________", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", -1000, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, -1000, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, -1000, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, false, 0.47F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, -1000.0F, 0.48F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, -1000.0F, 0.49F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, -1000.0F,
                "id1", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "___", "id2",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            [TestCase(
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.00F, 0,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "___",
                "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
                11, "fallback", true, true, true, true, true, true, true, true,
                21, 22, 23, 24, 25, 26, 27, 28, 29, true,
                31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
                true,
                "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
                "id1", "id2"
                )]
            public void CompareEquirectangural_DifferentValues_AreNotEqual(
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
                string hotSpotId2,
                // ForComparison
                string TitleForComparison, //1
                string AuthorForComparison,
                string StringsKey1ForComparison,
                string StringsValue1ForComparison,
                string StringsKey2ForComparison,
                string StringsValue2ForComparison,
                string BasePathForComparison,
                bool? AutoLoadForComparison,
                float? AutoRotateForComparison,
                int? AutoRotateInactivityDelayForComparison,
                int? AutoRotateStopDelayForComparison, //11
                string FallbackForComparison,
                bool? OrientationOnByDefaultForComparison,
                bool? ShowZoomCtrlForComparison,
                bool? KeyboardZoomForComparison,
                bool? MouseZoomForComparison,
                bool? DraggableForComparison,
                bool? DisableKeyboardCtrlForComparison,
                bool? ShowFullscreenCtrlForComparison,
                bool? ShowControlsForComparison,
                int? YawForComparison, //21
                int? PitchForComparison,
                int? HfovForComparison,
                int? MinYawForComparison,
                int? MaxYawForComparison,
                int? MinPitchForComparison,
                int? MaxPitchForComparison,
                int? MinHfovForComparison,
                int? MaxHfovForComparison,
                bool? CompassForComparison,
                int? NorthOffsetForComparison, //31
                string PreviewForComparison,
                string PreviewTitleForComparison,
                string PreviewAuthorForComparison,
                int? HorizonPitchForComparison,
                int? HorizonRollForComparison,
                bool? EscapeHTMLForComparison,
                string CrossOriginForComparison,
                bool? HotSpotDebugForComparison,
                int? SceneFadeDurationForComparison,
                bool? DynamicForComparison, //41

                string PanoramaForComparison,
                int? HaovForComparison,
                int? VaovForComparison,
                int? VOffsetForComparison,
                bool? IgnoreGPanoXMPForComparison,
                float BackgroundColor1ForComparison,
                float BackgroundColor2ForComparison,
                float BackgroundColor3ForComparison,

                string hotSpotId1ForComparison,
                string hotSpotId2ForComparison
                )
            {
                // Arrange hot spots mocks
                var spot1Mock = new Mock<IHotSpot>();
                spot1Mock.Setup(x => x.Id).Returns(hotSpotId1);
                spot1Mock.Setup(x => x.Equals(It.Is<IHotSpot>(spot => ReferenceEquals(spot, spot1Mock.Object)))).Returns(true);

                var spot2Mock = new Mock<IHotSpot>();
                spot2Mock.Setup(x => x.Id).Returns(hotSpotId2);
                spot2Mock.Setup(x => x.Equals(It.Is<IHotSpot>(spot => ReferenceEquals(spot, spot2Mock.Object)))).Returns(true);

                var spot1MockForComparison = new Mock<IHotSpot>();
                spot1MockForComparison.Setup(x => x.Id).Returns(hotSpotId1ForComparison);
                spot1MockForComparison.Setup(x => x.Equals(It.Is<IHotSpot>(spot => ReferenceEquals(spot, spot1MockForComparison.Object)))).Returns(true);

                var spot2MockForComparison = new Mock<IHotSpot>();
                spot2MockForComparison.Setup(x => x.Id).Returns(hotSpotId2ForComparison);
                spot2MockForComparison.Setup(x => x.Equals(It.Is<IHotSpot>(spot => ReferenceEquals(spot, spot2MockForComparison.Object)))).Returns(true);

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

                scene.AddHotSpot(spot1Mock.Object);
                scene.AddHotSpot(spot2Mock.Object);

                // Arrange second scene
                var scene2 = new Equirectangular()
                {
                    Title = TitleForComparison,
                    Author = AuthorForComparison,

                    BasePath = BasePathForComparison,
                    AutoLoad = AutoLoadForComparison,
                    AutoRotate = AutoRotateForComparison,
                    AutoRotateInactivityDelay = AutoRotateInactivityDelayForComparison,
                    AutoRotateStopDelay = AutoRotateStopDelayForComparison,
                    Fallback = FallbackForComparison,
                    OrientationOnByDefault = OrientationOnByDefaultForComparison,
                    ShowZoomCtrl = ShowZoomCtrlForComparison,
                    KeyboardZoom = KeyboardZoomForComparison,
                    MouseZoom = MouseZoomForComparison,
                    Draggable = DraggableForComparison,
                    DisableKeyboardCtrl = DisableKeyboardCtrlForComparison,
                    ShowFullscreenCtrl = ShowFullscreenCtrlForComparison,
                    ShowControls = ShowControlsForComparison,
                    Yaw = YawForComparison, //21
                    Pitch = PitchForComparison,
                    Hfov = HfovForComparison,
                    MinYaw = MinYawForComparison,
                    MaxYaw = MaxYawForComparison,
                    MinPitch = MinPitchForComparison,
                    MaxPitch = MaxPitchForComparison,
                    MinHfov = MinHfovForComparison,
                    MaxHfov = MaxHfovForComparison,
                    Compass = CompassForComparison,
                    NorthOffset = NorthOffsetForComparison, //31
                    Preview = PreviewForComparison,
                    PreviewTitle = PreviewTitleForComparison,
                    PreviewAuthor = PreviewAuthorForComparison,
                    HorizonPitch = HorizonPitchForComparison,
                    HorizonRoll = HorizonRollForComparison,
                    EscapeHTML = EscapeHTMLForComparison,
                    CrossOrigin = CrossOriginForComparison,
                    HotSpotDebug = HotSpotDebugForComparison,
                    SceneFadeDuration = SceneFadeDurationForComparison,
                    Dynamic = DynamicForComparison, //41

                    Panorama = PanoramaForComparison,
                    Haov = HaovForComparison,
                    Vaov = VaovForComparison,
                    VOffset = VOffsetForComparison,
                    IgnoreGPanoXMP = IgnoreGPanoXMPForComparison,
                    BackgroundColor = new[] { BackgroundColor1ForComparison, BackgroundColor2ForComparison, BackgroundColor3ForComparison }
                };

                scene2.Strings[StringsKey1ForComparison] = StringsValue1ForComparison;
                scene2.Strings[StringsKey2ForComparison] = StringsValue2ForComparison;

                scene2.AddHotSpot(spot2MockForComparison.Object);
                scene2.AddHotSpot(spot1MockForComparison.Object);

                // Arrange Scenes for polimorphism           
                Scene baseScene1 = scene;
                Scene baseScene2 = scene2;
                IScene iScene1 = scene;
                IScene iScene2 = scene2;

                // Act and Assert
                Assert.Multiple(() =>
                {
                    Assert.That(scene, Is.Not.EqualTo(scene2));
                    Assert.That(scene == scene2, Is.False);
                    Assert.That(scene != scene2, Is.True);
                    Assert.That(baseScene1, Is.Not.EqualTo(baseScene2));
                    Assert.That(baseScene1 == baseScene2, Is.False);
                    Assert.That(baseScene1 != baseScene2, Is.True);
                    Assert.That(iScene1, Is.Not.EqualTo(iScene2));
                });
            }
        }

        [Test]
        public void AddHotSpot_AdditionCorrect_ReturnTrue()
        {
            var scene = new Equirectangular();
            var result1 = scene.AddHotSpot(new InfoHotSpot("id"));
            var result2 = scene.AddHotSpot(new SceneHotSpot("id2", "sceneId"));

            Assert.Multiple(() =>
            {
                Assert.That(scene.HotSpots.Count, Is.EqualTo(2));
                Assert.That(result1, Is.True);
                Assert.That(result2, Is.True);
            });
        }

        [Test]
        public void AddHotSpot_NullNotAdded_ReturnsFalse()
        {
            var scene = new Equirectangular();
            var result = scene.AddHotSpot(null);

            Assert.Multiple(() =>
            {
                Assert.That(scene.HotSpots.Count, Is.EqualTo(0));
                Assert.That(result, Is.False);
            });
        }

        [Test]
        public void AddHotSpot_AddTwiceTheSameHotSpot_AddedOnlyOnce()
        {
            var scene = new Equirectangular();
            var spot = new SceneHotSpot("id", "sceneId");
            scene.AddHotSpot(spot);
            var result = scene.AddHotSpot(spot);

            Assert.Multiple(() =>
            {
                Assert.That(scene.HotSpots.Count, Is.EqualTo(1));
                Assert.That(result, Is.False);
            });
        }

        [Test]
        public void AddSceneHotSpot_SceneIsNull_ThrowsException()
        {
            var scene = new Equirectangular();

            Assert.Throws<ArgumentNullException>(() => scene.AddSceneHotSpot(null));
        }

        [TestCase("Scene1 Text", "Scene2 Text", "Scene1Text", "Scene2Text")]
        public void AddSceneHotSpot_CorrectAddition(string scene1Title, string scene2Title, string scene1Id, string scene2Id)
        {
            var scene = new Equirectangular()
            {
                Title = scene1Title
            };

            var scene2 = new Equirectangular()
            {
                Title = scene2Title
            };

            scene.AddSceneHotSpot(scene2, 1, 2, 3, 4);

            Assert.Multiple(() =>
            {
                Assert.That(scene.HotSpots.Count, Is.EqualTo(1));
                Assert.That(scene2.HotSpots.Count, Is.EqualTo(1));

                SceneHotSpot spot = (SceneHotSpot)scene.HotSpots.First();
                Assert.That(spot.Pitch, Is.EqualTo(1));
                Assert.That(spot.Yaw, Is.EqualTo(2));
                Assert.That(spot.SceneId, Is.EqualTo(scene2Id));
                Assert.That(spot.Text, Is.EqualTo(scene2Title));

                spot = (SceneHotSpot)scene2.HotSpots.First();
                Assert.That(spot.Pitch, Is.EqualTo(3));
                Assert.That(spot.Yaw, Is.EqualTo(4));
                Assert.That(spot.SceneId, Is.EqualTo(scene1Id));
                Assert.That(spot.Text, Is.EqualTo(scene1Title));
            });
        }
    }
}