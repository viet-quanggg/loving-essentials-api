namespace LovingEssentials.DataAccess.DTOs;

public class UpdateAddressDto
{
    public int Id { get; set; }
    public string HouseNumber { get; set; }
    public string Street {  get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string City { get; set; }
}