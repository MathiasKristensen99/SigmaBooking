﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SigmaBooking.MongoDB.Entities;

public class TableLayoutEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public bool IsDefault { get; set; }
    public DateTime Date { get; set; }
    public string[] TableIds { get; set; }
}