using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;

namespace Pano.ViewModel
{
    public abstract class ViewModelBaseDecorator : ViewModelBase
    {
        private static bool _isInDeveloperMode;
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged = delegate { };

        static ViewModelBaseDecorator()
        {
#if DEBUG
        _isInDeveloperMode = true;
#else
        _isInDeveloperMode = false;
#endif
        }

    public static bool IsInDeveloperModeStatic
        {
            get => _isInDeveloperMode;
            set
            {
                if (_isInDeveloperMode == value)
                    return;

                _isInDeveloperMode = value;
                RaiseStaticPropertyChanged();
                RaiseStaticPropertyChanged(nameof(IsInDeveloperModeVisibility));
            }
        }

        public static Visibility IsInDeveloperModeVisibility =>
            IsInDeveloperModeStatic ? Visibility.Visible : Visibility.Collapsed;

        public virtual string VmName => this.GetType().Name;

        protected static void RaiseStaticPropertyChanged([CallerMemberName] string propertyName = null)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void InitializeView() { }
    }
}
