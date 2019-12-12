using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private bool _propertyChangedIsBeingHandledMutex = false;
        private bool _messageReceivedMutex = false;
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
                    _messageReceivedMutex = true;
                    SelectedHotSpot = message.NewValue;
                });

            if (scene?.HotSpots != null)
                scene.HotSpots.CollectionChanged += (sender, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (HotSpot spot in args.NewItems)
                        {
                            spot.PropertyChanged += SpotOnPropertyChanged;
                        }
                    }
                    else if (args.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (HotSpot spot in args.OldItems)
                        {
                            spot.PropertyChanged -= SpotOnPropertyChanged;
                        }
                    }
                    Update();
                };
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

                if (!_messageReceivedMutex)
                {
                    MessengerInstance.Send(
                        new PropertyChangedMessage<HotSpot>(this, _selectedHotSpot, value, nameof(SelectedHotSpot)),
                        ViewModelLocator.SelectedHotSpotChangedFromImageToken);
                }
                Set(ref _selectedHotSpot, value);

                _messageReceivedMutex = false;
            }
        }

        public ObservableCollection<Circle> Drawings { get; set; } = new ObservableCollection<Circle>();
        public int ImageActualWidth { get; set; }
        public int ImageActualHeight { get; set; }
        public int RowActualWidth { get; set; }
        public int RowActualHeight { get; set; }
        public int MinX => (RowActualWidth - ImageActualWidth) / 2;//- (Circle.Diameter / 2);
        public int MaxX => ImageActualWidth + ((RowActualWidth - ImageActualWidth) / 2);//- (Circle.Diameter / 2);
        public int MinY => (RowActualHeight - ImageActualHeight) / 2;//- (Circle.Diameter / 2);
        public int MaxY => ImageActualHeight + ((RowActualHeight - ImageActualHeight) / 2);//- (Circle.Diameter / 2);
        public Circle SelectCircleByXAndY(int x, int y)
        {
            return Drawings
                .FirstOrDefault(circle =>
                    Math.Sqrt(
                        Math.Pow(x - circle.X, 2)
                        + Math.Pow(y - circle.Y, 2)
                    ) < Circle.Diameter
                );
        }

        public void Update()
        {
            if (_scene?.HotSpots != null)
            {
                foreach (var circle in Drawings)
                    circle.PropertyChanged -= CircleOnPropertyChanged;

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

        public void AddHotSpot(int x, int y)
        {
            CalculateXYToYawPitch(x - Circle.Radius, y - Circle.Radius, out int yaw, out int pitch);
            if (yaw < MinYaw || yaw > MaxYaw || pitch < MaxPitch || pitch > MinPitch)
                return;
            var tuple = (scene: SelectedScene, yaw: yaw, pitch: pitch);
            MessengerInstance.Send(
                new PropertyChangedMessage<(Scene scene, int yaw, int pitch)>(tuple, tuple, "AddHotSpot"),
                ViewModelLocator.AddHotSpotFromImageToken
            );
        }

        public void DeleteHotSpot(HotSpot spot)
        {
            if (spot != null)
                MessengerInstance.Send(
                    new PropertyChangedMessage<HotSpot>(spot, spot, "DeleteHotSpot"),
                    ViewModelLocator.DeleteHotSpotFromImageToken
                );
        }

        private void CircleOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Circle circle && _propertyChangedIsBeingHandledMutex == false)
            {
                try
                {
                    _propertyChangedIsBeingHandledMutex = true;
                    if (circle.HotSpot != null
                        && (e.PropertyName == nameof(Circle.X) || e.PropertyName == nameof(Circle.Y)))
                    {
                        CalculateXYToYawPitch(circle.X, circle.Y, out int yaw, out int pitch);
                        circle.HotSpot.Yaw = yaw;
                        circle.HotSpot.Pitch = pitch;
                    }
                }
                finally
                {
                    _propertyChangedIsBeingHandledMutex = false;
                }
            }
        }

        private void SpotOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is HotSpot spot
                && _propertyChangedIsBeingHandledMutex == false
                && (e.PropertyName == nameof(HotSpot.Yaw) || e.PropertyName == nameof(HotSpot.Pitch)))
            {
                try
                {
                    _propertyChangedIsBeingHandledMutex = true;
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
                    _propertyChangedIsBeingHandledMutex = false;
                }
            }
        }

        private void CalculateYawPitchToXY(HotSpot spot, out int x, out int y)
        {
            x = (int)(((spot.Yaw - MinYaw) * ImageActualWidth) / (1.0 * (MaxYaw - MinYaw))
                + ((RowActualWidth - ImageActualWidth) / 2.0) - Circle.Radius);
            y = (int)(((spot.Pitch - MinPitch) * ImageActualHeight) / (1.0 * (MaxPitch - MinPitch))
                + ((RowActualHeight - ImageActualHeight) / 2.0) - Circle.Radius);
        }

        private void CalculateXYToYawPitch(int x, int y, out int yaw, out int pitch)
        {
            yaw = (int)((x - ((RowActualWidth - ImageActualWidth) / 2.0) + (Circle.Radius))
                   * (MaxYaw - MinYaw) / (1.0 * ImageActualWidth) + MinYaw);

            pitch = (int)((y - ((RowActualHeight - ImageActualHeight) / 2.0) + (Circle.Radius))
                    * (MaxPitch - MinPitch) / (1.0 * ImageActualHeight) + MinPitch);
        }

        public class Circle : ViewModelBaseExtended
        {
            public const int Diameter = 16;
            public const int Radius = Diameter / 2;

            private int _x;
            private int _y;
            private bool _isSelected;
            private readonly WeakReference<HotSpot> _hotSpot = new WeakReference<HotSpot>(null);

            public int X
            {
                get => _x;
                set => Set(ref _x, value - Radius);
            }

            public int Y
            {
                get => _y;
                set => Set(ref _y, value - Radius);
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
            { _x = x, _y = y, _isSelected = false, HotSpot = spot };
        }
    }
}
