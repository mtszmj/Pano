using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pano.Automapper;
using Pano.Model;
using Pano.Serialization.Model;
using System.Reflection;

namespace Pano.Service
{
    public class SerializationMapper : ISerializationMapper
    {
        private IMapper _mapper;
        public SerializationMapper()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assembly);
            });

            _mapper = new Mapper(config);
        }

        public TDest Map<TSource, TDest>(TSource source)
        {
            return _mapper.Map<TDest>(source);
        }
    }
}
