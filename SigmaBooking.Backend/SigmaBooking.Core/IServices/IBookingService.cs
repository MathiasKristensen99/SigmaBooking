using SigmaBooking.Core.Models;

namespace SigmaBooking.Core.IServices;

public interface IBookingService
{
    Booking CreateBooking(Booking booking);

    Booking GetBooking(string id);

    void DeleteBooking(string id);

    Booking UpdateBooking(Booking booking);
    
    List<Booking> GetAllBookings();
}