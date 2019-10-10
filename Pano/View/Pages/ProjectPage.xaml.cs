﻿using System;
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
using Pano.Model;
using Pano.ViewModel;
using Pano.ViewModel.Pages;

namespace Pano.View.Pages
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();
        }

        public ProjectPage(ProjectViewModel project) : this()
        {
            if (DataContext is ProjectPageViewModel vm)
            {
                vm.Project = project;
            }
        }
    }
}