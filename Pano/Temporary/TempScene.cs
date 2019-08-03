using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Temporary
{
    public abstract class TempScene
    {
        private Lazy<string> _Guid { get; } = new Lazy<string>(() => Guid.NewGuid().ToString());

        public string Id
        {
            get
            {
                if (Title == null)
                    return _Guid.Value.ToString();

                return System.Text.RegularExpressions.Regex.Replace(Title, @"[^0-9a-zA-Z]+", "");
            }
        }
        public abstract PanoramaType Type { get; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
