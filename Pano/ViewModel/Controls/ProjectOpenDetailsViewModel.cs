using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Helpers;
using Pano.Service;

namespace Pano.ViewModel.Controls
{
    public class ProjectOpenDetailsViewModel : ViewModelBaseExtended
    {
        private readonly INavigationService _navigationService;
        private readonly IProjectsService _projectsService;
        private ProjectViewModel _selectedProject;

        public ProjectOpenDetailsViewModel(INavigationService navigationService,
            IProjectsService projectsService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _projectsService = projectsService ?? throw new ArgumentNullException(nameof(projectsService));

            OpenSelectedProjectCommand = new RelayCommand(
                async () =>
                {
                    MessengerInstance.Send(
                        new PropertyChangedMessage<BusyText>(null, new BusyText { State = true, Text = "Otwieram projekt..." }, "IsBusy"),
                        ViewModelLocator.IsBusyLoadingProjects);

                    var project = await Task.Run(() =>
                    {
                        return _projectsService.GetProject(SelectedProject.Model.ProjectId);
                    });

                    _navigationService.NavigateTo(ViewModelLocator.ProjectMainKey, new ProjectViewModel(project));

                    MessengerInstance.Send(
                        new PropertyChangedMessage<BusyText>(null, new BusyText { State = false }, "IsBusy"),
                        ViewModelLocator.IsBusyLoadingProjects);
                },
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