namespace SigmaBooking.WebApi.Dtos;

public class TableLayoutDto
{
    public string Id { get; set; }
    public bool IsDefault { get; set; }
    public string Date { get; set; }
    public List<TableDto> Tables { get; set; }
}