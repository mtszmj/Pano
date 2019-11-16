using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pano.Model.Db.HotSpots;
using Pano.Serialization.Model.HotSpots;

namespace Pano.Automapper
{
    public class HotSpotProfile : Profile
    {
        public HotSpotProfile()
        {
            CreateMap<SceneHotSpot, SceneHotSpotDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .AfterMap((src, dest) => dest.SceneId = src.TargetSceneId?.ToString());
        }
    }
}
