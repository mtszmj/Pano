using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.DB;
using Pano.Model.Db.HotSpots;

namespace Pano.Repository
{
    public class HotSpotRepository : Repository<HotSpot>, IHotSpotRepository
    {
        public HotSpotRepository(PanoContext context) : base(context)
        {
        }

        private PanoContext PanoContext => Context as PanoContext;

        public SceneHotSpot GetSceneHotSpotWithScenes(int id)
        {
            return PanoContext.HotSpots
                .OfType<SceneHotSpot>()
                .Include(x => x.Scene)
                .Include(x => x.TargetScene)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
