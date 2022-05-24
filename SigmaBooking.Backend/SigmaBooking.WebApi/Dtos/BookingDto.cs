using System;

namespace SigmaBooking.WebApi.Dtos;

public class BookingDto
{
    public string Id { get; set; }
    public TableDto Table { get; set; }
    public string TableId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int PeopleCount { get; set; }
    public string Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsEating { get; set; }
    public string Description { get; set; }
}