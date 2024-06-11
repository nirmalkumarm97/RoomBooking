using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class BookingRequest
    {
        public int CustomerId { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public int RoomType { get; set; }
        public int AdultCount { get; set; }
        public int? ChildrenCount { get; set; }
        public int CreatedBy { get; set; }
    }
}
