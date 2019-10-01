using Pano.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.ViewModel;

namespace Pano.ViewModels.HotSpots
{
    public class SceneHotSpotViewModel : ViewModelBaseDecorator
    {
        SceneHotSpot Model { get; set; }

        ObservableCollection<string> Scenes { get; set; } = new ObservableCollection<string>() { "test1", "test2" };

        public SceneHotSpotViewModel(SceneHotSpot model)
        {
            Model = model;
        }
    }
}
