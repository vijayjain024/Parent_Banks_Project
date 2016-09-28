using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Parent_Bank.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        //public string Account { get; set; }
        public DateTime TransactionDate { get; set; }

        [Required]
        [CustomValidation(typeof(Transaction), "CheckTransactionAmount")]
        public float Amount { get; set; }

        [Required]
        public string Note { get; set; }
        public virtual int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public static ValidationResult CheckTransactionAmount(Transaction transactions, ValidationContext context)
        {
            if (transactions.Amount == 0)
                return new ValidationResult("Transaction amount cannot be less than 0");
            else
                return ValidationResult.Success;
        }
         
    }
}