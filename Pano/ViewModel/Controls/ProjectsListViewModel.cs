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

namespace Pano.ViewModel.Controls
{
    public class ProjectsListViewModel : ViewModelBaseExtended
    {
        private IProjectsService _projectsService;
        public IDialogService _dialogService { get; set; }
        public ProjectsListViewModel(IProjectsService projectService,
            IDialogService dialogService)
        {
            _projectsService = projectService;
            _dialogService = dialogService;

            LoadProjectsCommand = new RelayCommand(LoadCommand);

            DeleteProjectCommand = new RelayCommand(DeleteCommand, SelectedProject != null);
        }

        public ObservableCollection<ProjectViewModel> Projects { get; private set; }
        

        public RelayCommand LoadProjectsCommand { get; }
        public RelayCommand DeleteProjectCommand { get; }

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

        private void LoadCommand()
        {
            _projectsService.GetProjects(GetProjectsCompleted);
        }

        private async void GetProjectsCompleted(IList<Project> result, Exception exception)
        {
            if (exception != null)
            {
                await _dialogService.ShowError(exception, "Exception", "OK", null);
                return;
            }

            Projects = new ObservableCollection<ProjectViewModel>();
            foreach (var project in result)
            {
                Projects.Add(new ProjectViewModel(project));
            }

            RaisePropertyChanged(nameof(Projects));
            await _dialogService.ShowMessage("Wczytano projekty", "Info");
        }

        private void DeleteCommand()
        {
            var project = SelectedProject;
            var result = Task.Run(() => _dialogService.ShowMessage("Usunięcie jest nieodwracalne. Czy jesteś pewien?",
                "Usuń projekt", "OK", "Anuluj", null)).Result;

            if (result)
            {
                _projectsService.RemoveProject(project.Model);
                _projectsService.SaveAll();
                Projects.Remove(project);
            }
        }
    }
}
