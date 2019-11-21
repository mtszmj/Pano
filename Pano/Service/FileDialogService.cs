using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Pano.Helpers;

namespace Pano.Service
{
    public class FileDialogService : IFileDialogService
    {
        private readonly List<string> _filters = new List<string>();
        public void ClearFilters()
        {
            _filters.Clear();
        }

        public void AddFilter(string name, string extension)
        {
            _filters.Add($"{name.Trim()} (*.{extension})|*.{extension}");
        }

        public Option<string> OpenFileDialog()
        {
            var dialog = new OpenFileDialog();

            if (_filters.Any())
                dialog.Filter = string.Join("|", _filters);

            if (dialog.ShowDialog() == true)
                return new Option<string>(dialog.FileName);

            return Option<string>.None;
        }

        public Option<string> SaveFileDialog()
        {
            var dialog = new SaveFileDialog();

            if (_filters.Any())
                dialog.Filter = string.Join("|", _filters);

            if (dialog.ShowDialog() == true)
                return new Option<string>(dialog.FileName);

            return Option<string>.None;
        }
    }
}
