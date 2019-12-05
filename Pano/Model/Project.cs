using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Pano.Model.Db.Scenes;

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

        public TourForDb Tour
        {
            get => _tour;
            set => Set(ref _tour, value);
        }

        public void SetSceneAsFirstScene(Scene scene)
        {
            if (!Tour.Scenes.Contains(scene))
                return;

            var previous = Tour.Default.FirstScene;
            Tour.Default.FirstScene = scene ?? throw new ArgumentNullException(nameof(scene));
            scene.RaisePropertyChanged(nameof(Scene.IsDefaultScene));
            previous?.RaisePropertyChanged(nameof(Scene.IsDefaultScene));
        }
    }
}
