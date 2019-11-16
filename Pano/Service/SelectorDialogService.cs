using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace Pano.Service
{
    public class SelectorDialogService<TSelected> : ISelectorDialogService<TSelected>
    {
        public SelectorDialogService(Type window, int numberOfButtons)
        {
            Window = window ?? throw new ArgumentNullException(nameof(window));
            NumberOfButtons = numberOfButtons;
        }

        public Type Window { get; set; }
        public IList<string> Buttons { get; set; }
        public RelayCommand<string> Command { get; set; }
        public IList<TSelected> ListToSelectFrom { get; set; }
        public TSelected Selected { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public int NumberOfButtons { get; set; }
        public Action<TSelected, int?> SelectionByButtonIndex { get; private set; }

        private Window CurrentWindow { get; set; }

        public void ShowDialog(IList<string> buttons,
            IEnumerable<TSelected> listToSelectFrom,
            Action<TSelected, int?> selectionByButtonIndex,
            string label)
        {
            if (buttons != null)
                Buttons = buttons;

            ListToSelectFrom = listToSelectFrom.ToList();

            SelectionByButtonIndex = selectionByButtonIndex;
            Command = new RelayCommand<string>(CommandToInvoke);

            Label = label;

            if (!((Activator.CreateInstance(Window)) is Window window))
                throw new InvalidOperationException();
            CurrentWindow = window;
            CurrentWindow.DataContext = this;
            CurrentWindow.ShowDialog();
        }

        protected void CommandToInvoke(string buttonIndex)
        {
            if (int.TryParse(buttonIndex, out int index))
                SelectionByButtonIndex?.Invoke(Selected, index);

            CurrentWindow.Close();
            CurrentWindow = null;
        }
    }
}
