using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
        var booking = _bookingsCollection.Find(Builders<BookingEntity>.Filter.Eq("_id", ObjectId.Parse(id)))
            .FirstOrDefault();
        return new Booking
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
    }

    public void DeleteBooking(string id)
    {
        _bookingsCollection.DeleteOne(entity => entity.Id == id);
    }

    public Booking UpdateBooking(Booking booking)
    {
        var filter = Builders<BookingEntity>.Filter.Eq("_id", ObjectId.Parse(booking.Id));

        var entity = new BookingEntity
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
        
        _bookingsCollection.ReplaceOne(filter, entity);

        return new Booking
        {
            Id = entity.Id,
            TableId = entity.TableId,
            Name = entity.Name,
            Email = entity.Email,
            IsEating = entity.IsEating, 
            Phone = entity.Phone,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Description = entity.Description
        };
    }

    public List<Booking> GetAllBookings()
    {
        var query = _bookingsCollection.AsQueryable<BookingEntity>()
            .Select(entity => new
            {
                entity.Id,
                entity.TableId,
                entity.Name,
                entity.Email,
                entity.IsEating, 
                entity.Phone,
                entity.StartTime,
                entity.EndTime,
                entity.Description
            });

        List<Booking> list = new List<Booking>();
        
        foreach (var booking in query)
        {
            list.Add(new Booking() {Id = booking.Id, TableId = booking.TableId, Name = booking.Name, Email = booking.Email, IsEating = booking.IsEating, Phone = booking.Phone, StartTime = booking.StartTime, EndTime = booking.EndTime, Description = booking.Description});
        }

        return list;
    }

    public List<Booking> GetBookingsByDate(DateTime dateTime)
    {
        var query = _bookingsCollection.Find(Builders<BookingEntity>.Filter.Eq("Date", dateTime.Date)).ToList();
        
        List<Booking> list = new List<Booking>();
        
        foreach (var booking in query)
        {
            list.Add(new Booking() {Id = booking.Id, TableId = booking.TableId, Name = booking.Name, Email = booking.Email, IsEating = booking.IsEating, Phone = booking.Phone, StartTime = booking.StartTime, EndTime = booking.EndTime, Description = booking.Description});
        }
        
        return list;
    }
}