using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO
{
    public class MemoryRepository : IRepository
    {
        private readonly ISerializer _Serializer;
        private readonly MemoryStream _Stream = new MemoryStream();

        public MemoryRepository(ISerializer serializer)
        {
            _Serializer = serializer;
        }

        public T Load<T>()
        {
            _Stream.Position = 0;
            StreamReader reader = new StreamReader(_Stream, Encoding.Default);
            var text = reader.ReadToEnd();
            return _Serializer.Deserialize<T>(text);
        }

        public void Save<T>(T obj)
        {
            var serialized = _Serializer.Serialize(obj);
            _Stream.Flush();
            _Stream.Write(Encoding.Default.GetBytes(serialized), 0, serialized.Length);
        }
    }
}
