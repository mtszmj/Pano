using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace Pano.ViewModel.Control
{
    public class ProjectNewViewModel : ViewModelBaseDecorator
    {
        private readonly INavigationService _navigationService;
        private ProjectViewModel _selectedProject;

        public ProjectNewViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            CreateSelectedProjectCommand = new RelayCommand(
                () => _navigationService.NavigateTo(ViewModelLocator.ProjectMainKey, SelectedProject),
                () => Test());

            Messenger.Default.Register<PropertyChangedMessage<ProjectViewModel>>(
                this,
                ViewModelLocator.ProjectToCreateToken,
                message =>
                {
                    SelectedProject = null;
                    SelectedProject = message.NewValue;
                });

            Messenger.Default.Register<object>(
                this,
                ViewModelLocator.CommandRequeryToken,
                message =>
                {
                    CreateSelectedProjectCommand.RaiseCanExecuteChanged();
                });


        }

        public bool Test()
        {
            var result = SelectedProject?.Model?.Name?.Length >= 3;
            return result;
        }

        public ProjectViewModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                if (_selectedProject == value)
                {
                    return;
                }

                _selectedProject = value;
                RaisePropertyChanged(string.Empty);
                CreateSelectedProjectCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CreateSelectedProjectCommand { get; }
    }
}
