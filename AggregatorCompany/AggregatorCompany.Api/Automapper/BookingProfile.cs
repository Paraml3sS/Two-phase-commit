using AggregatorCompany.Api;
using AutoMapper;

namespace AggregatorCompany.Api.Automapper
{
    public class BookingProfile: Profile
    {
        public BookingProfile()
        {
            CreateMap<BookTravelRequest, BookFlyRequest>();
            CreateMap<BookTravelRequest, BookHotelRequest>();
            CreateMap<BookTravelRequest, AccountWithdrawRequest>();
        }
    }
}