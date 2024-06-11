using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class FoodTransaction
    {
        public int Id { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerDetails CustomerDetails { get; set; }
        [ForeignKey("FoodItemId")]
        public FoodAccomodationMaster FoodAccomodationMaster { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("CreatedBy")]
        public Users Users { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
