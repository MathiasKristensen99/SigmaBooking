﻿using Microsoft.Extensions.Options;
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
        var tableLayoutEntity = new TableLayoutEntity
        {
            Id = tableLayout.Id,
            IsDefault = tableLayout.IsDefault,
            Date = tableLayout.Date,
            TableIds = tableLayout.TableIds
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
        throw new NotImplementedException();
    }

    public TableLayout GetTableLayout(DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    public void DeleteTableLayout(string id)
    {
        throw new NotImplementedException();
    }
}