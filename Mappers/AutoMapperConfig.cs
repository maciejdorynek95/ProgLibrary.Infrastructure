using AutoMapper;
using ProgLibrary.Core.Domain;
using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Book, BookDto>()
            .ForMember(x=>x.BooksReservations,m=>m.MapFrom(p=>p.Reservations.Count())); // wyjatek 1 
        })
            .CreateMapper();
    }
}
