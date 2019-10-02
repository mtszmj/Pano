using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Pano.Model;
using Pano.Service;

namespace Pano.ViewModel
{
    public class MainViewModel : ViewModelBaseDecorator
    {
        public string Title { get; set; }
        public string CurrentPage { get; set; }

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

        public Visibility NavigationVisibility => NavigationOn ? Visibility.Visible : Visibility.Collapsed;

        public MainViewModel()
        {
            Title = IsInDesignMode ? "Pano (Design Mode)" : "Pano";

            RegisterForMessages();
        }

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
    }
}