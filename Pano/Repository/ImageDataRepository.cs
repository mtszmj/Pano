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
    public class ImageDataRepository : Repository<ImageData>, IImageDataRepository
    {
        public ImageDataRepository(PanoContext context) : base(context)
        {
        }

        private PanoContext PanoContext => Context as PanoContext;

        public void SaveChanges()
        {
            foreach (var data in PanoContext.ImageDatas.Local)
            {
                var exists = PanoContext.ImageDatas.Where(x => x.ImageDataId == data.ImageDataId)?.Select(x => x.ImageDataId).ToList();

                if(exists.Any())
                    PanoContext.Entry(data).State = EntityState.Modified;
            }
        }
    }
}
