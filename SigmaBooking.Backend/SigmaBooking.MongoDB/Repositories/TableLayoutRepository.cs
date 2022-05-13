using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.MongoDB.Entities;

namespace SigmaBooking.MongoDB.Repositories;

public class TableLayoutRepository : ITableLayoutRepository
{
    private readonly IMongoCollection<TableLayoutEntity> _tablesLayoutCollection;
    private readonly IMongoCollection<TableEntity> _tablesCollection;

    public TableLayoutRepository(IOptions<SigmaBookingDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(
            options.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            options.Value.DatabaseName);

        _tablesLayoutCollection = mongoDatabase.GetCollection<TableLayoutEntity>(
            options.Value.TableLayoutsCollectionName);

        _tablesCollection = mongoDatabase.GetCollection<TableEntity>(
            options.Value.TablesCollectionName);
    }
    
    public TableLayout CreateTableLayout(TableLayout tableLayout)
    {
        var tableIds = new List<string>();
        
        foreach (var table in tableLayout.Tables)
        {
            tableIds.Add(table.Id);
        }

        var tableLayoutEntity = new TableLayoutEntity
        {
            Id = tableLayout.Id,
            IsDefault = tableLayout.IsDefault,
            Date = tableLayout.Date,
            TableIds = tableIds,
        };
        
        _tablesLayoutCollection.InsertOne(tableLayoutEntity);

        var tables = new List<Table>();
        
        foreach (var tableId in tableLayoutEntity.TableIds)
        {
            var table = _tablesCollection.Find(Builders<TableEntity>.Filter.Eq("_id", ObjectId.Parse(tableId)))
                .FirstOrDefault();
            
            tables.Add(new Table
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

        return new TableLayout
        {
            Id = tableLayoutEntity.Id,
            IsDefault = tableLayoutEntity.IsDefault,
            Date = tableLayoutEntity.Date,
            TableIds = tableLayoutEntity.TableIds,
            Tables = tables
        };
    }

    public TableLayout UpdateTableLayout(TableLayout tableLayout)
    {
        var filter = Builders<TableLayoutEntity>.Filter.Eq("_id", ObjectId.Parse(tableLayout.Id));

        var tableIds = tableLayout.Tables.Select(table => table.Id).ToList();

        var entity = new TableLayoutEntity
        {
            Id = tableLayout.Id,
            Date = tableLayout.Date,
            IsDefault = tableLayout.IsDefault,
            TableIds = tableIds
        };

        _tablesLayoutCollection.ReplaceOne(filter, entity);
        
        var tables = entity.TableIds.Select(tableId => _tablesCollection.Find(Builders<TableEntity>.Filter.Eq("_id", ObjectId.Parse(tableId)))
                .FirstOrDefault())
            .Select(table => new Table
            {
                Id = table.Id,
                Static = table.Static,
                X = table.X,
                Y = table.Y,
                W = table.W,
                H = table.H,
                I = table.I
            })
            .ToList();

        return new TableLayout
        {
            Id = entity.Id,
            Date = entity.Date,
            IsDefault = entity.IsDefault,
            Tables = tables,
            TableIds = entity.TableIds
        };
    }

    public TableLayout GetTableLayout(string date)
    {
        var tableLayout = _tablesLayoutCollection.Find(Builders<TableLayoutEntity>.Filter.Eq("Date", date))
            .FirstOrDefault();
        
        var tables = new List<Table>();

        if (tableLayout != null)
        {
            tables.AddRange(tableLayout.TableIds.Select(tableId => _tablesCollection.Find(Builders<TableEntity>.Filter.Eq("_id", ObjectId.Parse(tableId)))
                    .FirstOrDefault())
                .Select(table => new Table
                {
                    Id = table.Id,
                    Static = table.Static,
                    X = table.X,
                    Y = table.Y,
                    W = table.W,
                    H = table.H,
                    I = table.I
                }));

            return new TableLayout
            {
                Id = tableLayout.Id,
                Date = tableLayout.Date,
                IsDefault = tableLayout.IsDefault,
                TableIds = tableLayout.TableIds,
                Tables = tables
            };
        }
        else
        {
            var defaultTableLayout = _tablesLayoutCollection.Find(Builders<TableLayoutEntity>.Filter.Eq("IsDefault", true))
                .FirstOrDefault();

            tables.AddRange(defaultTableLayout.TableIds.Select(tableId => _tablesCollection.Find(Builders<TableEntity>.Filter.Eq("_id", ObjectId.Parse(tableId)))
                    .FirstOrDefault())
                .Select(table => new Table
                {
                    Id = table.Id,
                    Static = table.Static,
                    X = table.X,
                    Y = table.Y,
                    W = table.W,
                    H = table.H,
                    I = table.I
                }));

            return new TableLayout
            {
                Id = defaultTableLayout.Id,
                IsDefault = defaultTableLayout.IsDefault,
                Date = defaultTableLayout.Date,
                TableIds = defaultTableLayout.TableIds,
                Tables = tables
            };
        }
    }

    public void DeleteTableLayout(string id)
    {
        throw new NotImplementedException();
    }
}