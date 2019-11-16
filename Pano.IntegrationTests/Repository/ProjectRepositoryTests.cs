using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Pano.DB;
using Pano.Factories.Db;
using Pano.IntegrationTests.Examples;
using Pano.Model;
using Pano.Repository;
using Pano.Service;

namespace Pano.IntegrationTests.Repository
{
    [TestFixture]
    public class ProjectRepositoryTests
    {
        [Test]
        public void AddProjectToDbTest()
        {
            var dbConnection = Effort.DbConnectionFactory.CreateTransient();
            var context = new PanoContext(dbConnection);
            var unitOfWork = new UnitOfWork(context);
            var service = new ProjectsService(unitOfWork);
            var projectFactory = new ProjectFactory();
            var project = projectFactory.NewProject("TestProject", "TestDescription");

            service.Save(project);

            List<Project> list = new List<Project>();
            service.GetProjects((p, e) =>
            {
                if (e != null)
                    Assert.Fail("Exception during projects retrieval");

                list = p.ToList();
            });

            if (list.Count != 1)
            {
                Assert.Fail("List does not contain 1 project");
            }
        }
    }
}
