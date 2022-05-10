using SigmaBooking.Core.Models;

namespace SigmaBooking.Core.IServices;

public interface ITableLayoutService
{
    TableLayout CreateTableLayout(TableLayout tableLayout);

    TableLayout UpdateTableLayout(TableLayout tableLayout);

    TableLayout GetTableLayout(DateTime dateTime);

    void DeleteTableLayout(string id);
}