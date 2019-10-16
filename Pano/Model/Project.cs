using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace Pano.Model
{
    public class Project : ObservableObject
    {
        private int _projectId;
        private string _name = "";
        private string _description = "";
        private DateTime _dateOfCreation = DateTime.MinValue;
        private DateTime _dateOfLastModification = DateTime.MinValue;
        private TourForDb _tour;

        public Project()
        {
            var now = DateTime.Now;
            DateOfCreation = now;
            DateOfLastModification = now;
            _tour = new TourForDb();
            //var defaultScene = new Model.Db.Scenes.DefaultScene() {Title = "default scene title"};
            //var scene1 = new Model.Db.Scenes.Equirectangular() {Title = "equirectangular title 1"};
            //var scene2 = new Model.Db.Scenes.Equirectangular() {Title = "equirectangular title 2"};
            //var spot = new Model.Db.HotSpots.SceneHotSpot() {Scene = scene1, TargetScene = scene2};
            //defaultScene.FirstSceneRef = scene1;
            //scene1.HotSpots.Add(spot);
            //_tour.Default = defaultScene;
            //_tour.Scenes.Add(scene1);
            //_tour.Scenes.Add(scene2);


        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int ProjectId
        {
            get => _projectId;
            set
            {
                Set(ref _projectId, value);
                RaisePropertyChanged(nameof(ProjectIdString));
            }
        }

        public string ProjectIdString => _projectId.ToString();

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        public DateTime DateOfCreation
        {
            get => _dateOfCreation;
            set => Set(ref _dateOfCreation, value);
        }

        public DateTime DateOfLastModification
        {
            get => _dateOfLastModification;
            set => Set(ref _dateOfLastModification, value);
        }

        public virtual TourForDb Tour
        {
            get => _tour;
            set => Set(ref _tour, value);
        }
    }
}
