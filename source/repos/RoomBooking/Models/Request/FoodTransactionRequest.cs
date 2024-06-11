using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class FoodTransactionRequest
    {
        public int? Id { get; set; }
        public int FoodItemId { get; set; }
        public int CreatedBy { get; set; }
    }
}
