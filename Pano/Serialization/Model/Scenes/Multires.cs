using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Serialization.Model.Scenes
{
    public class Multires : Scene
    {
        public override PanoramaType Type => PanoramaType.Multires;

        public MultiResSubkeys MultiRes { get; } = new MultiResSubkeys();
        
        public override bool Equals(IScene other)
        {
            if (!base.Equals(other))
                return false;

            if (!(other is Multires scene))
                return false;

            return MultiRes.Equals(scene.MultiRes);
        }

        public class MultiResSubkeys : IEquatable<MultiResSubkeys>
        {
            /// <summary>
            /// This is the base path of the URLs for the multiresolution tiles. It is relative to 
            /// the regular basePath option if it is defined, else it is relative to the location 
            /// of pannellum.htm. An absolute URL can also be used.
            /// </summary>
            public string BasePath { get; set; }

            /// <summary>
            /// This is a format string for the location of the multiresolution tiles, relative 
            /// to multiRes.basePath, which is relative to basePath. Format parameters are %l 
            /// for the zoom level, %s for the cube face, %x for the x index, and %y for the y 
            /// index. For each tile, .extension is appended.
            /// </summary>
            public string Path { get; set; }

            /// <summary>
            /// This is a format string for the location of the fallback tiles for the CSS 3D 
            /// transform-based renderer if the WebGL renderer is not supported, relative to 
            /// multiRes.basePath, which is relative to basePath. The only format parameter 
            /// is %s, for the cube face. For each face, .extension is appended.
            /// </summary>
            public string FallbackPath { get; set; }

            /// <summary>
            /// Specifies the tiles’ file extension. Do not include the '.'.
            /// </summary>
            public string Extension { get; set; }

            /// <summary>
            /// This specifies the size in pixels of each image tile.
            /// </summary>
            public int TileResolution { get; set; }

            /// <summary>
            /// This specifies the maximum zoom level.
            /// </summary>
            public int MaxLevel { get; set; }

            /// <summary>
            /// This specifies the size in pixels of the full resolution cube faces the 
            /// image tiles were created from.
            /// </summary>
            public int CubeResolution { get; set; }

            public bool Equals(MultiResSubkeys other)
            {
                if (other == null)
                    return false;

                if (ReferenceEquals(this, other))
                    return true;

                return BasePath == other.BasePath
                    && Path == other.Path
                    && FallbackPath == other.FallbackPath
                    && Extension == other.Extension
                    && TileResolution == other.TileResolution
                    && MaxLevel == other.MaxLevel
                    && CubeResolution == other.CubeResolution;
            }
        }
    }
}
