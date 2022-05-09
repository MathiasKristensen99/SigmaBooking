using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.WebApi.Dtos;

namespace SigmaBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService service)
        {
            _bookingService = service;
        }

        [HttpPost]
        public ActionResult<BookingDto> CreateBooking([FromBody] BookingDto dto)
        {
            var bookingFromDto = new Booking
            {
                Name = dto.Name,
                TableId = dto.TableId,
                Phone = dto.Phone,
                Email = dto.Email,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsEating = dto.IsEating,
                Description = dto.Description
            };

            try
            {
                var newBooking = _bookingService.CreateBooking(bookingFromDto);
                return Created($"https://localhost:7026/api/bookings/{newBooking.Id}", newBooking);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        [HttpDelete("{id}")]
        public void DeleteBooking(string id)
        {
            _bookingService.DeleteBooking(id);
        }
    }
    
}
