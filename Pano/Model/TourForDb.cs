using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Pano.Model.Db.Scenes;

namespace Pano.Model
{
    public class TourForDb : ObservableObject
    {
        private Db.Scenes.DefaultSceneConfig _defaultSceneConfig = new Db.Scenes.DefaultSceneConfig();
        private int _tourForDbId;

        public int TourForDbId
        {
            get => _tourForDbId;
            set => Set(ref _tourForDbId, value);
        }

        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The default property contains options that are used for each scene, but options specified 
        /// for individual scenes override these options. The default property is required to have 
        /// a firstScene property that contains the scene ID for the first scene to be displayed.
        /// </summary>
        public Db.Scenes.DefaultSceneConfig Default { get; set; }

        /// <summary>
        /// The scenes property contains a dictionary of scenes, specified by scene IDs. The values 
        /// assigned to these IDs are specific to each scene.
        /// </summary>
        public ObservableCollection<Db.Scenes.Scene> Scenes { get; set; } = new ObservableCollection<Db.Scenes.Scene>();

        public SortedDictionary<string, Db.Scenes.Scene> ScenesDictionary =>
            new SortedDictionary<string, Scene>(Scenes.ToDictionary(x => x.Id));

        // Navigation property
        public Project Project { get; set; }

        //private Scene FirstScene => null; //Scenes?.FirstOrDefault().Value; // TODO


        public void AddScene(Model.Db.Scenes.Scene scene)
        {
            if(scene == null)
                throw new ArgumentNullException(nameof(scene));

            if(Scenes.Contains(scene))
                throw new ArgumentException(nameof(scene));

            scene.Tour = this;
            Scenes.Add(scene);
            if (_defaultSceneConfig.FirstScene == null)
            {
                _defaultSceneConfig.FirstScene = scene;
            }
        }

        public void DeleteScene(Model.Db.Scenes.Scene scene)
        {
            if(scene == null)
                throw new ArgumentNullException(nameof(scene));

            Scenes.Remove(scene);
        }

        //public void AddScene(Scene scene)
        //{
        //    if (scene == null || Scenes.ContainsValue(scene))
        //        return;

        //    Scenes.Add(scene.Id, scene);

        //    if (Default.FirstSceneRef == null)
        //    {
        //        Default.FirstSceneRef = scene;
        //    }
        //}

        //public void RemoveScene(Scene scene)
        //{
        //    Scenes.Remove(scene.Id);
        //    if (Default == scene)
        //    {
        //        Default.FirstSceneRef = null;
        //    }
        //}

        //protected bool Equals(TourForDb other)
        //{
        //    return Equals(_DefaultScene, other._DefaultScene) && Scenes.SequenceEqual(other.Scenes);
        //}

        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj))
        //        return false;

        //    if (ReferenceEquals(this, obj))
        //        return true;

        //    if (obj.GetType() != this.GetType())
        //        return false;

        //    return Equals((TourForDb) obj);
        //}

    }
}