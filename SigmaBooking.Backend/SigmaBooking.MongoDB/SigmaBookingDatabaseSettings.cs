namespace SigmaBooking.MongoDB;

public class SigmaBookingDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string BookingsCollectionName { get; set; } = null!;
    public string TablesCollectionName { get; set; } = null!;
}