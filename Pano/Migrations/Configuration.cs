using Pano.Model;
using Pano.Model.Db.Helpers;

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


            // na razie ponizej niedodane
            var defaultScene = new Model.Db.Scenes.DefaultScene() { Title = "default scene title" };
            //var spot = new Model.Db.HotSpots.SceneHotSpot() { Scene = scene1, TargetScene = scene2 };
            defaultScene.FirstSceneRef = scene1;
            //scene1.HotSpots.Add(spot);
            //tour.Default = defaultScene;
            //tour.Scenes.Add(scene1);
            //tour.Scenes.Add(scene2);
            //scene1.Strings.Add(new StringDictionaryEntry() { Key = "Title", Value = "CuStOm TiTlE" });

        }
    }
}
