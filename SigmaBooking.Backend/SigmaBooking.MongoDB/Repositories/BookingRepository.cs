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
            TableId = booking.TableId,
            Name = booking.Name,
            Phone = booking.Phone,
            Email = booking.Email,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            IsEating = booking.IsEating,
            Description = booking.Description
        };
        
        _bookingsCollection.InsertOne(bookingEntity);

        return new Booking
        {
            Id = bookingEntity.Id,
            TableId = bookingEntity.TableId,
            Name = bookingEntity.Name,
            Phone = bookingEntity.Phone,
            Email = bookingEntity.Email,
            StartTime = bookingEntity.StartTime,
            EndTime = bookingEntity.EndTime,
            IsEating = bookingEntity.IsEating,
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