using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;

namespace SigmaBooking.Domain.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository repository)
    {
        _tableRepository = repository;
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