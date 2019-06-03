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
        public void CreateDefaultTourTest()
        {
            var factory = new TourFactory();
            var tour = factory.CreateDefaultTour();

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


            Assert.IsTrue(true);
        }

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


            salon.AddHotSpot(przedpokoj, -5, 55, -10, -52);
            komorka.AddHotSpot(przedpokoj, -25, 90, -17, 142);
            lazienka.AddHotSpot(przedpokoj2, -8, 170, -15, 175);
            gabinet.AddHotSpot(przedpokoj, -10, -108, -14, -105);
            sypialnia.AddHotSpot(przedpokoj2, -8, 25, -8, 125);
            pokoj.AddHotSpot(przedpokoj2, -15, 175, -10, 35);

            przedpokoj.AddHotSpot(przedpokoj2, -13, 52, -20, -35);

            wc.AddHotSpot(przedpokoj, -20, -175, -16, -150);

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
            ISerializer serializer = new Pano.IO.JsonSerializer();
            IRepository repository = new MemoryRepository(serializer);

            repository.Save(tour);

            var tour2 = repository.Load<Tour>();
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


            salon.AddHotSpot(przedpokoj, -5, 55, -10, -52);
            komorka.AddHotSpot(przedpokoj, -25, 90, -17, 142);
            lazienka.AddHotSpot(przedpokoj2, -8, 170, -15, 175);
            gabinet.AddHotSpot(przedpokoj, -10, -108, -14, -105);
            sypialnia.AddHotSpot(przedpokoj2, -8, 25, -8, 125);
            pokoj.AddHotSpot(przedpokoj2, -15, 175, -10, 35);

            przedpokoj.AddHotSpot(przedpokoj2, -13, 52, -20, -35);

            wc.AddHotSpot(przedpokoj, -20, -175, -16, -150);

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
    }
}