using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Repository;

namespace Pano.Service
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectRepository _repo;

        public ProjectsService(IProjectRepository repo)
        {
            _repo = repo;
        }

        public void GetProjects(Action<IList<Project>, Exception> callback)
        {
            try
            {
                List<Project> projects = Task.Run(() => _repo.GetProjects()).Result.ToList();
                callback?.Invoke(projects, null);
            }
            catch (Exception e)
            {
                callback?.Invoke(Enumerable.Empty<Project>().ToList(), e);
            }
        }

        public Project GetProject(int id)
        {
            return Task.Run(() => _repo.GetProject(id).Result).Result;
        }

        public int Save(Project project)
        {
            return Task.Run(() => _repo.SaveProject(project)).Result;
        }

        public int SaveAll()
        {
            return Task.Run(() => _repo.SaveChanges()).Result;
        }

        public void RemoveHotSpot(Model.Db.HotSpots.HotSpot spot)
        {
            _repo.RemoveHotSpot(spot);
        }
    }
}
