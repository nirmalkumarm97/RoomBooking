using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.NewFolder
{
    public class RoomBookingDbContext : DbContext
    {
        public RoomBookingDbContext(DbContextOptions<RoomBookingDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<CustomerDetails> CustomerDetails { get; set; }
        public virtual DbSet<BookingDetails> BookingDetails { get; set; }
        public virtual DbSet<RoomPriceMaster> RoomPriceMaster { get; set; }
    }
}
