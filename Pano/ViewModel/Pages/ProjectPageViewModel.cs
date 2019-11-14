using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Win32;
using Pano.Factories.Db;
using Pano.Model;
using Pano.Service;

namespace Pano.ViewModel.Pages
{
    public class ProjectPageViewModel : ViewModelBaseExtended
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IProjectsService _projectsService;
        private readonly ISceneFactory _sceneFactory;
        private readonly IHotSpotFactory _hotSpotFactory;
        private ProjectViewModel _project;

        public ProjectPageViewModel(IDialogService dialogService, 
                                    INavigationService navigationService,
                                    IProjectsService projectsService,
                                    ISceneFactory sceneFactory,
                                    IHotSpotFactory hotSpotFactory)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _projectsService = projectsService ?? throw new ArgumentNullException(nameof(projectsService));
            _sceneFactory = sceneFactory ?? throw new ArgumentNullException(nameof(sceneFactory));
            _hotSpotFactory = hotSpotFactory ?? throw new ArgumentNullException(nameof(hotSpotFactory)); 

            SaveCommand = new RelayCommand(SaveProject);
            BackCommand = new RelayCommand(() => _navigationService.NavigateTo(ViewModelLocator.InitPageKey));
            AddSceneCommand = new RelayCommand(AddScene);
            DeleteSceneCommand = new RelayCommand(DeleteScene, () => SelectedScene != null);
            AddHotSpotCommand = new RelayCommand(AddHotSpot, () => SelectedScene != null);
            DeleteHotSpotCommand = new RelayCommand(DeleteHotSpot, () => SelectedHotSpot != null);
            ChangeImageCommand = new RelayCommand(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
                if (openFileDialog.ShowDialog() == true)
                {
                    var path = openFileDialog.FileName;
                    SelectedScene.SetImage(path);
                }
            });
            RotateClockwiseCommand = new RelayCommand(RotateClockwise, SelectedScene?.Image != null);
            RotateCounterclockwiseCommand = new RelayCommand(RotateCounterclockwise, SelectedScene?.Image != null);
        }

        public ProjectViewModel Project
        {
            get => _project;
            set
            {
                if (_project == value)
                    return;

                var oldValue = _project;
                _project = value;
                RaisePropertyChanged("");
            }
        }

        public ObservableCollection<Model.Db.Scenes.Scene> Scenes => Project?.Model?.Tour?.Scenes;

        private Model.Db.Scenes.Scene _selectedScene;
        public Model.Db.Scenes.Scene SelectedScene
        {
            get => _selectedScene;
            set
            {
                if (value == _selectedScene)
                    return;

                _selectedScene = value;
                RaisePropertyChanged();
            }
        }

        private Model.Db.HotSpots.HotSpot _selectedHotSpot;
        public Model.Db.HotSpots.HotSpot SelectedHotSpot
        {
            get => _selectedHotSpot;
            set => Set(ref _selectedHotSpot, value);
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand AddSceneCommand { get; set; }
        public RelayCommand DeleteSceneCommand { get; set; }
        public RelayCommand AddHotSpotCommand { get; set; }
        public RelayCommand DeleteHotSpotCommand { get; set; }
        public RelayCommand ChangeImageCommand { get; set; }
        public RelayCommand RotateClockwiseCommand { get; set; }
        public RelayCommand RotateCounterclockwiseCommand { get; set; }

        private void SaveProject()
        {
            var result = _projectsService.Save(this.Project.Model);
            Task.Run(() => _dialogService.ShowMessageBox($"Zapisano {result}", "Zapis"));
        }

        private void AddScene()
        {
            var scene = _sceneFactory.NewEquirectangularScene($"Scene {Scenes.Count + 1}");
            _project.Model.Tour.AddScene(scene);
        }

        private void DeleteScene()
        {
            if (SelectedScene != null)
            {
                var scene = SelectedScene;
                _projectsService.RemoveScene(SelectedScene);
                _project.Model.Tour.DeleteScene(scene);
            }
        }

        private void AddHotSpot()
        {
            if(SelectedScene == null)
                throw new InvalidOperationException();

            var spot = _hotSpotFactory.NewSceneHotSpot(SelectedScene, SelectedScene); // TODO wybor targetScene
            SelectedScene.AddHotSpot(spot);
        }

        private void DeleteHotSpot()
        {
            if(SelectedHotSpot != null)
            {
                _projectsService.RemoveHotSpot(SelectedHotSpot);
                SelectedScene.DeleteHotSpot(SelectedHotSpot);
            }
        }

        private void RotateClockwise()
        {
            SelectedScene.Image.RotateImageClockwise();
            SelectedScene.RaisePropertyChanged(nameof(BitmapImage));
        }

        private void RotateCounterclockwise()
        {
            SelectedScene.Image.RotateImageCounterclockwise();
            SelectedScene.RaisePropertyChanged(nameof(BitmapImage));
        }
        public override void InitializeView()
        {
            MessengerInstance.Send(
                new PropertyChangedMessage<bool>(
                    this,
                    false,
                    true,
                    ViewModelLocator.NavigationVisibleToken
                    ),
                ViewModelLocator.NavigationVisibleToken
            );
        }
    }
}
