using SigmaBooking.Core.Models;

namespace SigmaBooking.Core.IServices;

public interface ITableService
{
    Table CreateTable(Table table);

    List<Table> GetAllTables();

    Table UpdateTable(Table table);

    List<Table> UpdateTables(List<Table> tables);

    void DeleteTable(string id);

    Table GetTableById(string id);
}