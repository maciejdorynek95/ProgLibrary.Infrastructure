using AutoMapper;
using ProgLibrary.Core.Domain;
using ProgLibrary.Infrastructure.DTO;
using System.Linq;

namespace ProgLibrary.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        => new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<Book, BookDto>();
            //.ForMember(x => x.BooksReservations, m => m.MapFrom(p => p..Count())); // wyjatek 1 
            configuration.CreateMap<Book, BookDetailsDto>();
            configuration.CreateMap<Reservation, ReservationDto>();
            configuration.CreateMap<User, AccountDto>();
            configuration.CreateMap<User, AccountDetailsDto>();
        })
            .CreateMapper();
    }
}
