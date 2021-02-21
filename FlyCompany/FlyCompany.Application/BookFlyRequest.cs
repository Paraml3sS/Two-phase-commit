using System;

namespace FlyCompany.Application
{
    public class BookFlyRequest
    {
        public Guid TransactionId { get; set; }
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string FlyNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
    }
}