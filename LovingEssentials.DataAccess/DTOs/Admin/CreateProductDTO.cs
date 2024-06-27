namespace LovingEssentials.DataAccess.DTOs.Admin
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public byte Status { get; set; }
    }
}
