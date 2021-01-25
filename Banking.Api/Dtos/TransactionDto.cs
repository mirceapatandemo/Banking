namespace Banking.Api.Dtos
{
    public class TransactionDto
    {
        public string Iban { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string TransactionDate { get; set; }
    }
}
