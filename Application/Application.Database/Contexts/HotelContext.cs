using Application.Database.Contexts.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Database.Contexts
{
    public class HotelContext: DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options): base(options) { }
        
        public DbSet<HotelBooking> HotelBookings { get; set; }
    }
}