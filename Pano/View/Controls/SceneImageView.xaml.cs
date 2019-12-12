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
using Pano.Migrations;
using Pano.ViewModel.Controls;

namespace Pano.View.Controls
{
    /// <summary>
    /// Interaction logic for SceneImageView.xaml
    /// </summary>
    public partial class SceneImageView : UserControl
    {
        Point? dragStart = null;
        private SceneImageViewModel.Circle SelectedCircle { get; set; }
        public SceneImageView()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)sender;
            dragStart = e.GetPosition(element);
            element.CaptureMouse();

            if(DataContext is SceneImageViewModel vm)
            {
                SelectedCircle = vm.SelectCircleByXAndY((int)dragStart.Value.X, (int)dragStart.Value.Y);
                if(SelectedCircle != null)
                { 
                    Mouse.OverrideCursor = Cursors.Hand;
                    vm.SelectedHotSpot = SelectedCircle?.HotSpot;
                }
            }
        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)sender;
            dragStart = null;
            SelectedCircle = null;
            Mouse.OverrideCursor = null;
            element.ReleaseMouseCapture();
        }

        private void UIElement_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            var element = (UIElement)sender;
            var position = e.GetPosition(element);
            if(SelectedCircle != null && DataContext is SceneImageViewModel vm)
            {
                var posX = (int) position.X;
                var posY = (int) position.Y;

                if (posX > vm.MinX && posX < vm.MaxX)
                    SelectedCircle.X = (int) position.X;

                if (posY > vm.MinY && posY < vm.MaxY)
                    SelectedCircle.Y = (int) position.Y;
            }
        }

        private void SceneImageView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is SceneImageViewModel vm)
            {
                vm.ImageActualHeight = (int) ImageControl.ActualHeight;
                vm.ImageActualWidth = (int) ImageControl.ActualWidth;
                vm.RowActualHeight = (int) SpotsItemsControl.ActualHeight;
                vm.RowActualWidth = (int) SpotsItemsControl.ActualWidth;
                vm.Update();
            } 
        }

        private void UIElement_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = null;
            if (!(DataContext is SceneImageViewModel vm))
                return;

            vm.ImageActualHeight = (int)ImageControl.ActualHeight;
            vm.ImageActualWidth = (int)ImageControl.ActualWidth;
            vm.RowActualHeight = (int)SpotsItemsControl.ActualHeight;
            vm.RowActualWidth = (int)SpotsItemsControl.ActualWidth;

            if (e.ChangedButton == MouseButton.Left)
            {
                var element = (UIElement)sender;
                var position = e.GetPosition(element);
                vm.AddHotSpot((int)position.X, (int)position.Y);
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                var element = (UIElement)sender;
                var position = e.GetPosition(element);
                var selectedCircle = vm?.SelectCircleByXAndY((int) position.X, (int) position.Y);
                vm.DeleteHotSpot(selectedCircle?.HotSpot);
            }

        }

        private void SceneImageView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is SceneImageViewModel vm)
            {
                vm.ImageActualHeight = (int) ImageControl.ActualHeight;
                vm.ImageActualWidth = (int) ImageControl.ActualWidth;
                vm.RowActualHeight = (int) SpotsItemsControl.ActualHeight;
                vm.RowActualWidth = (int) SpotsItemsControl.ActualWidth;
                vm.Update();
            }
        }
    }
}
