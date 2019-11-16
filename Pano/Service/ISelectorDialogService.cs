using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace Pano.Service
{
    public interface ISelectorDialogService<TSelected>
    {
        Type Window { get; set; }
        IList<string> Buttons { get; set; }
        RelayCommand<string> Command { get; set; }
        IList<TSelected> ListToSelectFrom { get; set; }
        TSelected Selected { get; set; }
        string Label { get; set; }
        string Description { get; set; }

        void ShowDialog(IList<string> buttons, 
            IEnumerable<TSelected> listToSelectFrom,
            Action<TSelected, int?> selectionByButtonIndex,
            string label);
    }
}
