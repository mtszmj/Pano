using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Service;

namespace Pano.ViewModel
{
    public class ProjectOpenDetailsViewModel : ViewModelBaseDecorator
    {
        private readonly INavigationService _navigationService;
        private ProjectViewModel _selectedProject;

        public ProjectOpenDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            OpenSelectedProjectCommand = new RelayCommand(
                () => _navigationService.NavigateTo(ViewModelLocator.ProjectMainKey, SelectedProject),
                () => SelectedProject?.Model != null);

            Messenger.Default.Register<PropertyChangedMessage<ProjectViewModel>>(
                this,
                ViewModelLocator.ProjectToOpenToken,
                message =>
                {
                    SelectedProject = null;
                    SelectedProject = message.NewValue;
                });
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
                RaisePropertyChanged();
                OpenSelectedProjectCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand OpenSelectedProjectCommand { get; }
    }
}