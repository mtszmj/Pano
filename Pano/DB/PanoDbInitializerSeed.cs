using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Model.Db.Helpers;

namespace Pano.DB
{
    public class PanoDbInitializerSeed
    {
        public Project SeedProject()
        {
            var tour = new TourForDb();
            var defaultScene = new Model.Db.Scenes.DefaultScene() { Title = "default scene title" };
            var scene1 = new Model.Db.Scenes.Equirectangular() { Title = "equirectangular title 1" };
            var scene2 = new Model.Db.Scenes.Equirectangular() { Title = "equirectangular title 2" };
            var spot = new Model.Db.HotSpots.SceneHotSpot() { Scene = scene1, TargetScene = scene2 };
            defaultScene.FirstSceneRef = scene1;
            //scene1.HotSpots.Add(spot);
            //tour.Default = defaultScene;
            //tour.Scenes.Add(scene1);
            //tour.Scenes.Add(scene2);
            //scene1.Strings.Add(new StringDictionaryEntry() { Key = "Title", Value = "CuStOm TiTlE" });
            var project = new Project() { Tour = tour };

            return project;
        }
    }
}
