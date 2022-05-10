using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;

namespace SigmaBooking.Domain.Services;

public class TableLayoutService : ITableLayoutService
{
    private readonly ITableLayoutRepository _tableLayoutRepository;

    public TableLayoutService(ITableLayoutRepository repository)
    {
        _tableLayoutRepository = repository;
    }
    
    public TableLayout CreateTableLayout(TableLayout tableLayout)
    {
        return _tableLayoutRepository.CreateTableLayout(tableLayout);
    }

    public TableLayout UpdateTableLayout(TableLayout tableLayout)
    {
        return _tableLayoutRepository.UpdateTableLayout(tableLayout);
    }

    public TableLayout GetTableLayout(DateTime dateTime)
    {
        return _tableLayoutRepository.GetTableLayout(dateTime);
    }

    public void DeleteTableLayout(string id)
    {
        _tableLayoutRepository.DeleteTableLayout(id);
    }
}