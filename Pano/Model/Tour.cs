using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Model
{
    public class Tour
    {
        private DefaultScene _DefaultScene;
        public DefaultScene Default
        {
            get
            {
                if (_DefaultScene == null)
                {
                    _DefaultScene = new DefaultScene { Scene = FirstScene };
                    return _DefaultScene;
                }
                else if(_DefaultScene.Scene == null || !Scenes.ContainsValue(_DefaultScene.Scene))
                {
                    _DefaultScene.Scene = FirstScene;
                    return _DefaultScene;
                }
                else
                {
                    return _DefaultScene;
                }
            }
        }

        private Scene FirstScene => Scenes?.FirstOrDefault().Value;

        public Dictionary<string, Scene> Scenes { get; } = new Dictionary<string, Scene>();

        #region Methods
        public void AddScene(Scene scene)
        {
            if (scene == null || Scenes.ContainsValue(scene))
                return;

            Scenes.Add(scene.Id, scene);

            if(Default.Scene == null)
            {
                Default.Scene = scene;
            }
        }

        public void RemoveScene(Scene scene)
        {
            Scenes.Remove(scene.Id);
            if(Default == scene)
            {
                Default.Scene = null;
            }
        }
        #endregion
    }
}
