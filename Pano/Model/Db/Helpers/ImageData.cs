using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Pano.Extensions;

namespace Pano.Model.Db.Helpers
{
    public class ImageData
    {
        public int ImageDataId { get; set; }
        public byte[] Data { get; set; }
        public Image Image { get; set; }
        public System.Drawing.Image DrawingImage
        {
            get
            {
                var img = Data.ToDrawingImage();
                img.RotateFlip(Image?.Rotation ?? RotateFlipType.RotateNoneFlipNone);
                return img;
            }
        }
    }
}
