using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Parent_Bank.Models
{
    [CustomValidation(typeof(Account), "ValidateEmail")]
    public class Account
    {
        public Account() {
            OpenDate = DateTime.Now;
            Transactions = new List<Transaction>();
            Wishlists = new List<Wishlist>();
        }
        public int AccountId { get; set; }


        [EmailAddress]
        [Required]
        public string Owner { get; set; }

        [EmailAddress]
        [Required]
        public string Recepient { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime OpenDate { get; set; }

        [Range(1,99)]
        [Required]
        public float InterestRate { get; set; }

        [Required]
        public double Balance { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<Wishlist> Wishlists { get; set; }


        public static ValidationResult ValidateEmail(Account account, ValidationContext context)
        {
            // IF THE VALUE IS BLANK THEN NO NEED TO TEST RETURN SUCCESS
            if (account.Owner.Equals(account.Recepient))
            {
                return new ValidationResult("Owner and Recepient cannot be the same");
            }
            return ValidationResult.Success;
        }
    }
}