﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Imaging;

namespace Pano.Model.Db.Scenes
{
    public class Equirectangular : Scene
    {
        public override PanoramaType Type { get; protected set; } = PanoramaType.Equirectangular;

        /// <summary>
        /// Sets the URL to the equirectangular panorama image. This is relative to 
        /// basePath if it is set, else it is relative to the location of pannellum.htm. 
        /// An absolute URL can also be used.
        /// </summary>
        public string Panorama { get; set; }

        /// <summary>
        /// Sets the panorama’s horizontal angle of view, in degrees. Defaults to 360. 
        /// This is used if the equirectangular image does not cover a full 360 degrees 
        /// in the horizontal.
        /// </summary>
        public int? Haov { get; set; }

        /// <summary>
        /// Sets the panorama’s vertical angle of view, in degrees. Defaults to 180. 
        /// This is used if the equirectangular image does not cover a full 180 degrees 
        /// in the vertical.
        /// </summary>
        public int? Vaov { get; set; }

        /// <summary>
        /// Sets the vertical offset of the center of the equirectangular image from 
        /// the horizon, in degrees. Defaults to 0. This is used if vaov is less than 
        /// 180 and the equirectangular image is not cropped symmetrically.
        /// </summary>
        public int? VOffset { get; set; }

        /// <summary>
        /// If set to true, any embedded Photo Sphere XMP data will be ignored; else, 
        /// said data will override any existing settings. Defaults to false.
        /// </summary>
        public bool? IgnoreGPanoXMP { get; set; }

        /// <summary>
        /// Specifies an array containing RGB values [0, 1] that sets the background 
        /// color shown past the edges of a partial panorama. Defaults to [0, 0, 0] (black).
        /// </summary>
        public float[] BackgroundColor { get; set; } = { 0, 0, 0 };

        //public byte[] Image;

        public override Helpers.Image Image
        {
            get
            {
                if (!Images.Any())
                    Images.Add(new Helpers.Image());

                return Images[0];
            }
        }
        public override BitmapSource BitmapImage => Image.BitmapImage;
        
        public override void SetImage(string pathToImage)
        {
            Image.SetImage(pathToImage);
            RaisePropertyChanged(nameof(BitmapImage));
        }
        public override bool Equals(Scene other)
        {
            if (!base.Equals(other))
                return false;

            if (!(other is Equirectangular scene))
                return false;

            return Panorama == scene.Panorama
                && Haov == scene.Haov
                && Vaov == scene.Vaov
                && VOffset == scene.VOffset
                && IgnoreGPanoXMP == scene.IgnoreGPanoXMP
                && BackgroundColor.SequenceEqual(scene.BackgroundColor);
        }
    }
}
