using System;
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
using Pano.ViewModel;

namespace Pano.View
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Page1Button_Click(object sender, RoutedEventArgs e)
        {
            var nav = SimpleIoc.Default.GetInstance<INavigationService>();
            nav.NavigateTo(ViewModelLocator.Page1Key);
        }

        private void Page2Button_Click(object sender, RoutedEventArgs e)
        {
            var nav = SimpleIoc.Default.GetInstance<INavigationService>();
            nav.NavigateTo(ViewModelLocator.Page2Key);
        }

        private void Page3Button_Click(object sender, RoutedEventArgs e)
        {
            var nav = SimpleIoc.Default.GetInstance<INavigationService>();
            nav.NavigateTo(ViewModelLocator.Page3Key);
        }
    }
}
