using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pano.Model;
using Pano.Model.Db.Scenes;
using Pano.Serialization.Model;
using Pano.Serialization.Model.Scenes;

namespace Pano.Automapper
{
    public class TourProfile : Profile
    {
        public TourProfile()
        {
            CreateMap<TourForDb, Tour>()
                .ForSourceMember(src => src.Scenes, opt => opt.DoNotValidate())
                .ForMember(dst => dst.Scenes, opt => opt.MapFrom(src => src.ScenesDictionary))
                ;


        }
    }
}
