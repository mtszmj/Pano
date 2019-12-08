using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Pano.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this System.Drawing.Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public static System.Drawing.Image ToDrawingImage(this byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (var stream = new MemoryStream(byteArray))
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                return img;
            }
        }

        public static BitmapImage ToBitmapImage(this byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (var stream = new MemoryStream(byteArray))
            {
                return GetBitmapImageFromStream(stream);
            }
        }

        public static BitmapImage ToBitmapImage(this System.Drawing.Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return GetBitmapImageFromStream(stream);
            }
        }

        private static BitmapImage GetBitmapImageFromStream(Stream stream)
        {
            stream.Position = 0;
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }
    }
}
