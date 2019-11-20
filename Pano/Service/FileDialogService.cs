using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Pano.Helpers;

namespace Pano.Service
{
    public class FileDialogService : IFileDialogService
    {
        public Option<string> OpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
                return new Option<string>(dialog.FileName);

            return Option<string>.None;
        }

        public Option<string> SaveFileDialog()
        {
            var dialog = new SaveFileDialog();
            if(dialog.ShowDialog() == true)
                return new Option<string>(dialog.FileName);

            return Option<string>.None;
        }
    }
}
