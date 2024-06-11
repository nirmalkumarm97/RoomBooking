
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class BillingTransaction
    {
        public int Id { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerDetails CustomerDetails { get; set; }
        public int CustomerId { get; set; }
        public double TotalCostOfRooms { get; set; }
        public double TotalCostOfFood { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
