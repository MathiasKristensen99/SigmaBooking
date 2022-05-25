
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SigmaBooking.Core.Models;
using SigmaBooking.MongoDB.Entities;

namespace SigmaBooking.MongoDB.Repositories;

public class CredentialsRepository
{
    private readonly IMongoCollection<CredentialsEntity> _credentialsCollection;

    public CredentialsRepository(IOptions<SigmaBookingDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

        _credentialsCollection = mongoDatabase.GetCollection<CredentialsEntity>(
            options.Value.BookingsCollectionName);
    }

    public Credentials CreateCredentials(Credentials credentials)
    {
        var credentialsEntity = new CredentialsEntity
        {
            Id = credentials.Id,
            credentials = credentials.credentials
        };
        _credentialsCollection.InsertOne(credentialsEntity);

        return new Credentials
        {
            Id = credentialsEntity.Id,
            credentials = credentialsEntity.credentials
        };
    }
}