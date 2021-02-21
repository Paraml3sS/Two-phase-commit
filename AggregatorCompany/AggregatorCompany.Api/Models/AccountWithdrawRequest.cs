namespace AggregatorCompany.Api
{
    public class AccountWithdrawRequest: TransactionModel
    {
        public string ClientName { get; set; }
        public int Amount { get; set; }
    }
}