using System;
using System.Threading.Tasks;
using FlyCompany.Application;
using Microsoft.AspNetCore.Mvc;

namespace FlyCompany.Api.Controllers
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
        public async Task<ActionResult> BookHotel(BookFlyRequest booking)
        {
            var result = await _transactionHandler.HandlePrepare(booking.TransactionId,
                 @"INSERT INTO flybooking(ClientName,FlyNumber,""From"",""To"",""Date"")
                    VALUES(@ClientName, @FlyNumber, @From, @To, @Date);",
                    new { booking.ClientName, booking.FlyNumber, booking.From, booking.To, booking.Date }
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