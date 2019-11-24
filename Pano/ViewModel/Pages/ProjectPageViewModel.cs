using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Win32;
using Pano.Factories.Db;
using Pano.IO;
using Pano.Model;
using Pano.Model.Db.HotSpots;
using Pano.Model.Db.Scenes;
using Pano.Serialization.Model;
using Pano.Service;
using Pano.ViewModel.Controls;

namespace Pano.ViewModel.Pages
{
    public class ProjectPageViewModel : ViewModelBaseExtended
    {
        private readonly IDialogService _dialogService;
        private readonly ISelectorDialogService<Model.Db.Scenes.Scene> _selectorDialogService;
        private readonly IFileDialogService _fileDialogService;
        private readonly INavigationService _navigationService;
        private readonly IProjectsService _projectsService;
        private readonly ISceneFactory _sceneFactory;
        private readonly IHotSpotFactory _hotSpotFactory;
        private readonly ISerializationMapper _mapper;
        private readonly IStorage _storage;
        private ProjectViewModel _project;
        private readonly string[] AllowedExtensions = new[] { ".jpg", ".png" };

        public ProjectPageViewModel(IDialogService dialogService, 
                                    ISelectorDialogService<Model.Db.Scenes.Scene> selectorDialogService,
                                    IFileDialogService fileDialogService,
                                    INavigationService navigationService,
                                    IProjectsService projectsService,
                                    ISceneFactory sceneFactory,
                                    IHotSpotFactory hotSpotFactory,
                                    ISerializationMapper mapper,
                                    IStorage storage)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _selectorDialogService = selectorDialogService ?? throw new ArgumentNullException(nameof(selectorDialogService));
            _fileDialogService = fileDialogService ?? throw new ArgumentNullException(nameof(fileDialogService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _projectsService = projectsService ?? throw new ArgumentNullException(nameof(projectsService));
            _sceneFactory = sceneFactory ?? throw new ArgumentNullException(nameof(sceneFactory));
            _hotSpotFactory = hotSpotFactory ?? throw new ArgumentNullException(nameof(hotSpotFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));

            SaveCommand = new RelayCommand(SaveProject);
            ExportCommand = new RelayCommand(ExportProject, () => Project?.Model?.Tour != null);
            BackCommand = new RelayCommand(GoBack);
            AddSceneCommand = new RelayCommand(AddScene);
            DeleteSceneCommand = new RelayCommand(DeleteScene, () => SelectedScene != null);
            AddHotSpotCommand = new RelayCommand(AddHotSpot, () => SelectedScene != null);
            DeleteHotSpotCommand = new RelayCommand(DeleteHotSpot, () => SelectedHotSpot != null);
            ChangeImageCommand = new RelayCommand(ChangeImage);
            RotateClockwiseCommand = new RelayCommand(RotateClockwise, SelectedScene?.Image != null);
            RotateCounterclockwiseCommand = new RelayCommand(RotateCounterclockwise, SelectedScene?.Image != null);
            SelectTargetSceneCommand = new RelayCommand(SelectTargetScene, IsSceneHotSpot);
            HandleDragEnterCommand = new RelayCommand<DragEventArgs>(HandleDragEnter);
            HandleDropCommand = new RelayCommand<DragEventArgs>(HandleDrop);
            HandleDragLeaveCommand = new RelayCommand<DragEventArgs>(HandleDragLeave);
            HandleDragOverCommand = new RelayCommand<DragEventArgs>(HandleDragOver);


            Messenger.Default.Register<PropertyChangedMessage<HotSpot>>(
                this,
                ViewModelLocator.SelectedHotSpotChangedFromImageToken,
                message => SelectedHotSpot = message.NewValue
            );

            Messenger.Default.Register<PropertyChangedMessage<(Scene scene, int yaw, int pitch)>>(
                this,
                ViewModelLocator.AddHotSpotFromImageToken,
                message =>
                {
                    SelectedScene = message.NewValue.scene;
                    AddHotSpot(message.NewValue.yaw, message.NewValue.pitch);
                });

            Messenger.Default.Register<PropertyChangedMessage<HotSpot>>(
                this,
                ViewModelLocator.DeleteHotSpotFromImageToken,
                message =>
                {
                    SelectedHotSpot = message.NewValue;
                    DeleteHotSpot();
                }
            );
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

                SelectedSceneViewModel = new SceneImageViewModel(_selectedScene);
                //RaisePropertyChanged(nameof(SelectedSceneViewModel));
            }
        }

        private SceneImageViewModel _selectedSceneImageViewModel;

        public SceneImageViewModel SelectedSceneViewModel
        {
            get => _selectedSceneImageViewModel;
            set
            {
                _selectedSceneImageViewModel?.Cleanup();
                Set(ref _selectedSceneImageViewModel, value);
            }
        }

        private Model.Db.HotSpots.HotSpot _selectedHotSpot;

        public Model.Db.HotSpots.HotSpot SelectedHotSpot
        {
            get => _selectedHotSpot;
            set
            {

                var msg = new PropertyChangedMessage<HotSpot>(this, _selectedHotSpot, value, nameof(SelectedHotSpot));
                MessengerInstance.Send(msg, ViewModelLocator.SelectedHotSpotChangedFromListToken);

                Set(ref _selectedHotSpot, value);
                RaisePropertyChanged(nameof(IsSceneHotSpot));
                RaisePropertyChanged(nameof(SceneHotSpot));
                RaisePropertyChanged(nameof(HotSpotTargetSceneTitle));
            }
        }

        public bool IsSceneHotSpot => (SelectedHotSpot is Model.Db.HotSpots.SceneHotSpot);
        public Model.Db.HotSpots.SceneHotSpot SceneHotSpot => SelectedHotSpot as Model.Db.HotSpots.SceneHotSpot;
        public string HotSpotTargetSceneTitle => this.SceneHotSpot?.TargetScene?.Title;

        private bool _isRequiredFileIncludedInDragDrop;
        public bool IsRequiredFileIncludedInDragDrop
        {
            get => _isRequiredFileIncludedInDragDrop;
            set => Set(ref _isRequiredFileIncludedInDragDrop, value);
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand AddSceneCommand { get; set; }
        public RelayCommand DeleteSceneCommand { get; set; }
        public RelayCommand AddHotSpotCommand { get; set; }
        public RelayCommand DeleteHotSpotCommand { get; set; }
        public RelayCommand ChangeImageCommand { get; set; }
        public RelayCommand RotateClockwiseCommand { get; set; }
        public RelayCommand RotateCounterclockwiseCommand { get; set; }
        public RelayCommand SelectTargetSceneCommand { get; private set; }
        public RelayCommand<DragEventArgs> HandleDragEnterCommand { get; private set; }
        public RelayCommand<DragEventArgs> HandleDropCommand { get; private set; }
        public RelayCommand<DragEventArgs> HandleDragLeaveCommand { get; private set; }
        public RelayCommand<DragEventArgs> HandleDragOverCommand { get; private set; }

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

        private void SaveProject()
        {
            var result = _projectsService.Save(this.Project.Model);
            Task.Run(() => _dialogService.ShowMessageBox($"Zapisano {result}", "Zapis"));
        }

        private void ExportProject()
        {
            _fileDialogService.ClearFilters();
            _fileDialogService.AddFilter("Wszystkie pliki", "*");
            _fileDialogService.AddFilter("Json", "json");
            _fileDialogService.AddFilter("Txt", "txt");
            var path = _fileDialogService.SaveFileDialog();

            if (path)
            {
                SaveProject();

                var tour = _mapper.Map<TourForDb, Tour>(Project.Model.Tour);
                _storage.Path = path.Value;
                _storage.Save(tour);
            }

        }

        private void GoBack()
        {
            Cleanup();
            _navigationService.NavigateTo(ViewModelLocator.InitPageKey);
        }

        private void AddScene()
        {
            AddScene($"Scene {Scenes.Count + 1}");
        }

        private Scene AddScene(string title)
        {
            var scene = _sceneFactory.NewEquirectangularScene(title);
            _project.Model.Tour.AddScene(scene);
            SelectedScene = scene;
            return scene;
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

        private void AddHotSpot() => AddHotSpot(0, 0);
        private void AddHotSpot(int yaw, int pitch)
        {
            if(SelectedScene == null)
                throw new InvalidOperationException();

            var spot = _hotSpotFactory.NewSceneHotSpot(SelectedScene, null);
            spot.Text = $"HotSpot {SelectedScene.HotSpots.Count + 1}";
            spot.Yaw = yaw;
            spot.Pitch = pitch;
            SelectedScene.AddHotSpot(spot);
            SelectedHotSpot = spot;
            SelectTargetSceneWhenCreatingHotSpot();

            if (SelectedHotSpot is SceneHotSpot shs && shs.TargetScene != null)
            {
                shs.Text = shs.TargetScene.Title;
            }
        }

        private void DeleteHotSpot()
        {
            if(SelectedHotSpot != null)
            {
                _projectsService.RemoveHotSpot(SelectedHotSpot);
                SelectedScene.DeleteHotSpot(SelectedHotSpot);
            }
        }

        private void ChangeImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                var path = openFileDialog.FileName;
                SelectedScene.SetImage(path);
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

        private void SelectTargetScene()
        {
            var buttons = new List<string> { "Wyczyść", null, "OK", "Anuluj"};
            _selectorDialogService.ShowDialog(
                buttons, 
                _project.Model.Tour.Scenes.Except(new[] {SelectedScene}), 
                SelectIndex, 
                SelectedHotSpot.Text
                );
        }

        private void SelectTargetSceneWhenCreatingHotSpot()
        {
            var buttons = new List<string> { null, null, "OK", "Anuluj" };
            _selectorDialogService.ShowDialog(
                buttons,
                _project.Model.Tour.Scenes.Except(new[] { SelectedScene }),
                SelectIndex,
                SelectedHotSpot.Text
            );
        }

        private void SelectIndex(Model.Db.Scenes.Scene scene, int? buttonIndex)
        {
            if (!(SelectedHotSpot is Model.Db.HotSpots.SceneHotSpot spot))
                return;

            if(buttonIndex == 2 && scene != null)
            {
                spot.TargetScene = scene;
                spot.TargetSceneId = scene.SceneId;
            }

            if (buttonIndex == 0)
            {
                spot.TargetScene = null;
                spot.TargetSceneId = null;
            }

            RaisePropertyChanged(nameof(HotSpotTargetSceneTitle));
        }

        private void HandleDragEnter(DragEventArgs args)
        {
            var paths = (string[])args.Data.GetData(DataFormats.FileDrop);

            if (paths != null)
            {
                var files = EnumerateFilesWithCorrectExtension(paths);

                IsRequiredFileIncludedInDragDrop = files.Any();

                args.Handled = true;
                args.Effects = DragDropEffects.Copy;
            }
        }

        private void HandleDrop(DragEventArgs args)
        {
            var paths = (string[])args.Data.GetData(DataFormats.FileDrop);

            if (paths != null)
            {
                var files = EnumerateFilesWithCorrectExtension(paths);

                if (files.Any())
                {
                    foreach (var file in files)
                    {
                        var name = Path.GetFileNameWithoutExtension(file);
                        var scene = AddScene(name);
                        scene.SetImage(file);
                    }
                }
            }

            IsRequiredFileIncludedInDragDrop = false;
        }

        private void HandleDragLeave(DragEventArgs args)
        {
            IsRequiredFileIncludedInDragDrop = false;
        }

        private void HandleDragOver(DragEventArgs args)
        {
            args.Handled = true;
            args.Effects = IsRequiredFileIncludedInDragDrop ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private IEnumerable<string> EnumerateFilesWithCorrectExtension(string[] paths)
        {
            var directories = paths.Where(x => Directory.Exists(x));

            var filesFromDirectories = directories
                .SelectMany(x => Directory.EnumerateFiles(x, "*.*", SearchOption.AllDirectories))
                .Where(x => AllowedExtensions.Any(ext => x.ToLower().EndsWith(ext)));
            var files = paths
                .Where(x => AllowedExtensions.Any(ext => x.ToLower().EndsWith(ext)))
                .Concat(filesFromDirectories);
            return files;
        }
    }
}
