namespace SigmaBooking.WebApi.Dtos;

public class CreateTableLayoutDto
{
    public string Id { get; set; }
    public bool IsDefault { get; set; }
    public string Date { get; set; }
    public string[] TableIds { get; set; }
}