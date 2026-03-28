using AutoMapper;
using EventBooking.API.DTOs;
using EventBooking.API.Entities;

namespace EventBooking.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ── Event Mappings ──────────────────────────────────────────────
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.BookedSeats,
                    opt => opt.MapFrom(src =>
                        src.Bookings
                           .Where(b => b.Status == "Confirmed")
                           .Sum(b => b.SeatsBooked)));

            CreateMap<CreateEventDto, Event>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateEventDto, Event>();

            // ── Booking Mappings ────────────────────────────────────────────
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.EventTitle,
                    opt => opt.MapFrom(src => src.Event != null ? src.Event.Title : string.Empty))
                .ForMember(dest => dest.EventDate,
                    opt => opt.MapFrom(src => src.Event != null
                        ? src.Event.Date.ToString("dddd, MMMM dd yyyy 'at' hh:mm tt")
                        : string.Empty))
                .ForMember(dest => dest.EventLocation,
                    opt => opt.MapFrom(src => src.Event != null ? src.Event.Location : string.Empty))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User != null ? src.User.FullName : string.Empty));

            CreateMap<CreateBookingDto, Booking>()
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Confirmed"))
                .ForMember(dest => dest.ConfirmationCode,
                    opt => opt.MapFrom(_ => Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()));

            // ── User Mappings ───────────────────────────────────────────────
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.RegisteredAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => "User"));
        }
    }
}
