using AutoMapper;
using ProgLibrary.Core.Domain;
using ProgLibrary.Infrastructure.DTO;
using System.Linq;

namespace ProgLibrary.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Book, BookDto>();
            //.ForMember(x => x.BooksReservations, m => m.MapFrom(p => p..Count())); // wyjatek 1 
            cfg.CreateMap<Book, BookDetailsDto>();
            cfg.CreateMap<Reservation, ReservationDto>();
            cfg.CreateMap<User, AccountDto>();
        })
            .CreateMapper();
    }
}
