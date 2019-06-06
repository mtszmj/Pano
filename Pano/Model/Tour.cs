using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model
{
    public class Tour
    {
        #region Fields
        private DefaultScene _DefaultScene;
        #endregion

        #region Properties
        /// <summary>
        /// The default property contains options that are used for each scene, but options specified 
        /// for individual scenes override these options. The default property is required to have 
        /// a firstScene property that contains the scene ID for the first scene to be displayed.
        /// </summary>
        public DefaultScene Default
        {
            get
            {
                if (_DefaultScene == null)
                {
                    _DefaultScene = new DefaultScene { FirstSceneRef = FirstScene };
                    return _DefaultScene;
                }
                else if(_DefaultScene.FirstSceneRef == null || !Scenes.ContainsValue(_DefaultScene.FirstSceneRef))
                {
                    _DefaultScene.FirstSceneRef = FirstScene;
                    return _DefaultScene;
                }
                else
                {
                    return _DefaultScene;
                }
            }
        }

        /// <summary>
        /// The scenes property contains a dictionary of scenes, specified by scene IDs. The values 
        /// assigned to these IDs are specific to each scene.
        /// </summary>
        public Dictionary<string, Scene> Scenes { get; } = new Dictionary<string, Scene>();

        private Scene FirstScene => Scenes?.FirstOrDefault().Value;

        #endregion

        #region Methods
        public void AddScene(Scene scene)
        {
            if (scene == null || Scenes.ContainsValue(scene))
                return;

            Scenes.Add(scene.Id, scene);

            if(Default.FirstSceneRef == null)
            {
                Default.FirstSceneRef = scene;
            }
        }

        public void RemoveScene(Scene scene)
        {
            Scenes.Remove(scene.Id);
            if(Default == scene)
            {
                Default.FirstSceneRef = null;
            }
        }
        #endregion
    }
}
