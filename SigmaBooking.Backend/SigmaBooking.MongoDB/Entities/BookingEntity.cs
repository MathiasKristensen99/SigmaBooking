using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SigmaBooking.MongoDB.Entities;

public class BookingEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string TableId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int PeopleCount { get; set; }
    public string Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsEating { get; set; }
    public string Description { get; set; } = null!;
}