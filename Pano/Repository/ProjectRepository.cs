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
    public class ProjectRepository : IProjectRepository
    {
        private readonly PanoContext _context;

        public ProjectRepository(PanoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects
                                 .Include(p => p.Tour)
                                 .ToListAsync();
        }

        public async Task<Project> GetProject(int id)
        {
            return await _context.Projects
                                 .Include(p => p.Tour.Default) //.Default.FirstSceneRef)
                                 .Include(p => p.Tour.Scenes.Select(x => x.HotSpots))
                                 .Include(p => p.Tour.Scenes.Select(s => s.Images))
                                // .Include(p => p.Tour.Scenes.Select(s => s.HotSpots))
                                 .FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<int> SaveProject(Project project)
        {
            project.DateOfLastModification = DateTime.Now;
            _context.Projects.AddOrUpdate(project);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void RemoveHotSpot(Model.Db.HotSpots.HotSpot spot)
        {
            _context.HotSpots.Remove(spot);
        }
    }
}
