using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
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
                () =>
                {
                    var project = _projectsService.GetProject(SelectedProject.Model.ProjectId);
                    _navigationService.NavigateTo(ViewModelLocator.ProjectMainKey, new ProjectViewModel(project));
                    project.Description += " test"; // TODO delete
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