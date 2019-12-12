using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pano.Model;
using Pano.Serialization.Model.Scenes;

namespace Pano.Serialization.Model
{
    public class Tour
    {
        private DefaultSceneDto _DefaultScene;

        /// <summary>
        /// The default property contains options that are used for each scene, but options specified 
        /// for individual scenes override these options. The default property is required to have 
        /// a firstScene property that contains the scene ID for the first scene to be displayed.
        /// </summary>
        public DefaultSceneDto Default
        {
            get => _DefaultScene;
            set => _DefaultScene = value;
        }

        /// <summary>
        /// The scenes property contains a dictionary of scenes, specified by scene IDs. The values 
        /// assigned to these IDs are specific to each scene.
        /// </summary>
        public SortedDictionary<string, SceneDto> Scenes { get; } = new SortedDictionary<string, SceneDto>();

        private SceneDto FirstSceneDto => Default?.FirstSceneDtoRef; // ?? Scenes?.FirstOrDefault().Value;

        public void AddScene(SceneDto scene)
        {
            if (scene == null || Scenes.ContainsValue(scene))
                return;

            Scenes.Add(scene.Id, scene);

            if (Default.FirstSceneDtoRef == null)
            {
                Default.FirstSceneDtoRef = scene;
            }
        }

        public void RemoveScene(SceneDto scene)
        {
            Scenes.Remove(scene.Id);
            if (Default == scene)
            {
                Default.FirstSceneDtoRef = null;
            }
        }

        protected bool Equals(Tour other)
        {
            return Equals(_DefaultScene, other._DefaultScene) && Scenes.SequenceEqual(other.Scenes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((Tour)obj);
        }
    }
}
