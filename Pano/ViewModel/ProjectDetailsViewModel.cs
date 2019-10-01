using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Pano.Service;

namespace Pano.ViewModel
{
    public class ProjectDetailsViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private ProjectViewModel _selectedProject;

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
            }
        }

        public RelayCommand SaveSelectedProjectCommand { get; private set; }

        public ProjectDetailsViewModel(IProjectsService projectsService)
        {
            _projectsService = projectsService;

            SaveSelectedProjectCommand = new RelayCommand(
                () => _projectsService.Save(SelectedProject.Model),
                () => SelectedProject?.Model != null);

            Messenger.Default.Register<PropertyChangedMessage<ProjectViewModel>>(
                this,
                message =>
                {
                    SelectedProject = null;
                    SelectedProject = message.NewValue;
                });
        }
    }
}
