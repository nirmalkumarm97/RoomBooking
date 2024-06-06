using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class RoomPriceMaster
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public double PriceWithTax { get; set; }
        public double PriceWithoutTax { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public virtual ICollection<BookingDetails> Rooms { get; set; }
    }
}
