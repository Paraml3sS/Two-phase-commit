using System;
using System.Threading.Tasks;
using AccountCompany.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HotelCompany.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly DistributedTransactionHandler _transactionHandler;
        
        public PaymentController(DistributedTransactionHandler transactionHandler)
        {
            _transactionHandler = transactionHandler;
        }
        
        
        [HttpPost]
        public async Task<ActionResult> AccountWithdraw(AccountWithdrawRequest withdrawRequest)
        {
            var result = await _transactionHandler.HandlePrepare(withdrawRequest.TransactionId,
                 @"UPDATE account SET amount = amount - @Amount WHERE ClientName = @ClientName;",
                    new { withdrawRequest.ClientName, withdrawRequest.Amount }
            );

            return result == 1 
                ? Ok(withdrawRequest.TransactionId.ToString()) 
                : Problem(withdrawRequest.TransactionId.ToString());
        }
        
        [HttpPost("rollback")]
        public async Task<ActionResult> Rollback([FromBody]Guid transactionId) => Ok(await _transactionHandler.HandleRollback(transactionId));
        
        [HttpPost("commit")]
        public async Task<ActionResult> Commit([FromBody]Guid transactionId) => Ok(await _transactionHandler.HandleCommit(transactionId));
    }
}