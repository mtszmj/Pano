using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Model;

namespace Pano.ViewModel
{
    public class InitPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public InitPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NewCommand = new RelayCommand(() => _navigationService.NavigateTo(ViewModelLocator.NewProjectKey, new ProjectViewModel(new Project())));
            OpenCommand = new RelayCommand(() => _navigationService.NavigateTo(ViewModelLocator.ProjectsKey));
        }

        public RelayCommand NewCommand { get; }
        public RelayCommand OpenCommand { get; }
        
        public void NavigatedTo()
        {
            MessengerInstance.Send(
                new PropertyChangedMessage<bool>(false, false, null), 
                ViewModelLocator.NavigationVisibleToken
                );
        }


    }
}
