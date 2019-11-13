using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.DB;
using Pano.Model.Db.Scenes;

namespace Pano.Repository
{
    public class SceneRepository : Repository<Scene>, ISceneRepository
    {
        public SceneRepository(PanoContext context) : base(context)
        {
            
        }
    }
}
