using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.DB;

namespace Pano.IntegrationTests.Helpers
{
    internal class DropCreateDatabaseAlwaysAndSeed : DropCreateDatabaseAlways<PanoContext>
    {
        public override void InitializeDatabase(PanoContext context)
        {
            if (context.Database.Exists())
            {
                context.Database.ExecuteSqlCommand(
                    TransactionalBehavior.DoNotEnsureTransaction,
                    $"ALTER DATABASE [{context.Database.Connection.Database}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE"
                );
            }

            base.InitializeDatabase(context);
        }
    }
}
