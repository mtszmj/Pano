using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace Pano.ViewModel.Controls
{
    public class ProjectSummaryViewModel : ViewModelBaseDecorator
    {
        private ProjectViewModel _selectedProject;

        public ProjectSummaryViewModel()
        {
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
            }
        }

    }
}
