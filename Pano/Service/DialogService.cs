using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Views;
using MessageBox = System.Windows.MessageBox;

namespace Pano.Service
{
    public class DialogService : IDialogService
    {
        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
                afterHideCallback?.Invoke();
            });
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(error.Message, title, MessageBoxButton.OK);
                afterHideCallback?.Invoke();
            });
        }

        public async Task ShowMessage(string message, string title)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
            });
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
                afterHideCallback?.Invoke();
            });
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            return await Task.Run(() =>
            {
                var messageBoxResult = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
                var result = messageBoxResult == MessageBoxResult.OK;
                afterHideCallback?.Invoke(result);
                return result;
            });
        }

        public async Task ShowMessageBox(string message, string title)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
            });
        }
    }
}
