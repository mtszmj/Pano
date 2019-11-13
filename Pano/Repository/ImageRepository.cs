using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.DB;
using Pano.Model.Db.Helpers;

namespace Pano.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(PanoContext context) : base(context)
        {
        }
    }
}
