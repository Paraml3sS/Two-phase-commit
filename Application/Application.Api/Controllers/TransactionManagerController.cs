using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Database;
using Application.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionManagerController : Controller
    {
        private readonly HotelContext _hotelDb;

        public TransactionManagerController(HotelContext hotelDb)
        {
            _hotelDb = hotelDb;
        }

        [HttpGet]
        public async Task<IEnumerable<HotelBooking>> GetHotelBookings()
        {
            return await _hotelDb.HotelBookings
                .FromSqlRaw("SELECT * FROM schemaone.hotelbookings;")
                .AsNoTracking()
                .ToListAsync();
        }
    }
}