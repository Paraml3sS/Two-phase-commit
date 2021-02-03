using Application.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Database
{
    public class HotelContext: DbContext
    {
        public DbSet<HotelBooking> HotelBookings { get; set; }
        
        public HotelContext(DbContextOptions<HotelContext> options): base(options) { }
    }
}