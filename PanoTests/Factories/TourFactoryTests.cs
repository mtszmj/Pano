using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Pano.IO;
using Pano.Model;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pano.Factories.Tests
{
    [TestClass()]
    public class TourFactoryTests
    {

        [TestMethod()]
        public void CreateDefaultProjectTest()
        {
            var tour = new Tour();

            var salon = new Equirectangular()
            {
                Title = "Salon",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-kuchnia.jpg",
            };

            var komorka = new Equirectangular()
            {
                Title = "Komórka",
                Pitch = 0,
                Yaw = 0,
                Panorama = "komorka-p.jpg",
            };

            var lazienka = new Equirectangular()
            {
                Title = "Łazienka",
                Pitch = 0,
                Yaw = 0,
                Panorama = "lazienka_p-lazienka.jpg",
            };


            var gabinet = new Equirectangular()
            {
                Title = "Gabinet",
                Pitch = 0,
                Yaw = 0,
                Panorama = "panorama_p-gabinet.jpg",
            };

            var sypialnia = new Equirectangular()
            {
                Title = "Sypialnia",
                Pitch = 0,
                Yaw = 0,
                Panorama = "panorama_p-sypialnia.jpg",
            };

            var pokoj = new Equirectangular()
            {
                Title = "Pokój",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-pokoj.jpg",
            };

            var przedpokoj = new Equirectangular()
            {
                Title = "Przedpokój",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-przedpokoj.jpg",
            };

            var przedpokoj2 = new Equirectangular()
            {
                Title = "Przedpokój 2",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-przedpokoj2.jpg",
            };

            var wc = new Equirectangular()
            {
                Title = "WC",
                Pitch = 0,
                Yaw = 0,
                Panorama = "toaleta_p-wc.jpg",
            };


            salon.AddSceneHotSpot(przedpokoj, -5, 55, -10, -52);
            komorka.AddSceneHotSpot(przedpokoj, -25, 90, -17, 142);
            lazienka.AddSceneHotSpot(przedpokoj2, -8, 170, -15, 175);
            gabinet.AddSceneHotSpot(przedpokoj, -10, -108, -14, -105);
            sypialnia.AddSceneHotSpot(przedpokoj2, -8, 25, -8, 125);
            pokoj.AddSceneHotSpot(przedpokoj2, -15, 175, -10, 35);

            przedpokoj.AddSceneHotSpot(przedpokoj2, -13, 52, -20, -35);

            wc.AddSceneHotSpot(przedpokoj, -20, -175, -16, -150);

            tour.AddScene(salon);
            tour.AddScene(komorka);
            tour.AddScene(lazienka);
            tour.AddScene(gabinet);
            tour.AddScene(sypialnia);
            tour.AddScene(pokoj);
            tour.AddScene(przedpokoj);
            tour.AddScene(przedpokoj2);
            tour.AddScene(wc);
            tour.Default.HotSpotDebug = false;
            tour.Default.AutoLoad = true;
            tour.Default.AutoRotate = 1;



            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = contractResolver,
                Converters = new List<JsonConverter> { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },

            };

            string json = JsonConvert.SerializeObject(tour, Formatting.Indented, jsonSerializerSettings);
            Debug.Write(json);
        }

        [TestMethod()]
        public void CreateDefaultProjectTest2()
        {
            var tour = CreateDefaultProject();
            ISerializer serializer = new Pano.IO.JsonSerializer(new JsonConverter[] { new HotSpotJsonConverter(), new SceneJsonConverter() });
            IRepository repository = new MemoryRepository(serializer);

            repository.Save(tour);

            var tour2 = repository.Load<Tour>();

            foreach (var kvp in tour.Scenes)
            {
                tour2.Scenes.TryGetValue(kvp.Key, out var value);
                Assert.IsTrue(kvp.Value.Equals(value));
            }

            Assert.AreNotEqual(0, tour2.Scenes.Count);
        }

        [TestMethod()]
        public void CreateDefaultProjectTest3()
        {
            var tour = new Tour();

            var salon = new Equirectangular()
            {
                Title = "Zażółć gęślą jaźń",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-kuchnia.jpg",
            };

            var komorka = new Equirectangular()
            {
                Title = "Komórka",
                Pitch = 0,
                Yaw = 0,
                Panorama = "komorka-p.jpg",
            };

            salon.AddSceneHotSpot(komorka, -5, 55, -10, -52);
            tour.AddScene(salon);
            tour.AddScene(komorka);


            ISerializer serializer = new Pano.IO.JsonSerializer(new JsonConverter[] { new HotSpotJsonConverter(), new SceneJsonConverter() });
            IRepository repository = new MemoryRepository(serializer);

            repository.Save(tour);

            var tour2 = repository.Load<Tour>();

            foreach (var kvp in tour.Scenes)
            {
                tour2.Scenes.TryGetValue(kvp.Key, out var value);
                Assert.IsTrue(kvp.Value.Equals(value));
            }

            Assert.AreNotEqual(0, tour2.Scenes.Count);
        }

        [TestMethod()]
        public void CreateProject2Test()
        {
            var tour = CreateProject2();

            ISerializer serializer = new Pano.IO.JsonSerializer(new JsonConverter[] { new HotSpotJsonConverter(), new SceneJsonConverter() });
            IRepository repository = new MemoryRepository(serializer);

            repository.Save(tour);

            var tour2 = repository.Load<Tour>();
            
            foreach (var kvp in tour.Scenes)
            {
                tour2.Scenes.TryGetValue(kvp.Key, out var value);
                Assert.IsTrue(kvp.Value.Equals(value));
            }

            Assert.AreNotEqual(0, tour2.Scenes.Count);



            string json = serializer.Serialize(tour);
            Debug.Write(json);
        }

        private Tour CreateDefaultProject()
        {
            var tour = new Tour();

            var salon = new Equirectangular()
            {
                Title = "Salon",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-kuchnia.jpg",
            };

            var komorka = new Equirectangular()
            {
                Title = "Komórka",
                Pitch = 0,
                Yaw = 0,
                Panorama = "komorka-p.jpg",
            };

            var lazienka = new Equirectangular()
            {
                Title = "Łazienka",
                Pitch = 0,
                Yaw = 0,
                Panorama = "lazienka_p-lazienka.jpg",
            };


            var gabinet = new Equirectangular()
            {
                Title = "Gabinet",
                Pitch = 0,
                Yaw = 0,
                Panorama = "panorama_p-gabinet.jpg",
            };

            var sypialnia = new Equirectangular()
            {
                Title = "Sypialnia",
                Pitch = 0,
                Yaw = 0,
                Panorama = "panorama_p-sypialnia.jpg",
            };

            var pokoj = new Equirectangular()
            {
                Title = "Pokój",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-pokoj.jpg",
            };

            var przedpokoj = new Equirectangular()
            {
                Title = "Przedpokój",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-przedpokoj.jpg",
            };

            var przedpokoj2 = new Equirectangular()
            {
                Title = "Przedpokój 2",
                Pitch = 0,
                Yaw = 0,
                Panorama = "spacer_p-przedpokoj2.jpg",
            };

            var wc = new Equirectangular()
            {
                Title = "WC",
                Pitch = 0,
                Yaw = 0,
                Panorama = "toaleta_p-wc.jpg",
            };


            salon.AddSceneHotSpot(przedpokoj, -5, 55, -10, -52);
            komorka.AddSceneHotSpot(przedpokoj, -25, 90, -17, 142);
            lazienka.AddSceneHotSpot(przedpokoj2, -8, 170, -15, 175);
            gabinet.AddSceneHotSpot(przedpokoj, -10, -108, -14, -105);
            sypialnia.AddSceneHotSpot(przedpokoj2, -8, 25, -8, 125);
            pokoj.AddSceneHotSpot(przedpokoj2, -15, 175, -10, 35);

            przedpokoj.AddSceneHotSpot(przedpokoj2, -13, 52, -20, -35);

            wc.AddSceneHotSpot(przedpokoj, -20, -175, -16, -150);

            tour.AddScene(salon);
            tour.AddScene(komorka);
            tour.AddScene(lazienka);
            tour.AddScene(gabinet);
            tour.AddScene(sypialnia);
            tour.AddScene(pokoj);
            tour.AddScene(przedpokoj);
            tour.AddScene(przedpokoj2);
            tour.AddScene(wc);
            tour.Default.HotSpotDebug = false;
            tour.Default.AutoLoad = true;
            tour.Default.AutoRotate = 1;

            return tour;
        }

        private Tour CreateProject2()
        {
            var scenes = new Dictionary<string, Scene>
            {
                ["salon"] = new Equirectangular { Title = "Salon", Panorama = "p2.jpg" },
                ["kuchnia"] = new Equirectangular { Title = "Kuchnia", Panorama = "p1.jpg" },
                ["lazienka"] = new Equirectangular { Title = "Łazienka", Panorama = "p3.jpg" }
            };

            scenes["kuchnia"].AddSceneHotSpot(scenes["salon"], -17, -138, -14, -48);
            scenes["kuchnia"].AddSceneHotSpot(scenes["lazienka"], -17, 148, -15, 93);

            var tour = new Tour();
            foreach (var kv in scenes)
            {
                tour.AddScene(kv.Value);
            }

            return tour;
        }
    }
}