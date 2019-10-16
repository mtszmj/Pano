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
                                 .Include(p => p.Tour) //.Default.FirstSceneRef)
                                // .Include(p => p.Tour.Scenes.Select(s => s.HotSpots))
                                 .FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<int> SaveProject(Project project)
        {
            project.DateOfLastModification = DateTime.Now;

            if (project.ProjectId == 0)
            {
                var tour = project.Tour;
                project.Tour = null;

                if (tour != null)
                    tour.Project = project;

                _context.Projects.Add(project);
                _context.TourForDbs.AddOrUpdate(tour);
            }

            else
            {
                _context.Projects.Attach(project);
                _context.Entry(project).State = EntityState.Modified;
            }

            //_context.Entry(project).State = project.ProjectId == Guid.Empty ? EntityState.Added : EntityState.Modified;

            //var tour = project.Tour;
            //_context.TourForDbs.Attach(tour);
            //_context.Entry(tour).State = tour.TourForDbId == Guid.Empty ? EntityState.Added : EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
