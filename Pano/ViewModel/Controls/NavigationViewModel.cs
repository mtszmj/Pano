using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace Pano.ViewModel.Controls
{
    public class NavigationViewModel : ViewModelBaseExtended
    {
        private INavigationService _navigationService;

        public string CurrentPage { get; set; }
        public RelayCommand NavigateToProjects { get; }
        public RelayCommand NavigateToPanorama { get; }

        public NavigationViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;

            NavigateToProjects = new RelayCommand(
                () =>
                {
                    NavigateTo(ViewModelLocator.ProjectsKey);

                },
                () => CurrentPage != ViewModelLocator.ProjectsKey
            );

            NavigateToPanorama = new RelayCommand(
                () =>
                {
                    NavigateTo(ViewModelLocator.PanoramaKey);
                },
                () => (CurrentPage != ViewModelLocator.PanoramaKey)
            );

            MessengerInstance.Register<PropertyChangedMessage<string>>(
                this, 
                ViewModelLocator.CurrentPageToken,
                message =>
                {
                    CurrentPage = message.NewValue;
                    RaisePropertyChanged(nameof(CurrentPage));
                    NavigateToProjects.RaiseCanExecuteChanged();
                    NavigateToPanorama.RaiseCanExecuteChanged();
                });

        }

        private void NavigateTo(string pageKey)
        {
            _navigationService?.NavigateTo(pageKey);
        }
    }
}