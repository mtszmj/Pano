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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Pano.Model;
using Pano.Service;
using Pano.Service.Design;

namespace Pano.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
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
            }

            SimpleIoc.Default.Register<MainViewModel>();
            var main = SimpleIoc.Default.GetInstance<MainViewModel>(); // Ensure VM

            SimpleIoc.Default.Register<ProjectsListViewModel>();
            SimpleIoc.Default.GetInstance<ProjectsListViewModel>();

            SimpleIoc.Default.Register<ProjectDetailsViewModel>();
            SimpleIoc.Default.GetInstance<ProjectDetailsViewModel>();
        }

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public ProjectsListViewModel ProjectsList => SimpleIoc.Default.GetInstance<ProjectsListViewModel>();
        public ProjectDetailsViewModel ProjectDetails => SimpleIoc.Default.GetInstance<ProjectDetailsViewModel>();

        public static ViewModelLocator Locator => System.Windows.Application.Current.Resources["Locator"] as ViewModelLocator;

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}