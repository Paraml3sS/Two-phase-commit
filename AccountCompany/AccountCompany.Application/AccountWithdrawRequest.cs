using System;

namespace AccountCompany.Application
{
    public class AccountWithdrawRequest
    {
        public Guid TransactionId { get; set; }
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int Amount { get; set; }
    }
}