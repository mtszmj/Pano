using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Pano.ViewModel
{
    public abstract class ViewModelBaseDecorator : ViewModelBase
    {
#if DEBUG
        private static bool _isInDeveloperMode = true;
#else
        private static bool _isInDeveloperMode = false;
#endif

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged = delegate { };

        public virtual string VmName => this.GetType().Name;

        public static bool IsInDeveloperModeStatic
        {
            get => _isInDeveloperMode;
            set
            {
                if (_isInDeveloperMode == value)
                    return;

                _isInDeveloperMode = value;
                RaiseStaticPropertyChanged();
            }
        }

        protected static void RaiseStaticPropertyChanged([CallerMemberName] string propertyName = null)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
