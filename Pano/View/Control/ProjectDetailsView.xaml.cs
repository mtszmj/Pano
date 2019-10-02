﻿    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
    using Pano.ViewModel;

    namespace Pano.View
{
    /// <summary>
    /// Interaction logic for ProjectDetailsView.xaml
    /// </summary>
    public partial class ProjectDetailsView : UserControl
    {
        public ProjectDetailsView()
        {
            InitializeComponent();
        }

        private void ProjectDetailsView_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProjectDetailsViewModel vm)
            {
                vm.SelectedProject = null;
            }
        }
    }
}
