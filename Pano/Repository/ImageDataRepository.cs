using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.DB;
using Pano.Model;
using Pano.Model.Db.Helpers;

namespace Pano.Repository
{
    public class ImageDataRepository : Repository<ImageData>, IImageDataRepository
    {
        public ImageDataRepository(PanoContext context) : base(context)
        {
        }

        private PanoContext PanoContext => Context as PanoContext;

        public void CheckChanges()
        {
            foreach (var data in PanoContext.ImageDatas.Local)
            {
                if (PanoContext.Entry(data).State == EntityState.Unchanged)
                    continue;

                if(PanoContext.ImageDatas.Any(x => x.ImageDataId == data.ImageDataId))
                    PanoContext.Entry(data).State = EntityState.Modified;
            }
        }

        public IEnumerable<ImageData> GetImageDatasForProject(Project project)
        {
            return PanoContext.ImageDatas
                .Where(x => x.Image.Scene.Tour.Project.ProjectId == project.ProjectId)
                .Include(x => x.Image)
                .ToList();
        }
    }
}
