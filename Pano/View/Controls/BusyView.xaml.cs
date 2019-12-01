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
using Pano.ViewModel.Controls;

namespace Pano.View.Controls
{
    /// <summary>
    /// Interaction logic for BusyView.xaml
    /// </summary>
    public partial class BusyView : UserControl
    {
        private BusyViewModel vm;

        public BusyView()
        {
            InitializeComponent();
            vm = DataContext as BusyViewModel;
        }

        private void BusyView_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (vm != null)
                vm.Name = this.Name;
        }
    }
}
