using System;

namespace Application.Database.Models
{
    public class HotelBooking
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string HotelName { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
    }
}