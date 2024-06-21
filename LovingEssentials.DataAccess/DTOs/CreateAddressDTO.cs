namespace LovingEssentials.DataAccess.DTOs;

public record CreateAddressDTO
{
    public string HouseNumber { get; set; }
    public string Street {  get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public UserAddress? UserAddress { get; set; }

}

public record UserAddress
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}