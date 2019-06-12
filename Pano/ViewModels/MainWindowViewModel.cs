using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Pano.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        //TODO - converter
        public Visibility IsDragging => IsRequiredFileIncludedInDragDrop ? Visibility.Visible : Visibility.Collapsed;

        private readonly string[] AllowedExtensions = new[] { ".jpg", ".png" };

        private bool _IsRequiredFileIncludedInDragDrop;
        public bool IsRequiredFileIncludedInDragDrop
        {
            get => _IsRequiredFileIncludedInDragDrop;
            set
            {
                SetField(ref _IsRequiredFileIncludedInDragDrop, value);
                OnPropertyChanged(nameof(IsDragging));
            }
        }

        public void DragEnter(DragEventArgs e)
        {
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            
            if (paths != null)
            {
                var directories = paths.Where(x => Directory.Exists(x));
                
                var filesFromDirectories = directories
                    .SelectMany(x => Directory.EnumerateFiles(x, "*.*", SearchOption.AllDirectories))
                    .Where(x => AllowedExtensions.Any(ext => x.ToLower().EndsWith(ext)));
                var files = paths
                    .Where(x => AllowedExtensions.Any(ext => x.ToLower().EndsWith(ext)))
                    .Concat(filesFromDirectories);

                IsRequiredFileIncludedInDragDrop = files.Any();
            }
        }

        public void DragOver()
        {
        }

        public void DragDrop(DragEventArgs e)
        {
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (paths != null)
            {
                var directories = paths.Where(x => Directory.Exists(x));
                var filesFromDirectories = directories
                    .SelectMany(x => Directory.EnumerateFiles(x, "*.*", SearchOption.AllDirectories))
                    .Where(x => AllowedExtensions.Any(ext => x.ToLower().EndsWith(ext)));
                var files = paths
                    .Where(x => AllowedExtensions.Any(ext => x.ToLower().EndsWith(ext)))
                    .Concat(filesFromDirectories);

                if (files.Any())
                {
                    foreach (var file in files)
                    {
                        Console.Out.WriteLine(file);
                    }
                }
            }

            IsRequiredFileIncludedInDragDrop = false;
        }

        public void DragLeave()
        {
            IsRequiredFileIncludedInDragDrop = false;
        }
    }
}
