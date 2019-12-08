using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Model.Db.Helpers;

namespace Pano.Service
{
    public interface IProjectsService
    {
        void GetProjects(Action<IList<Project>, Exception> callback);
        Project GetProject(int id);
        int Save(Project project);
        int SaveAll();
        void RemoveProject(Project project);
        void RemoveScene(Model.Db.Scenes.Scene scene);
        void RemoveHotSpot(Model.Db.HotSpots.HotSpot spot);
        IEnumerable<ImageData> GetImageDatas(Project project);
    }
}
