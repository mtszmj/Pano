using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;

namespace Pano.Service
{
    public interface IProjectsService
    {
        void GetProjects(Action<IList<Project>, Exception> callback);
        void Save(Project project);
    }
}
