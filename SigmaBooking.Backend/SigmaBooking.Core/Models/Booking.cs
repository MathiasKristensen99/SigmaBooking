namespace SigmaBooking.Core.Models;

public class Booking
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public DateTime Time { get; set; }
    public string Description { get; set; }
}