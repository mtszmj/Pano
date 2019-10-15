using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Model.Db;

namespace Pano.DB
{
    public class PanoContext : DbContext
    {
        public PanoContext() : base()
        {
            SetInitializers();
        }
        public PanoContext(string connection) : base(connection)
        {
            SetInitializers();
        }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<TourForDb> TourForDbs { get; set; }
        public DbSet<Model.Db.Scenes.Scene> Scenes { get; set; }
        public DbSet<Model.Db.HotSpots.HotSpot> HotSpots { get; set; }

        private void SetInitializers()
        {
            Database.SetInitializer<PanoContext>(new DropCreateDatabaseIfModelChanges<PanoContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasOptional(p => p.Tour)
                .WithRequired(t => t.Project);

            modelBuilder.Entity<TourForDb>()
                .HasOptional(x => x.Default)
                .WithRequired(x => x.Tour);

            modelBuilder.Entity<Model.Db.Scenes.DefaultScene>()
                .HasOptional(x => x.FirstSceneRef);

        }
    }
}
