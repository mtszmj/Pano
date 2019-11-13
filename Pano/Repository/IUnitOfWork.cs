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
        ISceneRepository Scenes { get; }
        IImageRepository Images { get; }
        IDefaultSceneConfigRepository DefaultSceneConfigs { get; }

        int Complete();
    }
}
