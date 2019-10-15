using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Model;
using Pano.Service;

namespace Pano.ViewModel.Pages
{
    public class ProjectPageViewModel : ViewModelBaseExtended
    {
        private readonly IDialogService _dialogService;
        private readonly IProjectsService _projectsService;
        private ProjectViewModel _project;

        public ProjectPageViewModel(IDialogService dialogService, IProjectsService projectsService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _projectsService = projectsService ?? throw new ArgumentNullException(nameof(projectsService));

            SaveCommand = new RelayCommand(SaveProject);
        }

        public ProjectViewModel Project
        {
            get => _project;
            set
            {
                if (_project == value)
                    return;

                var oldValue = _project;
                _project = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }

        private void SaveProject()
        {
            var result = _projectsService.Save(this.Project.Model);
            Task.Run(() => _dialogService.ShowMessageBox($"Zapisano {result}", "Zapis"));
        }

        public override void InitializeView()
        {
            MessengerInstance.Send(
                new PropertyChangedMessage<bool>(
                    this, 
                    false, 
                    true, 
                    ViewModelLocator.NavigationVisibleToken
                    ), 
                ViewModelLocator.NavigationVisibleToken
            );
        }
    }
}
