using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SigmaBooking.MongoDB.Entities;

public class BookingEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime Time { get; set; }
    public string Description { get; set; } = null!;
}