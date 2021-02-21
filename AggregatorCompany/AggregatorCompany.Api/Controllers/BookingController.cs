using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AggregatorCompany.Api;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicrosoftExtensions = Microsoft.Extensions.Configuration;

namespace AggregatorCompany.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly TransactionManager _transactionManager;
        private readonly ILogger<BookingController> _logger;
        private readonly MicrosoftExtensions.IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public BookingController(TransactionManager transactionManager, ILogger<BookingController> logger, MicrosoftExtensions.IConfiguration configuration, IMapper mapper)
        {
            _transactionManager = transactionManager;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> BookTravel(BookTravelRequest request)
        {
            _logger.LogInformation($"||| Book travel request came in -- {request.ClientName}");
            
            //TO DO: Custom mapper configuration
            var transactionInfo = new TransactionInfo
            {
              TransactionId  = request.TransactionId,
              ApiCalls = new Dictionary<string, object>()
              {
                  { $"{_configuration["AccountCompanyApi"]}/payment" , _mapper.Map<AccountWithdrawRequest>(request) },
                  { $"{_configuration["HotelCompanyApi"]}/booking", _mapper.Map<BookHotelRequest>(request) },
                  { $"{_configuration["FlyCompanyApi"]}/booking", _mapper.Map<BookFlyRequest>(request) }
              }
            };

            var success = await _transactionManager.Execute(transactionInfo);
            _logger.LogInformation($"||| Book travel request result -- {success}");
            
            return success ? new StatusCodeResult(StatusCodes.Status201Created) : new StatusCodeResult(StatusCodes.Status503ServiceUnavailable) ;
        } 
    }
}