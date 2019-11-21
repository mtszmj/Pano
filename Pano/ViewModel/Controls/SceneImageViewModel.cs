using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Pano.Model.Db.Scenes;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;

namespace Pano.ViewModel.Controls
{
    public class SceneImageViewModel : ViewModelBaseExtended
    {
        public SceneImageViewModel(Scene scene)
        {
            SelectedScene = scene;
            Drawings.Add(
                new Circle()
                {
                    X = 10,
                    Y = 10,
                    Radius = 10
                });

            Drawings.Add(new Circle
            {
                X = 100,
                Y= 50,
                Radius = 20
            });
            Drawings.Add(new Circle
            {
                X = 75,
                Y = 24,
                Radius = 15
            });
        }

        private Scene _scene;
        public Scene SelectedScene
        {
            get => _scene;
            set
            {
                Set(ref _scene, value);
            }
        }

        private string _test = "TESTTTT";
        public string Test
        {
            get => _test; set => Set(ref _test, value); } 
        public ObservableCollection<Circle> Drawings { get; set; } = new ObservableCollection<Circle>();

        public class Circle : ViewModelBaseExtended
        {
            private int _x;
            private int _y;
            private int _radius;
            private bool _isSelected;

            public int X
            {
                get => _x;
                set => Set(ref _x, value);
            }

            public int Y
            {
                get => _y;
                set => Set(ref _y, value);
            }

            public int Radius
            {
                get => _radius;
                set => Set(ref _radius, value);
            }

            public bool IsSelected
            {
                get => _isSelected;
                set => Set(ref _isSelected, value);
            }
        }
    }
}
