using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class BillingResponse
    {
        public List<RoomList> Billing { get; set; }
    }
    public class RoomList
    {
        public string RoomName { get; set; }
        public int AdultCount { get; set; }
        public int? ChildrenCount { get; set; }
        public double Price { get; set; }
        public int NoOfDays { get; set; }
    }
}
