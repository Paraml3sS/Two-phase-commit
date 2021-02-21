using System;
using System.Collections.Generic;

namespace AggregatorCompany.Api
{
    public class TransactionInfo
    {
        public Guid TransactionId { get; set; }
        public Dictionary<string, Object> ApiCalls { get; set; }
    }
}