namespace LovingEssentials.DataAccess.DTOs;

public record UserAddressDTO
{
    public int Id { get; set; }
    public string HouseNumber { get; set; }
    public string Street {  get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    
    public UserInfo UserInformation { get; set; }
}

public record UserInfo
{
    public string Name { get; set; }
    public string Email { get; set; }  
    public string PhoneNumber { get; set; }
}