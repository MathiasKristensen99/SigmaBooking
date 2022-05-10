using SigmaBooking.Core.Models;

namespace SigmaBooking.Domain.IRepositories;

public interface ITableLayoutRepository
{
    TableLayout CreateTableLayout(TableLayout tableLayout);

    TableLayout UpdateTableLayout(TableLayout tableLayout);

    TableLayout GetTableLayout(DateTime dateTime);

    void DeleteTableLayout(string id);
}