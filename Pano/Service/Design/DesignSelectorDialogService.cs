using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Command;

namespace Pano.Service.Design
{
    public class DesignSelectorDialogService<TSelected> : SelectorDialogService<TSelected>, ISelectorDialogService<TSelected>
    {
        public DesignSelectorDialogService(Type window, int numberOfButtons) : base(window, numberOfButtons)
        {
            Buttons = new List<string>()
            {
                "button 1",
                "button 2",
                "button 3",
                "button 4",
            };
        }
    }
}
