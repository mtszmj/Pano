using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;

namespace Pano.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(int id);
        Task<int> SaveProject(Project project);
        Task<int> SaveChanges();

        void RemoveHotSpot(Model.Db.HotSpots.HotSpot spot);
    }
}
