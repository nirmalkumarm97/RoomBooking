﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class CustomerDetails
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Proof { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("CreatedBy")]
        public Users Users { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public virtual ICollection<BookingDetails> Bookings { get; set; }
        public virtual ICollection<BillingTransaction> BillingTransactions { get; set; }

    }
}
