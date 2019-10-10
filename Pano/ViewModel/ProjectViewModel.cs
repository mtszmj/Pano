using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Pano.Model;
using Pano.Service;

namespace Pano.ViewModel
{
    public class ProjectViewModel : ViewModelBaseExtended
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

            Model.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Project.Name))
                {
                    MessengerInstance.Send(new object(), ViewModelLocator.CommandRequeryToken);
                }
            };
        }

        public Project Model { get; }
        public Visibility ModifiedToday => Model.DateOfLastModification > DateTime.Today ? Visibility.Visible : Visibility.Collapsed;

        public static class Factory
        {
            public static ProjectViewModel New => new ProjectViewModel(new Project());
            public static ProjectViewModel TestObject
            {
                get
                {
                    var today = DateTime.Today + TimeSpan.FromHours(8);

                    return new ProjectViewModel(new Project()
                    {
                        DateOfCreation = today,
                        DateOfLastModification = today,
                        Name = "PROJEKT_03",
                        Description = "Projekt z dzisiaj"
                    });
                }
            }
        }
    }
}
