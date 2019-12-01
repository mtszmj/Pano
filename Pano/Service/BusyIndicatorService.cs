using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Pano.Helpers;
using Pano.ViewModel;

namespace Pano.Service
{
    public class BusyIndicatorService : IBusyIndicatorService
    {
        private string BusyToken { get; }
        private string SnackbarToken { get; }

        public BusyIndicatorService(string baseToken)
        {
            BusyToken = $"{baseToken}:busy";
            SnackbarToken = $"{baseToken}:snackbar";
        }

        public void SetBusy(string text = "")
        {
            Messenger.Default.Send(new PropertyChangedMessage<BusyText>(
                    this,
                    null,
                    new BusyText { State = true, Text = text },
                    "Busy"),
                BusyToken);
        }
        public void ResetBusy()
        {
            Messenger.Default.Send(new PropertyChangedMessage<BusyText>(
                    this,
                    null,
                    new BusyText { State = false, Text = string.Empty },
                    "Busy"),
                BusyToken);
        }

        public void Busy(bool isBusy, string text = "")
        {
            if(isBusy)
                SetBusy(text);
            else
                ResetBusy();
        }


        public void SetSnackbar(string text)
        {
            Messenger.Default.Send(new PropertyChangedMessage<BusyText>(
                    this,
                    null,
                    new BusyText { State = true, Text = text },
                    "Busy"),
                SnackbarToken);
        }
    }
}
