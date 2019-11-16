using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Model.Db.Scenes;

namespace Pano.Factories.Db
{
    public class ProjectFactory : IProjectFactory
    {
        public Project NewProject()
        {
            var now = DateTime.Now;
            var project = new Project()
            {
                DateOfCreation = now,
                DateOfLastModification = now,
            };

            var tour = new TourForDb();
            var config = new DefaultSceneConfig();

            tour.Default = config;
            config.Tour = tour;

            project.Tour = tour;
            tour.Project = project;

            return project;
        }

        public Project NewProject(string name, string description = "")
        {
            var project = NewProject();
            project.Name = name;
            project.Description = description;

            return project;
        }
    }
}
