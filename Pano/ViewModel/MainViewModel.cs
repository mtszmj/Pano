using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Model;
using Pano.Repository;
using Pano.Service;

namespace Pano.ViewModel
{
    public class MainViewModel : ViewModelBaseExtended
    {
        private readonly INavigationService _navigationService;
        private readonly IProjectsService _projectsService;
        private readonly IDialogService _dialogService;

        public MainViewModel(INavigationService navigationService, 
            IProjectsService projectsService, 
            IDialogService dialogService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _projectsService = projectsService ?? throw new ArgumentNullException(nameof(projectsService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Title = IsInDesignMode ? "Pano (Design Mode)" : "Pano";

            SaveCommand = new RelayCommand(SaveProject);

            RegisterForMessages();
        }

        public string Title { get; set; }
        public string CurrentPage { get; set; }

        private Frame _mainFrame;
        public Frame MainFrame { get => _mainFrame;
            set
            {
                Set(ref _mainFrame, value);
                (_navigationService as IInitializeService<Frame>)?.InitializeService(_mainFrame);
                _navigationService.NavigateTo(ViewModelLocator.InitPageKey);
            }
        }

        private bool _navigationOn = true;
        public bool NavigationOn
        {
            get => _navigationOn;
            set
            {
                _navigationOn = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(NavigationVisibility));
            }
        }

        public RelayCommand SaveCommand { get; set; }

        public Visibility NavigationVisibility => NavigationOn ? Visibility.Visible : Visibility.Collapsed;

        public void RegisterForMessages()
        {
            MessengerInstance.Register<PropertyChangedMessage<bool>>(
                this,
                ViewModelLocator.NavigationVisibleToken,
                message =>
                {
                    NavigationOn = message.NewValue;
                });
        }

        public void SendMessageWithCurrentPageTitle(Page page)
        {
            var newValue = ViewModelLocator.Locator.PageTitle.TryGetValue(page.GetType(), out var value) ? value : "";
            var msg = new PropertyChangedMessage<string>(null, newValue, nameof(CurrentPage));

            MessengerInstance.Send(msg, ViewModelLocator.CurrentPageToken);
        }

        private void SaveProject()
        {
            Console.WriteLine("TEST");
            _projectsService.SaveAll();
            Task.Run(() => _dialogService.ShowMessageBox("Zapisano", "Zapis"));
        }
    }
}