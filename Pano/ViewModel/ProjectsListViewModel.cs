using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pano.Model;
using Pano.Service;

namespace Pano.ViewModel
{
    public class ProjectsListViewModel : ViewModelBase
    {
        private IProjectsService _projectsService;

        public ProjectsListViewModel(IProjectsService projectService)
        {
            _projectsService = projectService;

            LoadProjectsCommand = new RelayCommand(
                () => _projectsService.GetProjects(GetProjectsCompleted));

            if (IsInDesignMode)
            {
                _projectsService.GetProjects(GetProjectsCompleted);
            }
        }

        public ObservableCollection<ProjectViewModel> Projects { get; private set; }


        public RelayCommand LoadProjectsCommand { get; }

        private ProjectViewModel _selectedProject = null;

        public ProjectViewModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                var oldValue = _selectedProject;
                _selectedProject = value;
                RaisePropertyChanged(nameof(SelectedProject), oldValue, _selectedProject, true);
            }
        }
        private void GetProjectsCompleted(IList<Project> result, Exception exception)
        {
            if (exception != null)
            {
                throw exception;
            }

            Projects = new ObservableCollection<ProjectViewModel>();
            foreach (var project in result)
            {
                Projects.Add(new ProjectViewModel(project));
            }

            RaisePropertyChanged(nameof(Projects));
        }

        public string VmName => nameof(ProjectsListViewModel);
    }
}
