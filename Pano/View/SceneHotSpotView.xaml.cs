using Pano.ViewModels.HotSpots;
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

namespace Pano.View.Controls
{
    /// <summary>
    /// Interaction logic for SceneHotSpotView.xaml
    /// </summary>
    public partial class SceneHotSpotView : UserControl
    {
        private SceneHotSpotViewModel _ViewModel;
        public SceneHotSpotViewModel ViewModel
        {
            get => _ViewModel;
            set
            {
                DataContext = _ViewModel = value;
            }
        }

        public SceneHotSpotView()
        {
            InitializeComponent();
        }

        public SceneHotSpotView(SceneHotSpotViewModel viewModel) : this()
        {
            ViewModel = viewModel;
        }
    }
}
