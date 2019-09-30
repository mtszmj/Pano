using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;

namespace Pano.Service.Design
{
    public class DesignProjectsService : IProjectsService
    {
        public void GetProjects(Action<IList<Project>, Exception> callback)
        {
            var weekAgo = DateTime.Today - TimeSpan.FromDays(7);
            var yesterday = DateTime.Now - TimeSpan.FromDays(1);
            var today = DateTime.Today + TimeSpan.FromHours(8);

            var list = new[]
            {
                new Project()
                {
                    DateOfCreation = weekAgo,
                    DateOfLastModification = weekAgo,
                    Name = "PROJEKT_01",
                    Description = "Projekt sprzed tygodnia"
                },

                new Project()
                {
                    DateOfCreation = yesterday,
                    DateOfLastModification = yesterday,
                    Name = "PROJEKT_02",
                    Description = "Projekt z wczoraj"
                },

                new Project()
                {
                    DateOfCreation = today,
                    DateOfLastModification = today,
                    Name = "PROJEKT_03",
                    Description = "Projekt z dzisiaj"
                },
            }.ToList();

            callback?.Invoke(list, null);
        }

        public void Save(Project project)
        {
            project.DateOfLastModification = DateTime.Now;
        }
    }
}
