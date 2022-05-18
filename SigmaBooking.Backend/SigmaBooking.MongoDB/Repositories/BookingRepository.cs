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
    private readonly IMongoCollection<TableEntity> _tablesCollection;

    public BookingRepository(IOptions<SigmaBookingDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(
            options.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            options.Value.DatabaseName);

        _bookingsCollection = mongoDatabase.GetCollection<BookingEntity>(
            options.Value.BookingsCollectionName);
        
        _tablesCollection = mongoDatabase.GetCollection<TableEntity>(
            options.Value.TablesCollectionName);
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
            PeopleCount = booking.PeopleCount,
            Date = booking.Date,
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
            PeopleCount = bookingEntity.PeopleCount,
            Date = bookingEntity.Date,
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
            PeopleCount = booking.PeopleCount,
            Date = booking.Date,
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
            PeopleCount = booking.PeopleCount,
            Date = booking.Date,
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
            PeopleCount = entity.PeopleCount,
            IsEating = entity.IsEating, 
            Phone = entity.Phone,
            Date = entity.Date,
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
                entity.PeopleCount,
                entity.Phone,
                entity.Date,
                entity.StartTime,
                entity.EndTime,
                entity.Description
            });

        var list = new List<Booking>();
        
        foreach (var booking in query)
        {
            list.Add(new Booking() {Id = booking.Id, TableId = booking.TableId, Name = booking.Name, Email = booking.Email, PeopleCount = booking.PeopleCount, IsEating = booking.IsEating, Phone = booking.Phone, StartTime = booking.StartTime, EndTime = booking.EndTime, Description = booking.Description});
        }

        return list;
    }

    public List<Booking> GetBookingsByDate(string date)
    {
        var query = _bookingsCollection.Find(Builders<BookingEntity>.Filter.Eq("Date", date)).ToList();
        
        var list = new List<Booking>();
        
        foreach (var booking in query)
        {
            var table = _tablesCollection.Find(Builders<TableEntity>.Filter.Eq("_id", ObjectId.Parse(booking.TableId)))
                .FirstOrDefault();
            var newTable = new Table
            {
                Id = table.Id,
                I = table.I,
                X = table.X,
                Y = table.Y,
                W = table.W,
                H = table.H,
                Static = table.Static
            };
            list.Add(new Booking() {Id = booking.Id, TableId = booking.TableId, Table = newTable, Name = booking.Name, Email = booking.Email, PeopleCount = booking.PeopleCount, IsEating = booking.IsEating, Phone = booking.Phone, Date = booking.Date, StartTime = booking.StartTime, EndTime = booking.EndTime, Description = booking.Description});
        }
        
        return list;
    }

    public List<Booking> GetBookingsByTableAndDate(string id, string date)
    {
        var filter = Builders<BookingEntity>.Filter.Or(
            Builders<BookingEntity>.Filter.Where(entity => entity.TableId.Equals(id)),
            Builders<BookingEntity>.Filter.Where(entity => entity.Date.Equals(date)));

        var query = _bookingsCollection.Find(filter).ToList();

        var table = _tablesCollection.Find(Builders<TableEntity>.Filter.Eq("_id", ObjectId.Parse(id)))
            .FirstOrDefault();

        var list = query.Select(booking => new Booking()
            {
                Id = booking.Id,
                TableId = booking.TableId,
                Table = new Table
                {
                    Id = table.Id,
                    X = table.X,
                    Y = table.Y,
                    W = table.W,
                    H = table.H,
                    I = table.I,
                    Static = table.Static
                },
                Name = booking.Name,
                Email = booking.Email,
                IsEating = booking.IsEating,
                PeopleCount = booking.PeopleCount,
                Phone = booking.Phone,
                Date = booking.Date,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Description = booking.Description
            })
            .ToList();

        return list;
    }
}