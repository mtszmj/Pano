using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Pano.Model.Db.Helpers;

namespace Pano.IO
{
    public class ImageStorage : IImageStorage
    {
        public void Save(string path, IEnumerable<Image> images, ImageFormat format = null)
        {
            string dir;
            if(File.Exists(path))
                dir = (new FileInfo(path)).Directory.FullName;
            else if (Directory.Exists(path))
                dir = path;
            else throw new ArgumentException(nameof(path));

            format = format ?? ImageFormat.Jpeg;

            foreach (var image in images)
            {
                var ext = FileExtensionFromEncoder(format);
                var imgPath = Path.Combine(dir, image.Scene.Id) + ext;
                using (var img = image.DrawingImage)
                {
                    img.Save(imgPath, format);
                }
                
            }
        }

        private string FileExtensionFromEncoder(ImageFormat format)
        {
            try
            {
                return ImageCodecInfo.GetImageEncoders()
                    .First(x => x.FormatID == format.Guid)
                    .FilenameExtension
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .First()
                    .Trim('*')
                    .ToLower();
            }
            catch (Exception)
            {
                return "." + format.ToString().ToLower();
            }
        }
    }
}
