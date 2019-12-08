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
        public void Save(string path, IEnumerable<ImageData> images, ImageFormat format = null)
        {
            string dir;
            if(File.Exists(path))
                dir = (new FileInfo(path)).Directory.FullName;
            else if (Directory.Exists(path))
                dir = path;
            else throw new ArgumentException(nameof(path));

            format = format ?? ImageFormat.Jpeg;

            foreach (var imageData in images)
            {
                var ext = FileExtensionFromEncoder(format);
                var imgPath = Path.Combine(dir, imageData.Image.Scene.Id) + ext;
                using (var img = imageData.DrawingImage)
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
