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
using Autofac;
using Autofac.Features.Indexed;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Pano.Model;
using Pano.Service;
using Pano.Service.Design;
using Pano.View.Controls;
using Pano.View.Pages;
using Pano.ViewModel.Controls;
using Pano.ViewModel.Pages;

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

        private static IContainer _container;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var builder = new ContainerBuilder();

            // Navigation service configuration
            NavigationService nav = new NavigationService();
            nav.Configure(ViewModelLocator.InitPageKey, typeof(InitPage));
            nav.Configure(ViewModelLocator.NewProjectKey, typeof(NewProjectPage));
            nav.Configure(ViewModelLocator.ProjectsKey, typeof(OpenProjectsPage));
            nav.Configure(ViewModelLocator.ProjectMainKey, typeof(InitPage)); //TODO zmienic na odpowiednia strone po dodaniu

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                builder.RegisterType<DesignProjectsService>().As<IProjectsService>().SingleInstance();
                builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
                builder.Register(c => nav).As<INavigationService>().SingleInstance();

                // Blendable objects
                builder.Register<ProjectViewModel>(c => ProjectViewModel.Factory.TestObject)
                    .Keyed<ProjectViewModel>(ProjectViewModelTestObjectKey);
            }
            else
            {
                // Create run time view services and models
                builder.RegisterType<DesignProjectsService>().As<IProjectsService>().SingleInstance();
                builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
                builder.Register(c => nav).As<INavigationService>().SingleInstance();
            }

            


            // View Models
            builder.RegisterType<NavigationViewModel>().SingleInstance();
            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<InitPageViewModel>();
            builder.RegisterType<ProjectsListViewModel>().SingleInstance();
            builder.RegisterType<ProjectDetailsViewModel>();
            builder.RegisterType<NewProjectPageViewModel>();
            builder.RegisterType<OpenProjectsPageViewModel>();
            builder.RegisterType<ProjectOpenDetailsViewModel>();
            builder.RegisterType<ProjectNewViewModel>();

            _container = builder.Build();
        }

        // Locator instance
        public static ViewModelLocator Locator => System.Windows.Application.Current.Resources["Locator"] as ViewModelLocator;

        // Main Window
        public MainViewModel Main => _container.Resolve<MainViewModel>();


        // Pages
        public InitPageViewModel InitPage => _container.Resolve<InitPageViewModel>();
        public NewProjectPageViewModel NewProjectPage => _container.Resolve<NewProjectPageViewModel>();
        public OpenProjectsPageViewModel OpenProjectsPage => _container.Resolve<OpenProjectsPageViewModel>();


        // Controls
        public NavigationViewModel Navigation => _container.Resolve<NavigationViewModel>();
        public ProjectsListViewModel ProjectsList => _container.Resolve<ProjectsListViewModel>();
        public ProjectDetailsViewModel ProjectDetails => _container.Resolve<ProjectDetailsViewModel>();
        public ProjectOpenDetailsViewModel ProjectOpenDetails => _container.Resolve<ProjectOpenDetailsViewModel>();
        public ProjectNewViewModel ProjectNew => _container.Resolve<ProjectNewViewModel>();

        // Design time data
        public ProjectViewModel ProjectTestObject => _container.Resolve<IIndex<string, ProjectViewModel>>()[ProjectViewModelTestObjectKey];


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}