using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Banking.Api.Entities
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }
        public string Product { get; set; }
        public string Iban { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
