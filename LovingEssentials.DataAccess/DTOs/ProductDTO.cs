namespace LovingEssentials.DataAccess.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
    }
}
