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
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Pano.Service;
using Pano.ViewModel;

namespace Pano
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();

            var frame = GlobalMainFrame;
            var nav = SimpleIoc.Default.GetInstance<INavigationService>();
            var navinit = nav as IInitializeService<Frame>;
            navinit?.InitializeService(frame);
            nav.NavigateTo(ViewModelLocator.Page1Key);
        }

        //Disable keyboard shortcuts
        private void GlobalMainFrame_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back
                || e.NavigationMode == NavigationMode.Forward)
            {
                e.Cancel = true;
            }
        }
    }
}
