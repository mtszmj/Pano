using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.DB;
using Pano.Model;

namespace Pano.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(PanoContext context) : base(context)
        {
        }

        private PanoContext PanoContext => Context as PanoContext;

        public IEnumerable<Project> GetAllProjects()
        {
            return PanoContext.Projects
                                 .Include(p => p.Tour)
                                 .ToList();
        }

        public Project GetProject(int id)
        {
            return PanoContext.Projects
                                 .Include(p => p.Tour.Default) //.Default.FirstSceneRef)
                                 .Include(p => p.Tour.Scenes.Select(x => x.HotSpots))
                                 .Include(p => p.Tour.Scenes.Select(s => s.Images))
                                 .FirstOrDefault(p => p.ProjectId == id);
        }
    }
}
