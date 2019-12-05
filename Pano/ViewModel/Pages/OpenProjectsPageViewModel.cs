using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Helpers;
using Pano.Service;

namespace Pano.ViewModel.Pages
{
    public class OpenProjectsPageViewModel : ViewModelBaseExtended
    {
        private INavigationService _navigationService;
        private readonly IBusyIndicatorService _busyIndicatorService;

        public OpenProjectsPageViewModel(INavigationService navigationService,
            Func<string,IBusyIndicatorService> busyIndicatorService)
        {
            _navigationService = navigationService;
            _busyIndicatorService = busyIndicatorService("OpenProjectsViewBusy");

            BackCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo(ViewModelLocator.InitPageKey);
            });

            Messenger.Default.Register<PropertyChangedMessage<BusyText>>(
                this,
                ViewModelLocator.IsBusyLoadingProjects,
                message =>
                {
                    if (message.NewValue?.State == true)
                        _busyIndicatorService.SetBusy(message.NewValue.Text);
                    else
                        _busyIndicatorService.ResetBusy();
                }
            );

            Messenger.Default.Register<PropertyChangedMessage<BusyText>>(
                this,
                ViewModelLocator.HasFinishedLoadingProjects,
                message =>
                {
                    _busyIndicatorService.SetSnackbar(message.NewValue.Text);
                }
            );
        }

        public ICommand BackCommand { get; set; }


    }
}
