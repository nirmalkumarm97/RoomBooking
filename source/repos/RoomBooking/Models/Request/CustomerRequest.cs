using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class CustomerRequest
    {
        public string CustomerName { get; set; }
        public string Proof { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public int CreatedBy { get; set; }
    }
}
