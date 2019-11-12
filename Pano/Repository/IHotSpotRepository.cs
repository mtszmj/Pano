using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model.Db.HotSpots;

namespace Pano.Repository
{
    public interface IHotSpotRepository : IRepository<HotSpot>
    {
        SceneHotSpot GetSceneHotSpotWithScenes(int id);
    }
}
