using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using HotSpot = Pano.Model.Db.HotSpots.HotSpot;
using Scene = Pano.Model.Db.Scenes.Scene;

namespace Pano.Service.Design
{
    public class DesignProjectsService : IProjectsService
    {
        List<Project> _list;

        public DesignProjectsService()
        {
            var weekAgo = DateTime.Today - TimeSpan.FromDays(7);
            var yesterday = DateTime.Now - TimeSpan.FromDays(1);
            var today = DateTime.Today + TimeSpan.FromHours(8);

            _list = new[]
            {
                new Project()
                {
                    ProjectId = new Random().Next(),
                    DateOfCreation = weekAgo,
                    DateOfLastModification = weekAgo,
                    Name = "PROJEKT_01",
                    Description = "Projekt sprzed tygodnia"
                },

                new Project()
                {
                    ProjectId = new Random().Next(),
                    DateOfCreation = yesterday,
                    DateOfLastModification = yesterday,
                    Name = "PROJEKT_02",
                    Description = "Projekt z wczoraj"
                },

                new Project()
                {
                    ProjectId = new Random().Next(),
                    DateOfCreation = today,
                    DateOfLastModification = today,
                    Name = "PROJEKT_03",
                    Description = "Projekt z dzisiaj"
                },
            }.ToList();

        }

        public void GetProjects(Action<IList<Project>, Exception> callback)
        {
            callback?.Invoke(_list, null);
        }

        public Project GetProject(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(Project project)
        {
            project.DateOfLastModification = DateTime.Now;
            return 1;
        }

        public int SaveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveScene(Scene scene)
        {
            throw new NotImplementedException();
        }

        public void RemoveHotSpot(HotSpot spot)
        {
            throw new NotImplementedException();
        }
    }
}
