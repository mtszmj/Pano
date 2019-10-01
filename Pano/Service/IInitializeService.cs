using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Service
{
    public interface IInitializeService<T>
    {
        void InitializeService(T value);
    }
}
