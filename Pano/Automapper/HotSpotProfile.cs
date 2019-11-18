using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pano.Model.Db.Helpers;
using Pano.Model.Db.HotSpots;
using Pano.Model.Db.Scenes;
using Pano.Serialization.Model.HotSpots;
using Pano.Serialization.Model.Scenes;

namespace Pano.Automapper
{
    public class HotSpotProfile : Profile
    {
        public HotSpotProfile()
        {
            CreateMap<HotSpot, IHotSpotDto>()
                .ConstructUsing((spot, context) =>
                {
                    switch (spot.Type)
                    {
                        case HotSpotType.Info:
                            return new InfoHotSpotDto();
                        case HotSpotType.Scene:
                            return new SceneHotSpotDto();
                        default:
                            return null;
                    }
                })
                ;

            CreateMap<HotSpot, HotSpotDto>()
                .IncludeBase<HotSpot, IHotSpotDto>()
                .ForSourceMember(src => src.Scene, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.SceneId, opt => opt.DoNotValidate())
                ;

            CreateMap<SceneHotSpot, SceneHotSpotDto>()
                .IncludeBase<HotSpot, HotSpotDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .AfterMap((src, dest) => dest.SceneId = src.TargetSceneId?.ToString())
                ;

            CreateMap<ObservableCollection<HotSpot>, List<IHotSpot>>();
        }
    }
}
