using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using Pano.Automapper;
using Pano.Factories.Db;
using Pano.Model;
using Pano.Model.Db.Scenes;
using Pano.Serialization.Model;
using Pano.Serialization.Model.HotSpots;
using Pano.Serialization.Model.Scenes;

namespace Pano.IntegrationTests.Automapper
{
    [TestFixture]
    public class TourDtoTests
    {
        private IMapper Mapper;
        [OneTimeSetUp]
        public void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HotSpotProfile>();
                cfg.AddProfile<SceneProfile>();
                cfg.AddProfile<TourProfile>();
            });

            Mapper = config.CreateMapper();
        }

        [Test]
        public void TourTest()
        {
            var sceneFactory = new SceneFactory();
            var sc1 = sceneFactory.NewEquirectangularScene("title 1");
            var sc2 = sceneFactory.NewEquirectangularScene("title 2");

            sc1.SceneId = 1;
            sc1.Hfov = 10;

            sc2.SceneId = 2;
            sc2.Hfov = 20;

            var spotFactory = new HotSpotFactory();
            var spot1 = spotFactory.NewSceneHotSpot(sc1, sc2);
            var spot2 = spotFactory.NewSceneHotSpot(sc2, sc1);

            spot1.Text = "sc1 to sc2";
            spot2.Text = "sc2 to sc1";
            sc1.HotSpots.Add(spot1);
            sc2.HotSpots.Add(spot2);

            var config = new DefaultSceneConfig() {Title = "default_config"};

            var tour = new TourForDb
            {
                Title = "test_title_of_tour",
                TourForDbId = 1,
                Default = config,
                Scenes = new ObservableCollection<Scene>() { sc1, sc2 }
            };

            Tour dto = new Tour();

            //foreach (var sc in tour.ScenesDictionary.Values)
            //{
                
            //    var res = Mapper.Map<SceneDto>(sc);
            //    var test = Mapper.Map<IHotSpotDto>(sc.HotSpots.FirstOrDefault());
            //}
            Mapper.Map(tour, dto);
            var sceneDto1 = dto.Scenes.FirstOrDefault(x => x.Value.Title == "title 1").Value;
            var spotDto1 = sceneDto1.HotSpots.FirstOrDefault() as SceneHotSpotDto;

            var sceneDto2 = dto.Scenes.FirstOrDefault(x => x.Value.Title == "title 2").Value;
            var spotDto2 = sceneDto2.HotSpots.FirstOrDefault();

            Assert.Multiple(() =>
            {
                Assert.That(dto.Default.Title, Is.EqualTo("default_config"));
                Assert.That(sceneDto1.Hfov, Is.EqualTo(10));
                Assert.That(sceneDto2.Hfov, Is.EqualTo(20));
                Assert.That(spotDto1.Text, Is.EqualTo("sc1 to sc2"));
                Assert.That(spotDto2.Text, Is.EqualTo("sc2 to sc1"));
            });
        }
    }
}
