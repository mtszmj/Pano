using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Pano.DB;
using Pano.Service;

namespace Pano.IntegrationTests.Helpers
{
    internal class BaseIntegrationFixture
    {
        protected PanoContext Context => SetUpFixture.Context;
        protected IProjectsService Service => SetUpFixture.Service;

        protected BaseIntegrationFixture()
        {
        }

        [TearDown]
        public void ResetChangeTracker()
        {
            IEnumerable<DbEntityEntry> changedEntriesCopy = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted
                );
            foreach (DbEntityEntry entity in changedEntriesCopy)
            {
                Context.Entry(entity.Entity).State = EntityState.Detached;
            }
        }
    }
}
