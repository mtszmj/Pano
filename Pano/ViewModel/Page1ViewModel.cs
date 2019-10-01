using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace Pano.ViewModel
{
    public class Page1ViewModel : ViewModelBaseDecorator
    {
        public Page1ViewModel()
        {
            
        }

        public Page1ViewModel(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
