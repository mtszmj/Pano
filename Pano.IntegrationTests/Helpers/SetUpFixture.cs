using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Pano.DB;
using Pano.Repository;
using Pano.Service;

namespace Pano.IntegrationTests.Helpers
{
    [SetUpFixture]
    internal class SetUpFixture
    {
        internal static PanoContext Context = new PanoContext();
        internal static IProjectsService Service;

        [OneTimeSetUp]
        public void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseAlwaysAndSeed());
            Context.Database.Initialize(false);

            Service = new ProjectsService(new UnitOfWork(Context));
        }
    }
}
