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
using Pano.ViewModel;

namespace Pano.View
{
    /// <summary>
    /// Interaction logic for NewProjectPage.xaml
    /// </summary>
    public partial class NewProjectPage : Page
    {
        public NewProjectPage()
        {
            InitializeComponent();
        }

        public NewProjectPage(ProjectViewModel newProject) : this()
        {
            if(DataContext is NewProjectViewModel vm)
                vm.SelectedProject = newProject;
        }
    }
}
