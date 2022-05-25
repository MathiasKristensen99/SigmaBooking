using System;

namespace SigmaBooking.Core.Models;

public class Booking
{
    public string Id { get; set; }
    public string TableId { get; set; }
    public Table Table { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int PeopleCount { get; set; }
    public string Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public bool IsEating { get; set; }
    public string Description { get; set; }
}