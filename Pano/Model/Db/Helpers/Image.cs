using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using Pano.Model.Db.Scenes;

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
                _bitmapImage = null;
                _bitmapImage = ByteArrayToBitmapImage(_data);
                RaisePropertyChanged(nameof(BitmapImage));
                RaisePropertyChanged(nameof(DrawingImage));
            }
        }

        public int? SceneId { get; set; }
        public Scene Scene { get; set; }

        private BitmapImage _bitmapImage;
        public BitmapImage BitmapImage
        {
            get
            {
                if (_bitmapImage == null)
                    _bitmapImage = ByteArrayToBitmapImage(Data);
                return _bitmapImage;
            }
        }

        private System.Drawing.Image _drawingImage;
        public System.Drawing.Image DrawingImage
        {
            get
            {
                if (_drawingImage == null)
                    _drawingImage = ByteArrayToImage(Data);
                return _drawingImage;
            }
        }

        public void RotateImageClockwise()
        {
            if (DrawingImage == null)
                return;

            DrawingImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            _data = null;
            Data = ImageToByteArray(_drawingImage);
        }

        public void RotateImageCounterclockwise()
        {
            if (DrawingImage== null)
                return;

            DrawingImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            _data = null;
            Data = ImageToByteArray(_drawingImage);
        }

        public void SetImage(string pathToImage)
        {
            _drawingImage = null;
            _drawingImage = System.Drawing.Image.FromFile(pathToImage);
            _data = null;
            Data = ImageToByteArray(_drawingImage);
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
                img.Freeze();
                return img;
            }
        }
    }
}
