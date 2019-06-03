using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO
{
    public interface IRepository
    {
        T Load<T>();
        void Save<T>(T obj);
    }
}
