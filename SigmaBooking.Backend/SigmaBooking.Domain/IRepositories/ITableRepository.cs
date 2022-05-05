using SigmaBooking.Core.Models;

namespace SigmaBooking.Domain.IRepositories;

public interface ITableRepository
{
    Table CreateTable(Table table);

    List<Table> GetAllTables();

    Table UpdateTable(Table table);

    void DeleteTable(string id);
}