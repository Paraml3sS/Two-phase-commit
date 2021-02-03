using System;

namespace Application.Database.Models
{
    public class FlyBooking
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string FlyNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
    }
}