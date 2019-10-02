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
    public class OpenProjectsViewModel : ViewModelBaseDecorator
    {
        private INavigationService _navigationService;
        public RelayCommand BackCommand { get; set; }

        public OpenProjectsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            BackCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo(ViewModelLocator.InitPageKey);
            });
        }
    }
}
