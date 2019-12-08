using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Model.Db.Helpers;

namespace Pano.Repository
{
    public interface IImageDataRepository : IRepository<ImageData>
    {
        void CheckChanges();
        IEnumerable<ImageData> GetImageDatasForProject(Project project);
    }
}
