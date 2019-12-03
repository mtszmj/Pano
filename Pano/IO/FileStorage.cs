using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Pano.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO
{
    public class FileStorage : IStorage
    {
        private readonly ISerializer _Serializer;

        public FileStorage(ISerializer serializer, string path = null)
        {
            _Serializer = serializer;
            Path = path;
        }

        public string Path { get; set; }

        public T Load<T>()
        {
            if(Path == null)
            {
                throw new InvalidOperationException($"{nameof(Path)} is not initialized.");
            }
            if (File.Exists(Path))
            {
                throw new InvalidOperationException($"Inorrect value of {nameof(Path)} - file does not exist.");
            }

            return _Serializer.Deserialize<T>(File.ReadAllText(Path));
        }

        public void Save<T>(T obj)
        {
            if (Path == null)
            {
                throw new ArgumentNullException(nameof(Path));
            }

            var serialized = _Serializer.Serialize(obj);
            File.WriteAllText(Path, serialized);
        }
    }
}
