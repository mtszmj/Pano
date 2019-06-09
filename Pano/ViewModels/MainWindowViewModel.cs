using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pano.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public Visibility IsDragging => IsRequiredFileIncludedInDragDrop ? Visibility.Visible : Visibility.Collapsed;

        private DragDropEffects EffectOk { get; } = DragDropEffects.Copy;
        private DragDropEffects EffectNok { get; } = DragDropEffects.None;

        private bool _IsRequiredFileIncludedInDragDrop;
        private bool IsRequiredFileIncludedInDragDrop {
            get => _IsRequiredFileIncludedInDragDrop;
            set
            {
                _IsRequiredFileIncludedInDragDrop = value;
                OnPropertyChanged(nameof(IsDragging));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void DragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var drop = (string[])e.Data.GetData(DataFormats.FileDrop);

                var directories = drop.Where(x => Directory.Exists(x));
                var filesFromDirectories = directories.SelectMany(x => Directory.GetFiles(x, "*.jpg", SearchOption.AllDirectories));

                var files = drop.Where(x => x.ToLower().EndsWith(".jpg")).Concat(filesFromDirectories);

                if (files.Any())
                {
                    e.Handled = true;
                    e.Effects = EffectOk;
                    IsRequiredFileIncludedInDragDrop = true;

                    foreach (var file in files)
                    {
                        Console.Out.WriteLine(file);
                    }
                }
                else
                {
                    e.Handled = true;
                    e.Effects = EffectNok;
                    IsRequiredFileIncludedInDragDrop = false;
                }
            }
        }

        public void DragOver(DragEventArgs e)
        {
            e.Handled = true;

            if (IsRequiredFileIncludedInDragDrop)
            {
                e.Effects = EffectOk;
            }
            else
            {
                e.Effects = EffectNok;
            }
        }

        public void DragDrop(DragEventArgs e)
        {
            e.Handled = true;
            if (IsRequiredFileIncludedInDragDrop)
            {
                e.Effects = EffectOk;
            }
            else
            {
                e.Effects = EffectNok;
            }
            IsRequiredFileIncludedInDragDrop = false;
        }

        public void DragLeave(DragEventArgs e)
        {
            IsRequiredFileIncludedInDragDrop = false;
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
