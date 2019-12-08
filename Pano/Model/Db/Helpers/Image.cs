using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using Pano.Model.Db.Scenes;

namespace Pano.Model.Db.Helpers
{
    public class Image : ObservableObject
    {
        private byte[] _thumbnail;
        private BitmapImage _bitmapImage;
        private RotateFlipType _rotation = RotateFlipType.RotateNoneFlipNone;
        private ImageData _imageData;


        public int ImageId { get; set; }

        public int? ImageDataId { get; set; }

        public ImageData ImageData
        {
            get => _imageData;
            set => _imageData = value;
        }

        /// <summary>
        /// Main picture data stored in DB
        /// </summary>
        public byte[] Data
        {
            get => _imageData?.Data;
            set
            {
                //Set(ref _data, value);
                //_bitmapImage = null;
                //_bitmapImage = ByteArrayToBitmapImage(_data);
                if(_imageData == null)
                    _imageData = new ImageData() {ImageDataId = ImageId};

                _imageData.Data = value;
                _imageData.Image = this;

                RaisePropertyChanged(nameof(Data));
                //RaisePropertyChanged(nameof(BitmapImage));
                //RaisePropertyChanged(nameof(DrawingImage));
            }
        }

        /// <summary>
        /// Main picture thumbnail data stored in DB
        /// </summary>
        public byte[] Thumbnail
        {
            get => _thumbnail;
            set
            {
                Set(ref _thumbnail, value);
                _bitmapImage = ByteArrayToBitmapImage(_thumbnail);
                RaisePropertyChanged(nameof(BitmapImage));
            }
        }

        public RotateFlipType Rotation
        {
            get => _rotation;
            set
            {
                Set(ref _rotation, value);

                if (Thumbnail == null)
                    return;

                using (System.Drawing.Image drawingImage = ByteArrayToDrawingImage(Thumbnail))
                {
                    drawingImage.RotateFlip(_rotation);
                    _bitmapImage = DrawingImageToBitmapImage(drawingImage);
                }
                RaisePropertyChanged(nameof(BitmapImage));
            }
        }


        public int? SceneId { get; set; }
        public Scene Scene { get; set; }


        /// <summary>
        /// Bitmap to show in WPF controls
        /// </summary>
        public BitmapImage BitmapImage
        {
            get
            {
                if (_bitmapImage == null)
                    _bitmapImage = ByteArrayToBitmapImage(Thumbnail);
                return _bitmapImage;
            }
        }

        //private System.Drawing.Image _drawingImage;
        /// <summary>
        /// Drawing image used to maintain picture mainpulation
        /// </summary>
        public System.Drawing.Image DrawingImage => null;
        //{
        //    get
        //    {
        //        if (_drawingImage == null)
        //            _drawingImage = ByteArrayToDrawingImage(Thumbnail);
        //        return _drawingImage;
        //    }
        //}

        public void RotateImageClockwise()
        {
            switch (Rotation)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    Rotation = RotateFlipType.Rotate90FlipNone;
                    break;
                case RotateFlipType.Rotate90FlipNone:
                    Rotation = RotateFlipType.Rotate180FlipNone;
                    break;
                case RotateFlipType.Rotate180FlipNone:
                    Rotation = RotateFlipType.Rotate270FlipNone;
                    break;
                case RotateFlipType.Rotate270FlipNone:
                    Rotation = RotateFlipType.RotateNoneFlipNone;
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(Rotation), (int)Rotation, typeof(RotateFlipType));
            }
        }

        public void RotateImageCounterclockwise()
        {
            switch (Rotation)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    Rotation = RotateFlipType.Rotate270FlipNone;
                    break;
                case RotateFlipType.Rotate90FlipNone:
                    Rotation = RotateFlipType.RotateNoneFlipNone;
                    break;
                case RotateFlipType.Rotate180FlipNone:
                    Rotation = RotateFlipType.Rotate90FlipNone;
                    break;
                case RotateFlipType.Rotate270FlipNone:
                    Rotation = RotateFlipType.Rotate180FlipNone;
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(Rotation), (int)Rotation, typeof(RotateFlipType));
            }
        }

        public void SetImage(string pathToImage)
        {
            using (System.Drawing.Image drawingImage = System.Drawing.Image.FromFile(pathToImage))
            {
                Data = DrawingImageToByteArray(drawingImage);
                Thumbnail = DrawingImageToByteArray(GetThumbnail(drawingImage));
                Rotation = RotateFlipType.RotateNoneFlipNone;
            }
        }

        private System.Drawing.Image GetThumbnail(System.Drawing.Image image)
        {
            var defaultWidth = 600;
            var height = (int)(image.Height * (defaultWidth / (double)image.Width));
            return image.GetThumbnailImage(defaultWidth, height, null, IntPtr.Zero);
        }

        protected byte[] DrawingImageToByteArray(System.Drawing.Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        protected System.Drawing.Image ByteArrayToDrawingImage(byte[] byteArray)
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
                return GetBitmapImageFromStream(stream);
            }
        }

        protected BitmapImage DrawingImageToBitmapImage(System.Drawing.Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return GetBitmapImageFromStream(stream);
            }
        }

        protected BitmapImage GetBitmapImageFromStream(Stream stream)
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
