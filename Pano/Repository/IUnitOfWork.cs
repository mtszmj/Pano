using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IHotSpotRepository HotSpots { get; }
        IProjectRepository Projects { get; }
        int Complete();
    }
}
