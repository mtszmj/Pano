using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Helpers;

namespace Pano.Service
{
    public interface IFileDialogService
    {
        void ClearFilters();
        void AddFilter(string name, string extension);
        Option<string> OpenFileDialog();
        Option<string> SaveFileDialog();
    }
}
