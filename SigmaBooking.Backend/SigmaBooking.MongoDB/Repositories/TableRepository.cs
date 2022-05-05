using Microsoft.Extensions.Options;
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
        throw new NotImplementedException();
    }

    public List<Table> GetAllTables()
    {
        throw new NotImplementedException();
    }

    public Table UpdateTable(Table table)
    {
        throw new NotImplementedException();
    }

    public void DeleteTable(string id)
    {
        throw new NotImplementedException();
    }
}