using Application.Database.Contexts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Database.Contexts
{
    public class FlyContext: DbContext
    {
        public FlyContext(DbContextOptions<FlyContext> options): base(options) { }
        
        public DbSet<FlyBooking> FlyBookings { get; set; }
    }
}