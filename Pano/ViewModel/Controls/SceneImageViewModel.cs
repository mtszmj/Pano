using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using Pano.Model.Db.HotSpots;
using Pano.Model.Db.Scenes;

namespace Pano.ViewModel.Controls
{
    public class SceneImageViewModel : ViewModelBaseExtended
    {

        public const int MinYaw = -180;
        public const int MaxYaw = 180;
        public const int MinPitch = 90;
        public const int MaxPitch = -90;

        private bool PropertyChangedIsBeingHandledMutex = false;
        private bool MessageReceivedMutex = false;
        private Scene _scene;
        private HotSpot _selectedHotSpot;

        public SceneImageViewModel(Scene scene)
        {
            SelectedScene = scene;
            Messenger.Default.Register<PropertyChangedMessage<HotSpot>>(
                this,
                ViewModelLocator.SelectedHotSpotChangedFromListToken,
                message =>
                {
                    MessageReceivedMutex = true;
                    SelectedHotSpot = message.NewValue;
                });
        }

        public Scene SelectedScene
        {
            get => _scene;
            set
            {
                if (_scene != null)
                {
                    foreach (var spot in _scene.HotSpots)
                    {
                        spot.PropertyChanged -= SpotOnPropertyChanged;
                    }
                }

                Set(ref _scene, value);

                if (_scene != null)
                {
                    foreach (var spot in _scene.HotSpots)
                    {
                        spot.PropertyChanged += SpotOnPropertyChanged;
                    }
                }
                
                Update();
                SelectedHotSpot = null;
            }
        }

        public HotSpot SelectedHotSpot
        {
            get => _selectedHotSpot;
            set
            {
                if (_selectedHotSpot == value)
                    return;

                var circle = Drawings.FirstOrDefault(x => x.HotSpot == _selectedHotSpot);
                if (circle != null)
                    circle.IsSelected = false;

                circle = Drawings.FirstOrDefault(x => x.HotSpot == value);
                if (circle != null)
                    circle.IsSelected = true;

                if(!MessageReceivedMutex)
                { 
                    MessengerInstance.Send(
                        new PropertyChangedMessage<HotSpot>(this, _selectedHotSpot, value, nameof(SelectedHotSpot)),
                        ViewModelLocator.SelectedHotSpotChangedFromImageToken);
                }
                Set(ref _selectedHotSpot, value);

                MessageReceivedMutex = false;
            }
        }

        public ObservableCollection<Circle> Drawings { get; set; } = new ObservableCollection<Circle>();

        public int ImageActualWidth { get; set; }
        public int ImageActualHeight { get; set; }
        public int RowActualWidth { get; set; }
        public int RowActualHeight { get; set; }
        public int MinX => (RowActualWidth - ImageActualWidth) / 2 - (Circle.DefaultRadius / 2);
        public int MaxX => ImageActualWidth + ((RowActualWidth - ImageActualWidth) / 2) - (Circle.DefaultRadius / 2);
        public int MinY => (RowActualHeight - ImageActualHeight) / 2 - (Circle.DefaultRadius / 2);
        public int MaxY => ImageActualHeight + ((RowActualHeight - ImageActualHeight) / 2) - (Circle.DefaultRadius / 2);


        public void Update()
        {
            if (_scene?.HotSpots != null)
            {
                Drawings.Clear();
                foreach (var spot in _scene.HotSpots)
                {
                    CalculateYawPitchToXY(spot, out int x, out int y);
                    var circle = Circle.Create(x, y, spot);
                    circle.PropertyChanged += CircleOnPropertyChanged;
                    Drawings.Add(circle);
                }
            }
        }

        private void CircleOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(sender is Circle circle && PropertyChangedIsBeingHandledMutex == false)
            {
                try
                {
                    PropertyChangedIsBeingHandledMutex = true;
                    if (circle.HotSpot != null
                        && (e.PropertyName == nameof(Circle.X) || e.PropertyName == nameof(Circle.Y)))
                    {
                        CalculateXYToYawPitch(circle, out int yaw, out int pitch);
                        circle.HotSpot.Yaw = yaw;
                        circle.HotSpot.Pitch = pitch;
                    }
                }
                finally
                {
                    PropertyChangedIsBeingHandledMutex = false;
                }
            }
        }



        private void SpotOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is HotSpot spot
                && PropertyChangedIsBeingHandledMutex == false
                && (e.PropertyName == nameof(HotSpot.Yaw) || e.PropertyName == nameof(HotSpot.Pitch)))
            {
                try
                {
                    PropertyChangedIsBeingHandledMutex = true;
                    var circle = Drawings.FirstOrDefault(x => x.HotSpot == spot);
                    if (circle != null)
                    {
                        CalculateYawPitchToXY(spot, out int x, out int y);
                        circle.X = x;
                        circle.Y = y;
                    }
                }
                finally
                {
                    PropertyChangedIsBeingHandledMutex = false;
                }

            }
        }

        private void CalculateYawPitchToXY(HotSpot spot, out int x, out int y)
        {
            x = (int) ((spot.Yaw - MinYaw) * ImageActualWidth) / (MaxYaw - MinYaw) + ((RowActualWidth - ImageActualWidth) / 2) - (Circle.DefaultRadius / 2);
            y = (int) ((spot.Pitch - MinPitch) * ImageActualHeight) / (MaxPitch - MinPitch) + ((RowActualHeight - ImageActualHeight) / 2) - (Circle.DefaultRadius / 2);
        }

        private void CalculateXYToYawPitch(Circle circle, out int yaw, out int pitch)
        {
            yaw = (circle.X - ((RowActualWidth - ImageActualWidth) / 2) + (Circle.DefaultRadius / 2)) *
                  (MaxYaw - MinYaw) / ImageActualWidth + MinYaw;

            pitch = (circle.Y - ((RowActualHeight - ImageActualHeight) / 2) + (Circle.DefaultRadius / 2)) *
                    (MaxPitch - MinPitch) / ImageActualHeight + MinPitch;
        }


        public class Circle : ViewModelBaseExtended
        {
            public const int DefaultRadius = 15;

            private int _x;
            private int _y;
            private bool _isSelected;
            private WeakReference<HotSpot> _hotSpot = new WeakReference<HotSpot>(null);
            
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
                get => DefaultRadius;
            }

            public bool IsSelected
            {
                get => _isSelected;
                set => Set(ref _isSelected, value);
            }

            public HotSpot HotSpot
            {
                get
                {
                    _hotSpot.TryGetTarget(out var spot);
                    return spot;
                }
                set
                {
                    _hotSpot.SetTarget(value);
                    RaisePropertyChanged();
                }
            }

            public static Circle Create(int x, int y, HotSpot spot) => new Circle()
                {_x = x, _y = y, _isSelected = false, HotSpot = spot};
        }
    }
}
