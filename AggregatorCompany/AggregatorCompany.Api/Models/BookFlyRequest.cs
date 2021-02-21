using System;

namespace AggregatorCompany.Api
{
    public class BookFlyRequest: TransactionModel
    {
        public string ClientName { get; set; }
        public string FlyNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
    }
}