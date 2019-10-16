using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model.Db.Helpers
{
    public class DictionaryEntry<TKey, TValue>
    {
        public virtual int Id { get; set; }
        public virtual TKey Key { get; set; } = default(TKey);
        public virtual TValue Value { get; set; } = default(TValue);
    }
}
