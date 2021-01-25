using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Api.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }        
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public Guid AccountId { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        
        public Category Category { get; set; }    
        public Account Account { get; set; }
    }
}
