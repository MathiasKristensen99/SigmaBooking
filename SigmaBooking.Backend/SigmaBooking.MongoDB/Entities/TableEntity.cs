using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SigmaBooking.MongoDB.Entities;

public class TableEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public bool Static { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int W { get; set; }
    public int H { get; set; }
    public int I { get; set; }
}