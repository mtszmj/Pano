using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Model.Db.Helpers;

namespace Pano.DB
{
    public class PanoDbInitializerDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<PanoContext>
    {
        PanoDbInitializerSeed Seed = new PanoDbInitializerSeed();

        public override void InitializeDatabase(PanoContext context)
        {
            //context.Projects.Add(Seed.SeedProject());
            base.Seed(context);
        }
    }
}
