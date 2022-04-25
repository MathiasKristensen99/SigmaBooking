using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;

namespace SigmaBooking.MongoDB.Repositories;

public class BookingRepository : IBookingRepository
{
    public Booking CreateBooking(Booking booking)
    {
        throw new NotImplementedException();
    }

    public Booking GetBooking(string id)
    {
        throw new NotImplementedException();
    }

    public void DeleteBooking(string id)
    {
        throw new NotImplementedException();
    }

    public Booking UpdateBooking(Booking booking)
    {
        throw new NotImplementedException();
    }
}