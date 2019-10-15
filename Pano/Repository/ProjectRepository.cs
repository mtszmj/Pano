using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProject(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<int> SaveProject(Project project)
        {
            project.DateOfLastModification = DateTime.Now;

            if (project.ProjectId == 0)
            {
                _context.Projects.Add(project);
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
