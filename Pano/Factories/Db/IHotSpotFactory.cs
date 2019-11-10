using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model.Db.HotSpots;
using Pano.Model.Db.Scenes;

namespace Pano.Factories.Db
{
    public interface IHotSpotFactory
    {
        HotSpot NewSceneHotSpot(Scene scene, Scene targetScene);
    }
}
