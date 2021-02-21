using System;

namespace AggregatorCompany.Api
{
    public class BookTravelRequest: TransactionModel
    {
        public string ClientName { get; set; }
        public string FlyNumber { get; set; }
        public string HotelName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public int Amount { get; set; }
    }
}