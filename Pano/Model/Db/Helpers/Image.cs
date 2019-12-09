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
using Pano.Extensions;
using Pano.Model.Db.Scenes;

namespace Pano.Model.Db.Helpers
{
    public class Image : ObservableObject
    {
        public const int ThumbnailWidth = 600;

        private byte[] _thumbnail;
        private BitmapImage _bitmapImage;
        private RotateFlipType _rotation = RotateFlipType.RotateNoneFlipNone;

        public int ImageId { get; set; }

        public ImageData ImageData { get; set; }

        /// <summary>
        /// Main picture data stored in DB
        /// </summary>
        public byte[] Data
        {
            get => ImageData?.Data;
            set
            {
                if(ImageData == null)
                    ImageData = new ImageData() {ImageDataId = ImageId};

                ImageData.Data = value;
                ImageData.Image = this;

                RaisePropertyChanged(nameof(Data));
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
                _bitmapImage = _thumbnail?.ToBitmapImage();
                RaisePropertyChanged(nameof(BitmapImage));
            }
        }

        /// <summary>
        /// Rotation of image
        /// </summary>
        public RotateFlipType Rotation
        {
            get => _rotation;
            set
            {
                Set(ref _rotation, value);

                if (Thumbnail == null)
                    return;

                using (System.Drawing.Image drawingImage = Thumbnail.ToDrawingImage())
                {
                    drawingImage.RotateFlip(_rotation);
                    _bitmapImage = drawingImage?.ToBitmapImage();
                }
                RaisePropertyChanged(nameof(BitmapImage));
            }
        }

        /// <summary>
        /// Navigation key
        /// </summary>
        public int? SceneId { get; set; }

        /// <summary>
        /// Navigation variable
        /// </summary>
        public Scene Scene { get; set; }

        /// <summary>
        /// Bitmap to show in WPF controls
        /// </summary>
        public BitmapImage BitmapImage
        {
            get
            {
                if (_bitmapImage == null)
                    _bitmapImage = Thumbnail?.ToBitmapImage();
                return _bitmapImage;
            }
        }

        /// <summary>
        /// Set image from path to image file
        /// </summary>
        /// <param name="pathToImage"></param>
        public void SetImage(string pathToImage)
        {
            using (System.Drawing.Image drawingImage = System.Drawing.Image.FromFile(pathToImage))
            {
                Data = drawingImage.ToByteArray();
                Thumbnail = (CreateThumbnail(drawingImage)).ToByteArray();
                Rotation = RotateFlipType.RotateNoneFlipNone;
            }
        }

        private System.Drawing.Image CreateThumbnail(System.Drawing.Image image)
        {
            var height = (int)(image.Height * (ThumbnailWidth / (double)image.Width));
            return image.GetThumbnailImage(ThumbnailWidth, height, null, IntPtr.Zero);
        }

        /// <summary>
        /// Rotate image clockwise by 90 degrees
        /// </summary>
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

        /// <summary>
        /// Rotate image counter clockwise by 90 degrees
        /// </summary>
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
    }
}
