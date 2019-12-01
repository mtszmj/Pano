using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Pano.Helpers;

namespace Pano.ViewModel.Controls
{
    public class BusyViewModel : ViewModelBaseExtended
    {
        private static int counter = 0;
        private bool _isBusy;
        private string _isBusyText;
        private bool _hasSnackbar;
        private string _hasSnackbarText;
        private int id;
        private string _name;

        public BusyViewModel()
        {
            id = ++counter;
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public string IsBusyText
        {
            get => _isBusyText;
            set => Set(ref _isBusyText, value);
        }

        public bool HasSnackbar
        {
            get => _hasSnackbar;
            set => Set(ref _hasSnackbar, value);
        }

        public string HasSnackbarText
        {
            get => _hasSnackbarText;
            set => Set(ref _hasSnackbarText, value);
        }

        public int SnackbarTime { get; set; } = 2000;

        public string BusyToken => $"{_name}:busy";
        public string SnackbarToken => $"{_name}:snackbar";

        public string Name
        {
            get => _name;
            set
            {
                Cleanup();
                _name = value;

                MessengerInstance.Register<PropertyChangedMessage<BusyText>>(
                    this,
                    BusyToken,
                    message =>
                    {
                        IsBusyText = message.NewValue?.Text;
                        IsBusy = message.NewValue?.State ?? false;
                    }
                );

                MessengerInstance.Register<PropertyChangedMessage<BusyText>>(
                    this,
                    SnackbarToken,
                    message =>
                    {
                        HasSnackbarText = $"(#{id}): " + message.NewValue?.Text;
                        HasSnackbar = message.NewValue?.State ?? false;

                        Task.Run(() =>
                        {
                            Thread.Sleep(SnackbarTime);
                            HasSnackbar = false;
                            HasSnackbarText = string.Empty;
                        });
                    }
                );
            }
        }

    }
}
