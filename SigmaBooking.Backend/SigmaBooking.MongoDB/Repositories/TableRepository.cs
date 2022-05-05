using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.MongoDB.Entities;

namespace SigmaBooking.MongoDB.Repositories;

public class TableRepository : ITableRepository
{
    private readonly IMongoCollection<TableEntity> _tablesCollection;

    public TableRepository(IOptions<SigmaBookingDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(
            options.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            options.Value.DatabaseName);

        _tablesCollection = mongoDatabase.GetCollection<TableEntity>(
            options.Value.TablesCollectionName);
    }

    public Table CreateTable(Table table)
    {
        var tableEntity = new TableEntity
        {
            Id = table.Id,
            Static = table.Static,
            X = table.X,
            Y = table.Y,
            W = table.W,
            H = table.H,
            I = table.I
        };
        
        _tablesCollection.InsertOne(tableEntity);

        return new Table
        {
            Id = tableEntity.Id,
            Static = tableEntity.Static,
            X = tableEntity.X,
            Y = tableEntity.Y,
            W = tableEntity.W,
            H = tableEntity.H,
            I = tableEntity.I
        };
    }

    public List<Table> GetAllTables()
    {
        var query = _tablesCollection.AsQueryable<TableEntity>()
            .Select(entity => new
            {
                entity.Id, entity.Static, entity.X, entity.Y, entity.W, entity.H, entity.I
            });

        var tables = new List<Table>();

        foreach (var table in query)
        {
            tables.Add(new Table()
            {
                Id = table.Id,
                Static = table.Static,
                X = table.X,
                Y = table.Y,
                W = table.W,
                H = table.H,
                I = table.I
            });
        }

        return tables;
    }

    public Table UpdateTable(Table table)
    {
        throw new NotImplementedException();
    }

    public void DeleteTable(string id)
    {
        _tablesCollection.DeleteOne(entity => entity.Id == id);
    }

    public Table GetTableById(string id)
    {
        var table = _tablesCollection.Find(Builders<TableEntity>.Filter.Eq("_id", ObjectId.Parse(id)))
            .FirstOrDefault();

        return new Table
        {
            Id = table.Id,
            Static = table.Static,
            X = table.X,
            Y = table.Y,
            W = table.W,
            H = table.H,
            I = table.I
        };
    }
}