namespace T2204M_3.DTOs
{
    public class ProductDTO
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string? thumbnail { get; set; }
        public decimal price { get; set; }
        public int qty { get; set; }
        public string? description { get; set; }
        public DateTime? createdAt { get; set;}
        public CategoryDTO category { get; set; }
        public BrandDTO brand { get; set; }
    }
}
