using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace Pano.ViewModel.Pages
{
    public class NewProjectPageViewModel : ViewModelBaseExtended
    {
        private INavigationService _navigationService;
        private ProjectViewModel _selectedProject;

        public NewProjectPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            BackCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo(ViewModelLocator.InitPageKey);
            });
        }

        public RelayCommand BackCommand { get; set; }

        public ProjectViewModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                var oldValue = _selectedProject;
                _selectedProject = value;
                RaisePropertyChanged();

                var msg = new PropertyChangedMessage<ProjectViewModel>(oldValue, _selectedProject, nameof(SelectedProject));
                MessengerInstance.Send(msg, ViewModelLocator.ProjectToCreateToken);
            }
        }
    }
}
