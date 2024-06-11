using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class BookingDetails
    {
        public int Id { get; set; }
        public string BookingId { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerDetails CustomerDetails { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("RoomType")]
        public RoomPriceMaster RoomPriceMaster { get; set; }
        public int RoomType { get; set; }
        public int AdultCount { get; set; }
        public int? ChildrenCount { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("CreatedBy")]
        public Users Users { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
