using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HotelCompany.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly DistributedTransactionHandler _transactionHandler;
        
        public BookingController(DistributedTransactionHandler transactionHandler)
        {
            _transactionHandler = transactionHandler;
        }
        
        [HttpPost]
        public async Task<IActionResult> BookHotel(BookHotelRequest booking)
        {
            var result = await _transactionHandler.HandlePrepare(booking.TransactionId,
                 @"INSERT INTO hotelbooking(ClientName,HotelName,Arrival,Departure)
                    VALUES(@ClientName, @HotelName, @Arrival, @Departure);",
                    new { booking.ClientName, booking.HotelName, booking.Arrival, booking.Departure }
            );

            return result == 1 
                ? Ok(booking.TransactionId.ToString()) 
                : Problem(booking.TransactionId.ToString());
        }
        
        [HttpPost("rollback")]
        public async Task<ActionResult> Rollback([FromBody]Guid transactionId) => Ok(await _transactionHandler.HandleRollback(transactionId));
        
        [HttpPost("commit")]
        public async Task<ActionResult> Commit([FromBody]Guid transactionId) => Ok(await _transactionHandler.HandleCommit(transactionId));
    }
}