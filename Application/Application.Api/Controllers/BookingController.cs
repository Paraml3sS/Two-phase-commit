using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Database.Contexts;
using Application.Database.Contexts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly HotelContext _hotelContext;
        private readonly FlyContext _flyContext;
        private readonly AccountContext _accountContext;

        /// <inheritdoc />
        public BookingController(HotelContext hotelContext, FlyContext flyContext, AccountContext accountContext)
        {
            _hotelContext = hotelContext;
            _flyContext = flyContext;
            _accountContext = accountContext;
        }
        

        [HttpGet]
        public async Task<ActionResult> GetHotelBookings()
        {
            var hotelBookingsTask = _hotelContext.HotelBookings
                .FromSqlRaw("SELECT * FROM schemaone.hotelbooking;")
                .AsNoTracking()
                .ToListAsync();
            
            var flyBookingsTask = _flyContext.FlyBookings
                .FromSqlRaw("SELECT * FROM schematwo.flybooking;")
                .AsNoTracking()
                .ToListAsync();
            
            var accountsTask = _accountContext.Accounts
                .FromSqlRaw("SELECT * FROM schemathree.account;")
                .AsNoTracking()
                .ToListAsync();
            
            var (hotelBookings, flyBookings, accounts) = (await hotelBookingsTask, await flyBookingsTask, await accountsTask);
            
            return Ok(new {
                FlyBookings = flyBookings,
                Accounts = accounts,
                HotelBookings = hotelBookings
            });
        }
        
        [HttpPost]
        public async Task<ActionResult> AddBooking()
        {
            var booking = CreateBooking();
            
            var guids = Enumerable.Range(1,3).Select(x => Guid.NewGuid()).ToList();
            
            var updateAccountTask = _accountContext.Database.ExecuteSqlRawAsync(
                $@"Begin;
                   UPDATE schemathree.account SET amount = amount - 1000 WHERE ClientName = 'Жирний';
                   PREPARE TRANSACTION '{guids[0]}';"
            );

            var saveHotelBookingTask = _hotelContext.Database.ExecuteSqlRawAsync(
                $@"Begin;
                   INSERT INTO schematwo.flybooking(ClientName,FlyNumber,""From"",""To"",""Date"")
                   VALUES('{booking.FlyBooking.ClientName}', '{booking.FlyBooking.FlyNumber}', '{booking.FlyBooking.From}', '{booking.FlyBooking.To}', '{booking.FlyBooking.Date}');
                   PREPARE TRANSACTION '{guids[1]}'"
            );
            
            var saveFlyBookingTask = _flyContext.Database.ExecuteSqlRawAsync(
                $@"Begin;
                   INSERT INTO schemaone.hotelbooking(ClientName,HotelName,Arrival,Departure)
                   VALUES('{booking.HotelBooking.ClientName}','{booking.HotelBooking.HotelName}', '{booking.HotelBooking.Arrival}', '{booking.HotelBooking.Departure}');
                   PREPARE TRANSACTION '{guids[2]}'"
            );
            
            var (accountUpdate, hotelBooking, flyBooking) = (await updateAccountTask, await saveHotelBookingTask, await saveFlyBookingTask).ToTuple();

            if (new List<int> {accountUpdate, hotelBooking, flyBooking}.Any( v => v == 0))
            {
                var rollbackAccount =  _accountContext.Database.ExecuteSqlRawAsync(
                    $@"ROLLBACK PREPARED '{guids[0]}';"
                );
                
                var rollbackHotel = _hotelContext.Database.ExecuteSqlRawAsync(
                    $@"ROLLBACK PREPARED '{guids[1]}';"
                );

                var rollbackFly = _flyContext.Database.ExecuteSqlRawAsync(
                    $@"ROLLBACK PREPARED '{guids[2]}';"
                );


                _ = (await rollbackAccount, await rollbackHotel, await rollbackFly);

                
            }
            else
            {
                var commitAccount =  _accountContext.Database.ExecuteSqlRawAsync(
                    $@"COMMIT PREPARED '{guids[0]}';"
                );
                
                var commitHotel = _hotelContext.Database.ExecuteSqlRawAsync(
                    $@"COMMIT PREPARED '{guids[1]}';"
                );

                var commitFly = _flyContext.Database.ExecuteSqlRawAsync(
                    $@"COMMIT PREPARED '{guids[2]}';"
                );


                _ = (await commitAccount, await commitHotel, await commitFly);
            }
            
            
            return Ok();
        }
        
        private Booking CreateBooking()
        {
            return new Booking {
                Account = new Account
                {
                    ClientName = "Petro",
                    Amount = 100
                },
                FlyBooking = new FlyBooking
                {
                    ClientName = "Petro",
                    FlyNumber = "I believe I can fly",
                    From = "Papua",
                    To = "Gvinea",
                    Date = DateTime.UtcNow
                },
                HotelBooking = new HotelBooking
                {
                    ClientName = "Petro",
                    HotelName = "Kukuriky",
                    Departure = DateTime.UtcNow,
                    Arrival = DateTime.Now + TimeSpan.FromDays(2)
                }
            };
        }
    }
}