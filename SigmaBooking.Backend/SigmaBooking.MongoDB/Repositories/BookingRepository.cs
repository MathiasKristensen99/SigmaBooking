using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.MongoDB.Entities;

namespace SigmaBooking.MongoDB.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly IMongoCollection<BookingEntity> _bookingsCollection;

    public BookingRepository(IOptions<SigmaBookingDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(
            options.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            options.Value.DatabaseName);

        _bookingsCollection = mongoDatabase.GetCollection<BookingEntity>(
            options.Value.BookingsCollectionName);
    }
    public Booking CreateBooking(Booking booking)
    {
        var bookingEntity = new BookingEntity
        {
            Id = booking.Id,
            Name = booking.Name,
            Phone = booking.Phone,
            Time = booking.Time,
            Description = booking.Description
        };
        
        _bookingsCollection.InsertOne(bookingEntity);

        return new Booking
        {
            Id = bookingEntity.Id,
            Name = bookingEntity.Name,
            Phone = bookingEntity.Phone,
            Time = bookingEntity.Time,
            Description = bookingEntity.Description
        };
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