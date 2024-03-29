﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model.Db;
using Pano.Model.Db.Helpers;
using System.Collections.ObjectModel;
using System.Data.Common;
using Pano.Model;
using Pano.Model.Db.Scenes;
using Scene = Pano.Model.Db.Scenes.Scene;
using SceneHotSpot = Pano.Model.Db.HotSpots.SceneHotSpot;

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

        public PanoContext(DbConnection connection) : base(connection, true)
        {
            SetInitializers();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TourForDb> TourForDbs { get; set; }
        public DbSet<Model.Db.Scenes.DefaultSceneConfig> DefaultScenes { get; set; }
        public DbSet<Model.Db.Scenes.Scene> Scenes { get; set; }
        public DbSet<Model.Db.HotSpots.HotSpot> HotSpots { get; set; }
        public DbSet<StringDictionaryEntry> StringDictionaryEntries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageData> ImageDatas { get; set; }

        private void SetInitializers()
        {
            this.Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer<PanoContext>(new DropCreateDatabaseIfModelChanges<PanoContext>());
            //Database.SetInitializer(new PanoDbInitializerDropCreateDatabaseAlways());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOptional(p => p.Tour)
                .WithRequired(t => t.Project);

            modelBuilder.Entity<TourForDb>()
                .HasMany(t => t.Scenes)
                .WithRequired(s => s.Tour)
                .HasForeignKey(s => s.TourId);

            modelBuilder.Entity<TourForDb>()
                .HasRequired(t => t.Default)
                .WithRequiredPrincipal(d => d.Tour)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<DefaultSceneConfig>()
                .HasOptional(x => x.FirstScene)
                .WithMany()
                .HasForeignKey(dsc => dsc.FirstSceneId);

            modelBuilder.Entity<Scene>()
                .HasMany(s => s.Strings)
                .WithRequired(sde => sde.Scene)
                .HasForeignKey(sde => sde.SceneId);

            modelBuilder.Entity<Scene>()
                .HasMany(s => s.HotSpots)
                .WithRequired(h => h.Scene)
                .HasForeignKey(h => h.SceneId);

            modelBuilder.Entity<Scene>()
                .HasMany(s => s.Images)
                .WithOptional(i => i.Scene)
                .HasForeignKey(i => i.SceneId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Image>()
                .HasOptional(i => i.ImageData)
                .WithRequired(d => d.Image)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SceneHotSpot>()
                .HasOptional(h => h.TargetScene)
                .WithMany()
                .HasForeignKey(h => h.TargetSceneId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Image>()
                .Ignore(x => x.Data)
                .Ignore(x => x.BitmapImage)
                ;

            modelBuilder.Properties<DateTime>()
                .Configure(p => p.HasColumnType("datetime2"));
        }
    }
}
