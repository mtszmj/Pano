using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace Pano.Model.Db.Helpers
{
    public class Image : ObservableObject
    {
        public int ImageId { get; set; }

        private byte[] _data;
        public byte[] Data
        {
            get => _data;
            set
            {
                Set(ref _data, value);
                RaisePropertyChanged(nameof(BitmapImage));
                RaisePropertyChanged(nameof(DrawingImage));
            }
        }

        public int? SceneId { get; set; }
        public Model.Db.Scenes.Scene Scene { get; set; }

        public BitmapImage BitmapImage => ByteArrayToBitmapImage(Data);
        public System.Drawing.Image DrawingImage => ByteArrayToImage(Data);

        public void RotateImageClockwise()
        {
            var img = DrawingImage;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Data = ImageToByteArray(img);
        }

        public void RotateImageCounterclockwise()
        {
            var img = DrawingImage;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Data = ImageToByteArray(img);
        }

        public void SetImage(string pathToImage)
        {
            var img = System.Drawing.Image.FromFile(pathToImage);
            Data = ImageToByteArray(img);
        }

        protected byte[] ImageToByteArray(System.Drawing.Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        protected System.Drawing.Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (var stream = new MemoryStream(byteArray))
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                return img;
            }
        }

        protected BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (var stream = new MemoryStream(byteArray))
            {
                stream.Position = 0;
                var img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = stream;
                img.EndInit();
                return img;
            }
        }
    }
}
