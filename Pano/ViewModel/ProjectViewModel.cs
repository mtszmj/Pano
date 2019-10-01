using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pano.Model;
using Pano.Service;

namespace Pano.ViewModel
{
    public class ProjectViewModel : ViewModelBaseDecorator
    {
        public ProjectViewModel(Project model)
        {
            Model = model;

            Model.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Project.DateOfLastModification))
                {
                    RaisePropertyChanged(nameof(ModifiedToday));
                }
            };
        }

        public Project Model { get; private set; }
        public Visibility ModifiedToday => Model.DateOfLastModification > DateTime.Today ? Visibility.Visible : Visibility.Collapsed;
    }
}
