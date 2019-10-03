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

using System;
using System.Collections.Generic;
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
using Pano.ViewModel.Control;

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
        public const string InitPageKey = "InitPageKey";
        public const string ProjectsKey = "ProjectsKey";
        public const string PanoramaKey = "PanoramaKey";
        public const string NewProjectKey = "NewProjectKey";
        public const string ProjectMainKey = "ProjectMainKey";
        public const string ProjectViewModelTestObjectKey = "ProjectViewModelTestObjectKey";

        public readonly Dictionary<Type, string> PageTitle = new Dictionary<Type, string>()
        {
            [typeof(OpenProjectsPage)] = ProjectsKey,
        };

        public const string CurrentPageToken = "_current_page_token";
        public const string NavigationVisibleToken = "_navigation_visible_token";
        public const string ProjectToOpenToken = "_project_to_open_token";
        public const string ProjectToCreateToken = "_project_to_create_token";
        public const string CommandRequeryToken = "_command_requery_token";

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
                SimpleIoc.Default.Register<ProjectViewModel>(() => ProjectViewModel.Factory.TestObject, ProjectViewModelTestObjectKey);
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IProjectsService, DesignProjectsService>();
                SimpleIoc.Default.Register<IDialogService, DialogService>();


                NavigationService nav = new NavigationService();
                nav.Configure(ViewModelLocator.InitPageKey, typeof(InitPage));
                nav.Configure(ViewModelLocator.NewProjectKey, typeof(NewProjectPage));
                nav.Configure(ViewModelLocator.ProjectsKey, typeof(OpenProjectsPage));


                SimpleIoc.Default.Register<INavigationService>(() => nav);
            }

            SimpleIoc.Default.Register<NavigationViewModel>(true);
            SimpleIoc.Default.Register<MainViewModel>(true);
            var main = SimpleIoc.Default.GetInstance<MainViewModel>(); // Ensure VM

            SimpleIoc.Default.Register<InitPageViewModel>(true);
            SimpleIoc.Default.Register<ProjectsListViewModel>(false);
            SimpleIoc.Default.Register<ProjectDetailsViewModel>(true);
            SimpleIoc.Default.Register<NewProjectPageViewModel>();
            SimpleIoc.Default.Register<OpenProjectsPageViewModel>();
            SimpleIoc.Default.Register<ProjectOpenDetailsViewModel>(true);
            SimpleIoc.Default.Register<ProjectNewViewModel>(true);
        }

        // Locator instance
        public static ViewModelLocator Locator => System.Windows.Application.Current.Resources["Locator"] as ViewModelLocator;

        // Main Window
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();


        // Pages
        public InitPageViewModel InitPage => SimpleIoc.Default.GetInstance<InitPageViewModel>();
        public NewProjectPageViewModel NewProjectPage => SimpleIoc.Default.GetInstance<NewProjectPageViewModel>();
        public OpenProjectsPageViewModel OpenProjectsPage => SimpleIoc.Default.GetInstance<OpenProjectsPageViewModel>();


        // Controls
        public NavigationViewModel Navigation => SimpleIoc.Default.GetInstance<NavigationViewModel>();
        public ProjectsListViewModel ProjectsList => SimpleIoc.Default.GetInstance<ProjectsListViewModel>();
        public ProjectDetailsViewModel ProjectDetails => SimpleIoc.Default.GetInstance<ProjectDetailsViewModel>();
        public ProjectOpenDetailsViewModel ProjectOpenDetails => SimpleIoc.Default.GetInstance<ProjectOpenDetailsViewModel>();
        public ProjectNewViewModel ProjectNew => SimpleIoc.Default.GetInstance<ProjectNewViewModel>();


        // Design-time objects
        public ProjectViewModel ProjectTestObject => SimpleIoc.Default.GetInstance<ProjectViewModel>(ProjectViewModelTestObjectKey);


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}