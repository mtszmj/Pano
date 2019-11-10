using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Migrations;
using Pano.Model.Db.Scenes;

namespace Pano.Factories.Db
{
    public class SceneFactory : ISceneFactory
    {
        public Scene NewEquirectangularScene(string title = "")
        {
            var scene = new Equirectangular()
            {
                Title = title,
            };

            return scene;
        }

    }
}
