using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pano.Model;
using Pano.Serialization.Model;

namespace Pano.Automapper
{
    public class TourProfile : Profile
    {
        public TourProfile()
        {
            CreateMap<TourForDb, Tour>()
                ;
        }
    }
}
