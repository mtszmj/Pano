using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.Db.Helpers
{
    public class StringDictionaryEntry : DictionaryEntry<string, string>
    {
        public int SceneId { get; set; }
        public Scenes.Scene Scene { get; set; }
    }
}
