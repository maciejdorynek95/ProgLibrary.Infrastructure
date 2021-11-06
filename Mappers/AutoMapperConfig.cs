using AutoMapper;
using ProgLibrary.Core.Domain;
using ProgLibrary.Infrastructure.DTO;
using ProgLibrary.Infrastructure.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ProgLibrary.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        => new MapperConfiguration(configuration =>
        {

            /// Book
            configuration.CreateMap<Book, BookDto>()
                .ForMember(x => x.BookReservations, m => m.MapFrom(p => p.Reservations.Count())); // wyjatek 1 
            configuration.CreateMap<BookDto, BookViewModel>();

            configuration.CreateMap<Book, BookDetailsDto>()
                .ForMember(x => x.BookReservations, m => m.MapFrom(p => p.Reservations.Count())); // wyjatek 2   
            configuration.CreateMap<BookDetailsDto, BookDetailsViewModel>();

            ///User
            configuration.CreateMap<User, AccountDto>();
            configuration.CreateMap<User, AccountDetailsDto>();

            /// Account
            configuration.CreateMap<AccountDto, AccountViewModel>();
            configuration.CreateMap<AccountDetailsDto, AccountDetailsViewModel>();

            ///Reservation
            configuration.CreateMap<Reservation, ReservationDto>();
            configuration.CreateMap<ReservationDto, ReservationViewModel>();
        })
            .CreateMapper();
    }
}
