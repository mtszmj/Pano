using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Model;
using Pano.Service;

namespace Pano.ViewModel
{
    public class ProjectsListViewModel : ViewModelBaseDecorator
    {
        private IProjectsService _projectsService;
        public ProjectsListViewModel(IProjectsService projectService,
            IDialogService dialogService)
        {
            _projectsService = projectService;
            DialogService = dialogService;

            LoadProjectsCommand = new RelayCommand(
                () => _projectsService.GetProjects(GetProjectsCompleted));

            if (IsInDesignMode)
            {
                _projectsService.GetProjects(GetProjectsCompleted);
            }
        }

        public ObservableCollection<ProjectViewModel> Projects { get; private set; }
        
        public IDialogService DialogService { get; set; }

        public RelayCommand LoadProjectsCommand { get; }

        private ProjectViewModel _selectedProject = null;

        public ProjectViewModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                var oldValue = _selectedProject;
                _selectedProject = value;
                RaisePropertyChanged();

                var msg = new PropertyChangedMessage<ProjectViewModel>(oldValue, _selectedProject, nameof(SelectedProject));
                MessengerInstance.Send(msg, ViewModelLocator.ProjectToOpenToken);
            }
        }

        private async void GetProjectsCompleted(IList<Project> result, Exception exception)
        {
            if (exception != null)
            {
                await DialogService.ShowError(exception, "Exception", "OK", null);
                return;
            }

            Projects = new ObservableCollection<ProjectViewModel>();
            foreach (var project in result)
            {
                Projects.Add(new ProjectViewModel(project));
            }

            RaisePropertyChanged(nameof(Projects));
            await DialogService.ShowMessage("Wczytano projekty", "Info");
        }
    }
}
