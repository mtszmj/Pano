using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Views;

namespace Pano.Service
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private Page _pageKey;

        public void GoBack()
        {
        }

        public void NavigateTo(string pageKey)
        {
        }

        public void NavigateTo(string pageKey, object parameter)
        {
        }

        public string CurrentPageKey => _pageKey;
    }
}
