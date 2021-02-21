using System;

namespace AggregatorCompany.Api
{
    public class TransactionModel
    {
        public Guid TransactionId { get; set; } = Guid.NewGuid();
    }
}