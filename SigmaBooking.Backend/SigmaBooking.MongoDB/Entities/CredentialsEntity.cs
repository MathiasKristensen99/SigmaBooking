using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SigmaBooking.MongoDB.Entities;

public class CredentialsEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }

    public string credentials { get; set; }
}