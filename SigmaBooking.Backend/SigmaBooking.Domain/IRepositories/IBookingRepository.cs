using SigmaBooking.Core.Models;

namespace SigmaBooking.Domain.IRepositories;

public interface IBookingRepository
{
    Booking CreateBooking(Booking booking);

    Booking GetBooking(string id);

    void DeleteBooking(string id);

    Booking UpdateBooking(Booking booking);
    
    List<Booking> GetAllBookings();
    
    List<Booking> GetBookingsByDate(string date);
    
    List<Booking> GetBookingsByTableAndDate(string id, string date);
}