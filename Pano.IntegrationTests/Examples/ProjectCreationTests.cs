using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Pano.IO;
using Pano.Model;

namespace Pano.IntegrationTests.Examples
{
    [TestFixture]
    public class ProjectCreationTests
    {
        [Test]
        public void TestProject01()
        {
            var tour = ExampleProjects.CreateProject01();

            var serializer = new Pano.IO.JsonSerializer(new JsonConverter[] { new HotSpotJsonConverter(new JObjectParser()), new SceneJsonConverter(new JObjectParser()) });
            var json = serializer.Serialize(tour);
            var tourAfterDeserialization = serializer.Deserialize<TourForSerialization>(json);

            Assert.That(tour, Is.EqualTo(tourAfterDeserialization));
        }

        private static class ExampleProjects
        {
            public static TourForSerialization CreateProject01()
            {
                var tour = new TourForSerialization();

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

        }

    }
}
