namespace Banking.Api.Entities
{
    public class TransactionSummary
    {
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
    }
}
