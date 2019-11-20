using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO
{
    public interface IStorage
    {
        string Path { get; set; }

        T Load<T>();
        void Save<T>(T obj);
    }
}
