using SigmaBooking.Core.Models;

namespace SigmaBooking.Core.IServices;

public interface ITableLayoutService
{
    TableLayout CreateTableLayout(TableLayout tableLayout);

    TableLayout UpdateTableLayout(TableLayout tableLayout);

    TableLayout GetTableLayout(string date);

    void DeleteTableLayout(string id);
}