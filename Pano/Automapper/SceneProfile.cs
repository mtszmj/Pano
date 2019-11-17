using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pano.Model.Db.Helpers;
using Pano.Model.Db.Scenes;
using Pano.Serialization.Model.Scenes;

namespace Pano.Automapper
{
    public class SceneProfile : Profile
    {
        public SceneProfile()
        {
            CreateMap<Scene, SceneDto>()
                ;

            CreateMap<Equirectangular, EquirectangularDto>()
                .IncludeBase<Scene, SceneDto>()
                ;

            CreateMap<DefaultSceneConfig, DefaultSceneDto>()
                ;

            CreateMap<IEnumerable<StringDictionaryEntry>, Dictionary<string, string>>()
                .AfterMap((entries, dict) =>
                {
                    foreach (var entry in entries)
                    {
                        dict[entry.Key] = entry.Value;
                    }
                })
                ;
        }

    }
}
