/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Pano"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Pano.Model;
using Pano.Service;
using Pano.Service.Design;
using Pano.View;

namespace Pano.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public const string Page1Key = "Page1Key";
        public const string Page2Key = "Page2Key";
        public const string Page3Key = "Page3Key";

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                SimpleIoc.Default.Register<IProjectsService, DesignProjectsService>();
                if (SimpleIoc.Default.IsRegistered<IDialogService>())
                {
                    SimpleIoc.Default.Unregister<IDialogService>();
                }
                SimpleIoc.Default.Register<IDialogService, DesignDialogService>();
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IProjectsService, DesignProjectsService>();
                SimpleIoc.Default.Register<IDialogService, DialogService>();


                NavigationService nav = new NavigationService();
                nav.Configure(ViewModelLocator.Page1Key, typeof(Page1));
                nav.Configure(ViewModelLocator.Page2Key, typeof(Page2));
                nav.Configure(ViewModelLocator.Page3Key, typeof(Page3));


                SimpleIoc.Default.Register<INavigationService>(() => nav);
            }

            SimpleIoc.Default.Register<MainViewModel>();
            var main = SimpleIoc.Default.GetInstance<MainViewModel>(); // Ensure VM

            SimpleIoc.Default.Register<ProjectsListViewModel>();
            SimpleIoc.Default.GetInstance<ProjectsListViewModel>();

            SimpleIoc.Default.Register<ProjectDetailsViewModel>();
            SimpleIoc.Default.GetInstance<ProjectDetailsViewModel>();

            SimpleIoc.Default.Register<Page1ViewModel>(() => new Page1ViewModel(Page1Key), Page1Key);
            SimpleIoc.Default.Register<Page1ViewModel>(() => new Page1ViewModel(Page2Key), Page2Key);
            SimpleIoc.Default.Register<Page1ViewModel>(() => new Page1ViewModel(Page3Key), Page3Key);



        }

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public ProjectsListViewModel ProjectsList => SimpleIoc.Default.GetInstance<ProjectsListViewModel>();
        public ProjectDetailsViewModel ProjectDetails => SimpleIoc.Default.GetInstance<ProjectDetailsViewModel>();
        public Page1ViewModel Page1 => SimpleIoc.Default.GetInstance<Page1ViewModel>(Page1Key);
        public Page1ViewModel Page2 => SimpleIoc.Default.GetInstance<Page1ViewModel>(Page2Key);
        public Page1ViewModel Page3 => SimpleIoc.Default.GetInstance<Page1ViewModel>(Page3Key);


        public static ViewModelLocator Locator => System.Windows.Application.Current.Resources["Locator"] as ViewModelLocator;

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}