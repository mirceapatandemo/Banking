namespace Banking.Api.Dtos
{
    public class TransactionSummaryDto
    {
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
    }
}
