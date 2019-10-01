using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;

namespace Pano.ViewModel
{
    public class ViewModelGlobalResources : ViewModelBase
    {
        private bool _isInDeveloperMode = true;

        public bool IsInDeveloperMode
        {
            get => _isInDeveloperMode;
            set
            {
                if (_isInDeveloperMode == value)
                    return;

                _isInDeveloperMode = value;
                RaisePropertyChanged(nameof(IsInDeveloperMode));
                RaisePropertyChanged(nameof(IsInDeveloperModeVisibility));
            }
        }

        public Visibility IsInDeveloperModeVisibility =>
            IsInDeveloperMode ? Visibility.Visible : Visibility.Collapsed;

    }
}
