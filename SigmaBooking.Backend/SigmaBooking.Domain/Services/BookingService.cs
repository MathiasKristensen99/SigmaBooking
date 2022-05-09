using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;

namespace SigmaBooking.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _repository = bookingRepository;
    }
    
    public Booking CreateBooking(Booking booking)
    {
        return _repository.CreateBooking(booking);
    }

    public Booking GetBooking(string id)
    {
        return _repository.GetBooking(id);
    }

    public void DeleteBooking(string id)
    {
        _repository.DeleteBooking(id);
    }

    public Booking UpdateBooking(Booking booking)
    {
        return _repository.UpdateBooking(booking);
    }
    
    public List<Booking> GetAllBookings()
    {
        return _repository.GetAllBookings();
    }
}