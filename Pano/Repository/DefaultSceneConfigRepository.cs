using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.DB;
using Pano.Model.Db.Scenes;

namespace Pano.Repository
{
    public class DefaultSceneConfigRepository : Repository<DefaultSceneConfig>, IDefaultSceneConfigRepository
    {
        public DefaultSceneConfigRepository(PanoContext context) : base(context)
        {
        }
    }
}
