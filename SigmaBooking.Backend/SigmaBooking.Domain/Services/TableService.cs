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
        return _tableRepository.CreateTable(table);
    }

    public List<Table> GetAllTables()
    {
        return _tableRepository.GetAllTables();
    }

    public Table UpdateTable(Table table)
    {
        return _tableRepository.UpdateTable(table);
    }

    public List<Table> UpdateTables(List<Table> tables)
    {
        return _tableRepository.UpdateTables(tables);
    }

    public void DeleteTable(string id)
    {
        _tableRepository.DeleteTable(id);
    }

    public Table GetTableById(string id)
    {
        return _tableRepository.GetTableById(id);
    }
}