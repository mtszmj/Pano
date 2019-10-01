using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;

namespace Pano.Service.Design
{
    public class DesignDialogService : IDialogService
    {
        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return Task.CompletedTask;
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            return Task.CompletedTask;
        }

        public Task ShowMessage(string message, string title)
        {
            return Task.CompletedTask;
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            return Task.CompletedTask;
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            return Task.FromResult(true);
        }

        public Task ShowMessageBox(string message, string title)
        {
            return Task.CompletedTask;
        }
    }
}
