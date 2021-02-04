using Application.Database.Contexts.Models;

namespace Application.Api
{
    public class Booking
    {
        public HotelBooking HotelBooking { get; set; }
        public FlyBooking FlyBooking { get; set; }
        public Account Account { get; set; }
    }
}