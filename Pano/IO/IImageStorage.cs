using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model.Db.Helpers;

namespace Pano.IO
{
    public interface IImageStorage
    {
        void Save(string path, IEnumerable<Image> images, ImageFormat format = null);
    }
}
