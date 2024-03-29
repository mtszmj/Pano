﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.DB;

namespace Pano.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PanoContext _context;

        public UnitOfWork(PanoContext context)
        {
            _context = context;
            HotSpots = new HotSpotRepository(context);
            Projects = new ProjectRepository(context);
            Scenes = new SceneRepository(context);
            Images = new ImageRepository(context);
            ImageDatas = new ImageDataRepository(context);
            DefaultSceneConfigs = new DefaultSceneConfigRepository(context);
        }

        public IHotSpotRepository HotSpots { get; }
        public IProjectRepository Projects { get; }
        public ISceneRepository Scenes { get; }
        public IImageRepository Images { get; }
        public IImageDataRepository ImageDatas { get; }
        public IDefaultSceneConfigRepository DefaultSceneConfigs { get; }
        public int Complete()
        {
            ImageDatas.CheckChanges();
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
