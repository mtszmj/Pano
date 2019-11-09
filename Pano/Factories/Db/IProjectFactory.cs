using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;

namespace Pano.Factories.Db
{
    public interface IProjectFactory
    {
        Project NewProject();
    }
}
