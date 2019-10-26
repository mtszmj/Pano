using Pano.Model;
using Pano.Model.Db.Helpers;
using Pano.Model.Db.Scenes;
using SceneHotSpot = Pano.Model.Db.HotSpots.SceneHotSpot;

namespace Pano.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pano.DB.PanoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Pano.DB.PanoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var project = new Project()
            {
                ProjectId = 1,
                Name = "SeedProject",
                Description = "Project created during seed method",
                DateOfCreation = DateTime.Now,
                DateOfLastModification = DateTime.Now,
            };
            context.Projects.AddOrUpdate(p => p.Name, project);

            var tour = new TourForDb()
            {
                TourForDbId = 1,
                Title = "Seed Title for Tour",
                Project = project
            };
            context.TourForDbs.AddOrUpdate(t => t.Title, tour);

            var scene1 = new Model.Db.Scenes.Equirectangular()
            {
                SceneId = 1,
                Title = "equirectangular title 1 - seed",
                TourId = 1,
                Tour = tour
            };
            context.Scenes.AddOrUpdate(s => s.Title, scene1);

            var scene2 = new Model.Db.Scenes.Equirectangular()
            {
                SceneId = 2,
                Title = "equirectangular title 2 - seed",
                TourId = 1,
                Tour = tour
            };
            context.Scenes.AddOrUpdate(s => s.Title, scene2);

            var defaultScene = new Model.Db.Scenes.DefaultSceneConfig()
            {
                Id = 1,
                Title = "default scene title - seed",
                Tour = tour
            };
            context.DefaultScenes.AddOrUpdate(d => d.Title, defaultScene);

            var sde1 = new StringDictionaryEntry()
            {
                Id = 1,
                Key = "Title",
                Value = "Title set by String KeyValue",
                SceneId = 1
            };
            context.StringDictionaryEntries.AddOrUpdate(s => new {s.Key, s.Value}, sde1);

            var sde2 = new StringDictionaryEntry()
            {
                Id = 2,
                Key = "Title2",
                Value = "Title2 set by String KeyValue",
                SceneId = 2
            };
            context.StringDictionaryEntries.AddOrUpdate(s => new {s.Key, s.Value, s.SceneId}, sde2);

            var spot1 = new Model.Db.HotSpots.SceneHotSpot()
            {
                Id = 1,
                SceneId = 1,
                TargetSceneId = 2,
                Text = "Scene 1 to Scene 2 Seed"
            };
            var spot2 = new Model.Db.HotSpots.SceneHotSpot()
            {
                Id = 2,
                SceneId = 2,
                TargetSceneId = 1,
                Text = "Scene 2 to Scene 1 Seed"
            };
            context.HotSpots.AddOrUpdate(
                s => new { s.SceneId, s.Text },
                spot1, spot2
            );

            context.SaveChanges();

            var pp = new Project()
            {
                DateOfCreation = DateTime.Now,
                DateOfLastModification = DateTime.Now,
                Name = "Test wstawiania w calosci"
            };

            var tt = new TourForDb()
            {
                Title = "Tytul dla wstawiania w calosci",
                Project = pp
            };

            pp.Tour = tt;

            tt.Default = new DefaultSceneConfig() {Title = "Tytul default od calosci", Tour = tt};

            var sc1 = new Model.Db.Scenes.Equirectangular() {Title = "equi1"};
            var sc2 = new Model.Db.Scenes.Equirectangular() {Title = "equi2"};
            var hs1 = new SceneHotSpot { Scene = sc1, TargetScene = sc2, Text = "spot 1" };
            var hs2 = new SceneHotSpot() { Scene = sc2, TargetScene = sc1, Text = "spot 2" };
            sc1.HotSpots.Add(hs1);
            sc2.HotSpots.Add(hs2);
            tt.Scenes.Add(sc1);
            tt.Scenes.Add(sc2);


            context.Projects.Add(pp);

            //context.HotSpots.AddOrUpdate(s => s.SceneId, spot2);


            // na razie ponizej niedodane
            //defaultScene.FirstSceneRef = scene1;
            //scene1.HotSpots.Add(spot);
            //tour.Default = defaultScene;
            //tour.Scenes.Add(scene1);
            //tour.Scenes.Add(scene2);
            //scene1.Strings.Add(new StringDictionaryEntry() { Key = "Title", Value = "CuStOm TiTlE" });

        }
    }
}
