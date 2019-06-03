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
    public class FileRepository : IRepository
    {
        private readonly ISerializer _Serializer;

        public FileRepository(ISerializer serializer, string filePath = null)
        {
            _Serializer = serializer;
            FilePath = filePath;
        }

        public string FilePath { get; set; }

        public T Load<T>()
        {
            if(FilePath == null)
            {
                throw new InvalidOperationException($"{nameof(FilePath)} is not initialized.");
            }
            else if (File.Exists(FilePath))
            {
                throw new InvalidOperationException($"Inorrect value of {nameof(FilePath)} - file does not exist.");
            }

            return _Serializer.Deserialize<T>(File.ReadAllText(FilePath));
        }

        public void Save<T>(T obj)
        {
            if (FilePath == null)
            {
                throw new ArgumentNullException(nameof(FilePath));
            }

            var serialized = _Serializer.Serialize(obj);
            File.WriteAllText(FilePath, serialized);
        }
    }
}
