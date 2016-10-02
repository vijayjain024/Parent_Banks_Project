using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Parent_Bank.Models
{
    public class Wishlist
    {
        public int id { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public string Description { get; set; }
        [Url]
        [Required]
        public string Link { get; set; }
        public DateTime DateAdded { get; set; }
        public bool approvedStatus { get; set; }
        public virtual int AccountId { get; set; }
        public virtual Account Account { get; set; }
        // Nikhil Marathe first commit - test
    }
}