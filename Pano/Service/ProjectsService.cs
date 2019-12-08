using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Repository;
using Scene = Pano.Model.Db.Scenes.Scene;

namespace Pano.Service
{
    public class ProjectsService : IProjectsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public void GetProjects(Action<IList<Project>, Exception> callback)
        {
            
            //    List<Project> projects = Task.Run(() => _repo.GetAllProjects()).Result.ToList();
            try
            {
                List<Project> projects = _unitOfWork.Projects.GetAllProjects().ToList();
                callback?.Invoke(projects, null);
            }
            catch (Exception e)
            {
                callback?.Invoke(Enumerable.Empty<Project>().ToList(), e);
            }

        }

        public Project GetProject(int id)
        {
            return _unitOfWork.Projects.GetProject(id);
        }

        public int Save(Project project)
        {
            //return Task.Run(() => _repo.SaveProject(project)).Result;
            project.DateOfLastModification = DateTime.Now;
            _unitOfWork.Projects.AddOrUpdate(project);
            
            //foreach(var entity in project.Tour.Scenes.SelectMany(s => s.Images).Select(i => i.ImageData).Where(i => i != null))
            //    _unitOfWork.ImageDatas.AddOrUpdate(entity);

            return _unitOfWork.Complete();
        }

        public int SaveAll()
        {
            //return Task.Run(() => _repo.SaveChanges()).Result;
            return _unitOfWork.Complete();
        }

        public void RemoveProject(Project project)
        {
            var p = GetProject(project.ProjectId);
            _unitOfWork.Projects.Remove(p);
        }

        public void RemoveScene(Model.Db.Scenes.Scene scene)
        {
            _unitOfWork.Scenes.Remove(scene);
        }

        public void RemoveHotSpot(Model.Db.HotSpots.HotSpot spot)
        {
            //_repo.RemoveHotSpot(spot);
            _unitOfWork.HotSpots.Remove(spot);
        }
    }
}
