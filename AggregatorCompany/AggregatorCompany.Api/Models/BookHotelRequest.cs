using System;

namespace AggregatorCompany.Api
{
    public class BookHotelRequest: TransactionModel
    {
        public string ClientName { get; set; }
        public string HotelName { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
    }
}