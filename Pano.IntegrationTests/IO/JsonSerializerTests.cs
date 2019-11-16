using NUnit.Framework;
using Pano.IO;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pano.Serialization.Model.HotSpots;
using Pano.Serialization.Model.Scenes;
using JsonSerializer = Pano.IO.JsonSerializer;

namespace Pano.IntegrationTests.IO
{
    [TestFixture]
    public class JsonSerializerTests
    {
        [Test]
        public void SerializeSceneHotSpot(
            [Values(10)] int pitch,
            [Values(20)] int yaw,
            [Values("test_text")] string text,
            [Values("test_id")] string id,
            [Values("test_css")] string cssClass,
            [Values("test_sceneId")] string sceneId,
            [Values(30)] int targetPitch,
            [Values(40)] int targetYaw,
            [Values(50)] int targetHfov
        )
        {
            var hotSpotForSerialization = new SceneHotSpot(id, sceneId)
            {
                Pitch = pitch,
                Yaw = yaw,
                Text = text,
                CssClass = cssClass,
                TargetPitch = targetPitch,
                TargetYaw = targetYaw,
                TargetHfov = targetHfov,
            };

            var serializer = new JsonSerializer();
            var json = serializer.Serialize(hotSpotForSerialization);
            var hotSpotAfterDeserialization = serializer.Deserialize<SceneHotSpot>(json);

            Assert.That(hotSpotForSerialization, Is.EqualTo(hotSpotAfterDeserialization));
        }

        [Test]
        public void SerializeInfoHotSpot(
            [Values(10)] int pitch,
            [Values(20)] int yaw,
            [Values("test_text")] string text,
            [Values("test_id")] string id,
            [Values("test_css")] string cssClass,
            [Values("test_url")] string url
        )
        {
            InfoHotSpot hotSpotForSerialization = new InfoHotSpot(id)
            {
                Pitch = pitch,
                Yaw = yaw,
                Text = text,
                CssClass = cssClass,
                URL = url
            };


            var serializer = new JsonSerializer();
            var json = serializer.Serialize(hotSpotForSerialization);
            var hotSpotAfterDeserialization = serializer.Deserialize<InfoHotSpot>(json);

            Assert.That(hotSpotForSerialization, Is.EqualTo(hotSpotAfterDeserialization));
        }

        [TestCase(
            "title", "author", "key1", "value1", "key2", "value2", "basePath", true, 0.09F, 10,
            11, "fallback", true, true, true, true, true, true, true, true,
            21, 22, 23, 24, 25, 26, 27, 28, 29, true,
            31, "preview", "previewTitle", "previewAuthor", 35, 36, true, "crossOrigin", true, 40,
            true,
            "panorama", 43, 44, 45, true, 0.47F, 0.48F, 0.49F,
            "id1", "id2"
        )]
        public void SerializeEquirectangularScene(
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
            var sceneForSerialization = new Equirectangular()
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
                BackgroundColor = new[] {BackgroundColor1, BackgroundColor2, BackgroundColor3}
            };

            sceneForSerialization.Strings[StringsKey1] = StringsValue1;
            sceneForSerialization.Strings[StringsKey2] = StringsValue2;


            var serializer = JsonSerializer.Factory.DefaultInstance();
//            var serializer = new JsonSerializer();
            var json = serializer.Serialize(sceneForSerialization);
            var sceneAfterDeserialization = serializer.Deserialize<Equirectangular>(json);
            Assert.That(sceneForSerialization, Is.EqualTo(sceneAfterDeserialization));

            serializer = new JsonSerializer(
                new JsonConverter[]
                {
                    new HotSpotJsonConverter(new JObjectParser()),
                    new SceneJsonConverter(new JObjectParser())
                }
            );
            json = serializer.Serialize(sceneForSerialization);
            sceneAfterDeserialization = serializer.Deserialize<Equirectangular>(json);
            Assert.That(sceneForSerialization, Is.EqualTo(sceneAfterDeserialization));


            Scene generalSceneForSerialization = sceneForSerialization;
//            serializer = new JsonSerializer(new JsonConverter[]
//                {new HotSpotJsonConverter(new JObjectParser()), new SceneJsonConverter(new JObjectParser())});
            serializer = JsonSerializer.Factory.DefaultInstance();
            json = serializer.Serialize(generalSceneForSerialization);
            var generalSceneAfterDeserialization = serializer.Deserialize<Scene>(json);

            Assert.That(generalSceneForSerialization, Is.EqualTo(generalSceneAfterDeserialization));
        }

        [Test]
        public void SerializeDefaultScene(
            [Values("title")] string Title,
            [Values("author")] string Author,
            [Values("path")] string BasePath,
            [Values(true)] bool? AutoLoad,
            [Values(-0.2f)] float? AutoRotate,
            [Values(3)] int? AutoRotateInactivityDelay,
            [Values(4)] int? AutoRotateStopDelay,
            [Values(5)] int? Yaw, //21
            [Values(6)] int? Pitch,
            [Values(7)] int? Hfov,
            [Values(8)] int? MinYaw,
            [Values(9)] int? MaxYaw,
            [Values(10)] int? MinPitch,
            [Values(11)] int? MaxPitch,
            [Values(12)] int? MinHfov,
            [Values(13)] int? MaxHfov,
            [Values("firstScene")] string FirstScene
        )
        {
            var defaultSceneForSerialization = new DefaultScene()
            {
                Title = Title,
                Author = Author,
                BasePath = BasePath,
                AutoLoad = AutoLoad,
                AutoRotate = AutoRotate,
                AutoRotateInactivityDelay = AutoRotateInactivityDelay,
                AutoRotateStopDelay = AutoRotateStopDelay,
                Yaw = Yaw,
                Pitch = Pitch,
                Hfov = Hfov,
                MinYaw = MinYaw,
                MaxYaw = MaxYaw,
                MinPitch = MinPitch,
                MaxPitch = MaxPitch,
                MinHfov = MinHfov,
                MaxHfov = MaxHfov
            };

            var serializer = JsonSerializer.Factory.DefaultInstance();
            var json = serializer.Serialize(defaultSceneForSerialization);
            var defaultSceneAfterSerialization = serializer.Deserialize<DefaultScene>(json);

            Assert.That(defaultSceneForSerialization, Is.EqualTo(defaultSceneAfterSerialization));
        }
    }
}