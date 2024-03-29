﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Serialization.Model.Scenes
{
    public class CubemapDto : SceneDto
    {
        public override PanoramaType Type => PanoramaType.Cubemap;

        /// <summary>
        /// This is an array of URLs for the six cube faces in the order 
        /// front, right, back, left, up, down. 
        /// These are relative to basePath if it is set, else they are 
        /// relative to the location of pannellum.htm. Absolute URLs can 
        /// also be used.
        /// </summary>
        string[] CubeMap { get; set; } = new string[6];

        public override bool Equals(ISceneDto other)
        {
            if (!base.Equals(other))
                return false;

            if (!(other is CubemapDto scene))
                return false;

            return CubeMap.SequenceEqual(scene.CubeMap);
        }
    }
}
