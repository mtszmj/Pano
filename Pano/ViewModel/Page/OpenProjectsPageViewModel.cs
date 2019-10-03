using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace Pano.ViewModel
{
    public class OpenProjectsPageViewModel : ViewModelBaseDecorator
    {
        private INavigationService _navigationService;
        public RelayCommand BackCommand { get; set; }

        public OpenProjectsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            BackCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo(ViewModelLocator.InitPageKey);
            });
        }
    }
}
