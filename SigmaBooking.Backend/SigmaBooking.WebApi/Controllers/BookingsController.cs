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
        
        [HttpGet]
        public ActionResult<BookingsDto> GetBookings()
        {
            try
            {
                var bookings = _bookingService.GetAllBookings().Select(booking => new BookingDto
                {
                    Id = booking.Id,
                    Name = booking.Name,
                    TableId = booking.TableId,
                    Phone = booking.Phone,
                    Email = booking.Email,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    IsEating = booking.IsEating,
                    Description = booking.Description
                }).ToList();

                return Ok(new BookingsDto
                {
                    List = bookings
                });

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<BookingDto> GetBooking(string id)
        {
            try
            {
                var booking = _bookingService.GetBooking(id);
                return Ok(new BookingDto
                {
                    Id = booking.Id,
                    Name = booking.Name,
                    TableId = booking.TableId,
                    Phone = booking.Phone,
                    Email = booking.Email,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    IsEating = booking.IsEating,
                    Description = booking.Description
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("date/{date}")]
        public ActionResult<BookingsDto> GetBookingsByDate(DateTime date)
        {
            try
            {
                var bookings = _bookingService.GetBookingsByDate(date).Select(booking => new BookingDto
                {
                    Id = booking.Id,
                    Name = booking.Name,
                    Email = booking.Email,
                    Phone = booking.Phone,
                    Description = booking.Description,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    TableId = booking.TableId,
                    IsEating = booking.IsEating
                }).ToList();

                return Ok(new BookingsDto
                {
                    List = bookings
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<BookingDto> UpdateBooking(string id, BookingDto dto)
        {
            var booking = _bookingService.UpdateBooking(new Booking
            {
                Id = id,
                Name = dto.Name,
                TableId = dto.TableId,
                Phone = dto.Phone,
                Email = dto.Email,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsEating = dto.IsEating,
                Description = dto.Description
            });
            return Ok(dto);
        }
    }
    
}
