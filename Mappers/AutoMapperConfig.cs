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
            
            /// DTO
 
            configuration.CreateMap<Book, BookDto>()
            .ForMember(x => x.BookReservations, m => m.MapFrom(p => p.Reservations.Count())); // wyjatek 1 
            configuration.CreateMap<Book, BookDetailsDto>()
              .ForMember(x => x.BookReservations, m => m.MapFrom(p => p.Reservations.Count())); // wyjatek 1 
            configuration.CreateMap<Reservation, ReservationDto>();
            configuration.CreateMap<User, AccountDto>();
            configuration.CreateMap<User, AccountDetailsDto>();


            /// ViewModel
            configuration.CreateMap<BookDto, BookViewModel>();
            //.ForMember(x => x.BookReservations, m => m.MapFrom(p => p.BookReservations));
            configuration.CreateMap<BookDetailsDto, BookDetailsViewModel>();
            //.ForMember(x => x.BookReservations, m => m.MapFrom(p => p.Reservations.Count()));

        })
            .CreateMapper();
    }
}
