namespace SigmaBooking.Core.Models;

public class Booking
{
    public string Id { get; set; }
    public string TableId { get; set; }

    public Table Table { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsEating { get; set; }
    public string Description { get; set; }
}