using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.Db.Helpers
{
    public class ImageData
    {
        public int ImageDataId { get; set; }
        public byte[] Data { get; set; }
        
        public Image Image { get; set; }
    }
}
