using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;

namespace Pano.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProject(int id);
    }
}
