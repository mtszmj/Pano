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
using Autofac;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Win32;
using Pano.Factories.Db;
using Pano.Helpers;
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
        private readonly string[] AllowedExtensions = new[] { ".jpg", ".png" };
        private readonly ISelectorDialogService<Scene> _selectorDialogService;
        private readonly IFileDialogService _fileDialogService;
        private readonly INavigationService _navigationService;
        private readonly IProjectsService _projectsService;
        private readonly ISceneFactory _sceneFactory;
        private readonly IHotSpotFactory _hotSpotFactory;
        private readonly ISerializationMapper _mapper;
        private readonly IStorage _storage;
        private readonly IImageStorage _imageStorage;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IBusyIndicatorService _busyIndicatorService;
        private ProjectViewModel _project;
        private Scene _selectedScene;
        private SceneImageViewModel _selectedSceneImageViewModel;
        private HotSpot _selectedHotSpot;
        private bool _isRequiredFileIncludedInDragDrop;
        private bool _isBusy;
        private string _isBusyText;
        private bool _hasFinished;
        private string _hasFinishedText;

        public ProjectPageViewModel(ISelectorDialogService<Scene> selectorDialogService,
                                    IFileDialogService fileDialogService,
                                    INavigationService navigationService,
                                    IProjectsService projectsService,
                                    ISceneFactory sceneFactory,
                                    IHotSpotFactory hotSpotFactory,
                                    ISerializationMapper mapper,
                                    IStorage storage,
                                    IImageStorage imageStorage,
                                    Func<string, IBusyIndicatorService> busyIndicatorService,
                                    ILifetimeScope lifetimeScope)
        {
            _selectorDialogService = selectorDialogService ?? throw new ArgumentNullException(nameof(selectorDialogService));
            _fileDialogService = fileDialogService ?? throw new ArgumentNullException(nameof(fileDialogService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _projectsService = projectsService ?? throw new ArgumentNullException(nameof(projectsService));
            _sceneFactory = sceneFactory ?? throw new ArgumentNullException(nameof(sceneFactory));
            _hotSpotFactory = hotSpotFactory ?? throw new ArgumentNullException(nameof(hotSpotFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _imageStorage = imageStorage ?? throw new ArgumentNullException(nameof(imageStorage));
            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
            _busyIndicatorService = busyIndicatorService("ProjectPageViewBusy");

            SaveCommand = new RelayCommand(() => SaveProject());
            ExportCommand = new RelayCommand(() => ExportProject(), () => Project?.Model?.Tour != null);
            BackCommand = new RelayCommand(() => GoBack());
            AddSceneCommand = new RelayCommand(() => AddScene());
            DeleteSceneCommand = new RelayCommand(() => DeleteScene(), () => SelectedScene != null);
            AddHotSpotCommand = new RelayCommand(() => AddHotSpot(), () => SelectedScene != null);
            DeleteHotSpotCommand = new RelayCommand(() => DeleteHotSpot(), () => SelectedHotSpot != null);
            ChangeImageCommand = new RelayCommand(() => ChangeImage());
            RotateClockwiseCommand = new RelayCommand(() => RotateClockwise(), SelectedScene?.Image != null);
            RotateCounterclockwiseCommand = new RelayCommand(() => RotateCounterclockwise(), SelectedScene?.Image != null);
            SelectTargetSceneCommand = new RelayCommand(() => SelectTargetScene(), IsSceneHotSpot);
            HandleDragEnterCommand = new RelayCommand<DragEventArgs>(e => HandleDragEnter(e));
            HandleDropCommand = new RelayCommand<DragEventArgs>(e => HandleDrop(e));
            HandleDragLeaveCommand = new RelayCommand<DragEventArgs>(e => HandleDragLeave(e));
            HandleDragOverCommand = new RelayCommand<DragEventArgs>(e => HandleDragOver(e));
            SetSceneAsFirstSceneCommand = new RelayCommand<Scene>(s => Project.Model.SetSceneAsFirstScene(s));

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
            }
        }

        public SceneImageViewModel SelectedSceneViewModel
        {
            get => _selectedSceneImageViewModel;
            set
            {
                _selectedSceneImageViewModel?.Cleanup();
                Set(ref _selectedSceneImageViewModel, value);
            }
        }

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

        public bool IsSceneHotSpot => (SelectedHotSpot is SceneHotSpot);
        public SceneHotSpot SceneHotSpot => SelectedHotSpot as SceneHotSpot;
        public string HotSpotTargetSceneTitle => this.SceneHotSpot?.TargetScene?.Title;

        public bool IsRequiredFileIncludedInDragDrop
        {
            get => _isRequiredFileIncludedInDragDrop;
            set => Set(ref _isRequiredFileIncludedInDragDrop, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public string IsBusyText
        {
            get => _isBusyText;
            set => Set(ref _isBusyText, value);
        }

        public bool HasFinished
        {
            get => _hasFinished;
            set => Set(ref _hasFinished, value);
        }

        public string HasFinishedText
        {
            get => _hasFinishedText;
            set => Set(ref _hasFinishedText, value);
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand BackCommand { get; }
        public RelayCommand AddSceneCommand { get; }
        public RelayCommand DeleteSceneCommand { get; }
        public RelayCommand AddHotSpotCommand { get; }
        public RelayCommand DeleteHotSpotCommand { get; }
        public RelayCommand ChangeImageCommand { get; }
        public RelayCommand RotateClockwiseCommand { get; }
        public RelayCommand RotateCounterclockwiseCommand { get; }
        public RelayCommand SelectTargetSceneCommand { get; }
        public RelayCommand<DragEventArgs> HandleDragEnterCommand { get; }
        public RelayCommand<DragEventArgs> HandleDropCommand { get; }
        public RelayCommand<DragEventArgs> HandleDragLeaveCommand { get; }
        public RelayCommand<DragEventArgs> HandleDragOverCommand { get; }
        public RelayCommand<Scene> SetSceneAsFirstSceneCommand { get; }

        public override void InitializeView()
        {
        }

        private async Task SaveProject()
        {
            _busyIndicatorService.SetBusy("Trwa zapis...");
            var result = await Task.Run(async () => _projectsService.Save(this.Project.Model));
            RaisePropertyChanged("");
            _busyIndicatorService.ResetBusy();
            _busyIndicatorService.SetSnackbar($"Projekt zapisany ({result} zmian)");
        }

        private async Task ExportProject()
        {
            _fileDialogService.ClearFilters();
            _fileDialogService.AddFilter("Json", "json");
            _fileDialogService.AddFilter("Txt", "txt");
            _fileDialogService.AddFilter("Wszystkie pliki", "*");
            var path = _fileDialogService.SaveDirectoryDialog();

            if (path)
            {
                await SaveProject();
                _busyIndicatorService.SetBusy("Eksportuję...");
                await Task.Run(() =>
                    {
                        var tour = _mapper.Map<TourForDb, Tour>(Project.Model.Tour);
                        _storage.Path = $"{path.Value}\\pano.json";
                        _storage.Save(tour);

                        var imageDatas = _projectsService.GetImageDatas(Project.Model);
                        _imageStorage.Save(path.Value, imageDatas);
                    }
                );
                _busyIndicatorService.ResetBusy();
                _busyIndicatorService.SetSnackbar("Eksport zakończony.");
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
            if (SelectedScene == null)
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
            if (SelectedHotSpot != null)
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

        private async void RotateClockwise()
        {
            _busyIndicatorService.SetBusy("Obracam...");
            await Task.Run(() =>
                SelectedScene.Image.RotateImageClockwise()
            );
            SelectedScene.RaisePropertyChanged(nameof(BitmapImage));
            _busyIndicatorService.ResetBusy();

        }

        private async void RotateCounterclockwise()
        {
            _busyIndicatorService.SetBusy("Obracam...");
            await Task.Run(() => SelectedScene.Image.RotateImageCounterclockwise());
            SelectedScene.RaisePropertyChanged(nameof(BitmapImage));
            _busyIndicatorService.ResetBusy();
        }

        private void SelectTargetScene()
        {
            var buttons = new List<string> { "Wyczyść", null, "OK", "Anuluj" };
            _selectorDialogService.ShowDialog(
                buttons,
                _project.Model.Tour.Scenes.Except(new[] { SelectedScene }),
                SelectIndex,
                SelectedHotSpot.Text,
                "Wybierz docelową scenę:"
                );
        }

        private void SelectTargetSceneWhenCreatingHotSpot()
        {
            var buttons = new List<string> { null, null, "OK", "Anuluj" };
            _selectorDialogService.ShowDialog(
                buttons,
                _project.Model.Tour.Scenes.Except(new[] { SelectedScene }),
                SelectIndex,
                SelectedHotSpot.Text,
                "Wybierz docelową scenę:"
            );
        }

        private void SelectIndex(Scene scene, int? buttonIndex)
        {
            if (!(SelectedHotSpot is SceneHotSpot spot))
                return;

            if (buttonIndex == 2 && scene != null)
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

        private async void HandleDrop(DragEventArgs args)
        {
            var paths = (string[])args.Data.GetData(DataFormats.FileDrop);

            if (paths == null)
            {
                IsRequiredFileIncludedInDragDrop = false;
                return;
            }

            _busyIndicatorService.SetBusy("Ładuję zdjęcia");
            IsRequiredFileIncludedInDragDrop = false;
            var files = EnumerateFilesWithCorrectExtension(paths);

            var scenesFromFiles = await Task.Run(() =>
            {
                var scenes = new List<Scene>();
                foreach (var file in files)
                {
                    var name = Path.GetFileNameWithoutExtension(file);
                    var scene = _sceneFactory.NewEquirectangularScene(name);
                    scenes.Add(scene);
                    scene.SetImage(file);
                }
                return scenes;
            });

            foreach (var scene in scenesFromFiles)
            {
                _project.Model.Tour.AddScene(scene);
            }

            SelectedScene = scenesFromFiles.FirstOrDefault();
            if (Project.Model.Tour.Default.FirstScene == null)
            {
                Project.Model.Tour.Default.FirstScene = SelectedScene;
            }

            _busyIndicatorService.ResetBusy();
            _busyIndicatorService.SetSnackbar($"Załadowano: {scenesFromFiles.Count} zdjęć.");
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
