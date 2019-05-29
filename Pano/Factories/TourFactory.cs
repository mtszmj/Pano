using Pano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Factories
{
    public class TourFactory
    {
        public Tour CreateDefaultTour()
        {
            var spotsFactory = new HotSpotFactory();
            var sceneFactory = new ScenesFactory();
            var tour = new Tour();

            var scene1 = sceneFactory.CreateDefaultScene();
            var scene2 = sceneFactory.CreateDefaultScene();
            tour.AddScene(scene1);
            tour.AddScene(scene2);

            return tour;

        }
    }
}
