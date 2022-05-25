
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.MongoDB.Entities;

namespace SigmaBooking.MongoDB.Repositories;

public class CredentialsRepository : ICredentialsRepository
{
    private readonly IMongoCollection<CredentialsEntity> _credentialsCollection;

    public CredentialsRepository(IOptions<SigmaBookingDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

        _credentialsCollection = mongoDatabase.GetCollection<CredentialsEntity>(
            options.Value.CredentialsCollectionName);
    }

    public CredentialsModel CreateCredentials(CredentialsModel credentialsModel)
    {
        var credentialsEntity = new CredentialsEntity
        {
            Id = credentialsModel.Id,
            credentials = credentialsModel.Credentials
        };
        _credentialsCollection.InsertOne(credentialsEntity);

        return new CredentialsModel
        {
            Id = credentialsEntity.Id,
            Credentials = credentialsEntity.credentials
        };
    }

    public CredentialsModel GetCredentials(string id)
    {
        var credentials = _credentialsCollection
            .Find(Builders<CredentialsEntity>.Filter
                .Eq("_id", ObjectId.Parse(id)))
            .FirstOrDefault();
        return new CredentialsModel
        {
            Id = credentials.Id,
            Credentials = credentials.credentials
        };
    }

    public void DeleteCredentials(string id)
    {
        _credentialsCollection.DeleteOne(ent => ent.Id == id);
    }
}