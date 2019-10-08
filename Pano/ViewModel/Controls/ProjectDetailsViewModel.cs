using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Service;

namespace Pano.ViewModel.Controls
{
    public class ProjectDetailsViewModel : ViewModelBaseDecorator
    {
        private readonly IProjectsService _projectsService;
        private ProjectViewModel _selectedProject;
        private string _buttonText;

        public ProjectDetailsViewModel(IProjectsService projectsService)
        {
            _projectsService = projectsService;

            SaveSelectedProjectCommand = new RelayCommand(
                () => _projectsService.Save(SelectedProject.Model),
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

        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText == value)
                    return;

                _buttonText = value;
                RaisePropertyChanged();
            }
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
                SaveSelectedProjectCommand.RaiseCanExecuteChanged();
            }
        }
        
        public RelayCommand SaveSelectedProjectCommand { get; }

    }
}
