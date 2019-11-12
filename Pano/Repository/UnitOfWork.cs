using System;
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
        }

        public IHotSpotRepository HotSpots { get; }
        public IProjectRepository Projects { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
