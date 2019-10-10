using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Views;

namespace Pano.Service
{
    public class NavigationService : INavigationService, IInitializeService<Frame>
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private string _previousPageKey;

        public Frame CurrentFrame { get; set; }

        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (CurrentFrame?.Content == null)
                    {
                        return null;
                    }

                    var pageType = CurrentFrame.Content.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        public void GoBack()
        {
            if (CanGoBack)
            { 
                NavigateTo(_previousPageKey);
            }
        }

        public bool CanGoBack => _previousPageKey != null && _previousPageKey != CurrentPageKey;

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                
                if (!_pagesByKey.TryGetValue(pageKey, out var type))
                {
                    throw new ArgumentException($"No such page: {pageKey}. Configure pages before navigating.", nameof(pageKey));
                }

                ConstructorInfo constructor = null;
                object[] parameters = null;

                if (parameter == null)
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(c => !c.GetParameters().Any());

                    parameters = new object[] {};
                }
                else
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(
                            c =>
                            {
                                var p = c.GetParameters();
                                return p.Length == 1
                                       && p[0].ParameterType == parameter.GetType();
                            });

                    parameters = new[] { parameter };
                }

                if (constructor == null)
                {
                    throw new InvalidOperationException($"No suitable constructor found for page {pageKey}");
                }


                _previousPageKey = CurrentPageKey;
                var page = constructor.Invoke(parameters) as Page;
                CurrentFrame.Navigate(page);
            }
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public void Initialize(Frame frame)
        {
            CurrentFrame = frame;
        }

        public void InitializeService(Frame value)
        {
            Initialize(value);
        }
    }
}
