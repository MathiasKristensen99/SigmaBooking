namespace SigmaBooking.Core.Models;

public class TableLayout
{
    public string Id { get; set; }
    public bool IsDefault { get; set; }
    public string Date { get; set; }
    public List<string> TableIds { get; set; }
    public List<Table> Tables { get; set; }
}