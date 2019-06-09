using System;
using System.Collections.Generic;
using System.IO;
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

namespace Pano
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private DragDropEffects EffectOk { get; } = DragDropEffects.Copy;
        private DragDropEffects EffectNok { get; } = DragDropEffects.None;
        private bool IsRequiredFileIncludedInDragDrop { get; set; }



        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            MainGrid.Background = Brushes.Aquamarine;

            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var drop = (string[])e.Data.GetData(DataFormats.FileDrop);

                var directories = drop.Where(x => Directory.Exists(x));
                var filesFromDirectories = directories.SelectMany(x => Directory.GetFiles(x, "*.jpg", SearchOption.AllDirectories));

                var files = drop.Where(x => x.ToLower().EndsWith(".jpg")).Concat(filesFromDirectories);



                if(files.Any())
                {
                    e.Handled = true;
                    e.Effects = EffectOk;
                    IsRequiredFileIncludedInDragDrop = true;

                    foreach (var file in files)
                    {
                        Console.Out.WriteLine(file);
                    }
                }
                else
                {
                    e.Handled = true;
                    e.Effects = EffectNok;
                    IsRequiredFileIncludedInDragDrop = false;
                }
            }
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            if (IsRequiredFileIncludedInDragDrop)
            {
                e.Effects = EffectOk;
            }
            else
            {
                e.Effects = EffectNok;
            }
            
            this.MainGrid.Background = Brushes.Red;
        }

        private void Grid_DragLeave(object sender, DragEventArgs e)
        {
            IsRequiredFileIncludedInDragDrop = false;

            this.MainGrid.Background = Brushes.Gray;
        }

        private void MainGrid_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;

            if (IsRequiredFileIncludedInDragDrop)
            {
                e.Effects = EffectOk;
            }
            else
            {
                e.Effects = EffectNok;
            }
        }
    }
}
