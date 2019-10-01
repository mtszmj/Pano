using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Views;

namespace Pano.Service
{
    public class DialogService : IDialogService
    {
        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return Task.Run(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
                afterHideCallback?.Invoke();
            });
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            return Task.Run(() =>
            {
                MessageBox.Show(error.Message, title, MessageBoxButton.OK);
                afterHideCallback?.Invoke();
            });
        }

        public Task ShowMessage(string message, string title)
        {
            return Task.Run(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
            });
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            return Task.Run(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
                afterHideCallback?.Invoke();
            });
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            return Task.Run(() =>
            {
                var messageBoxResult = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
                var result = messageBoxResult == MessageBoxResult.OK;
                afterHideCallback?.Invoke(result);
                return result;
            });
        }

        public Task ShowMessageBox(string message, string title)
        {
            return Task.Run(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
            });
        }
    }
}
