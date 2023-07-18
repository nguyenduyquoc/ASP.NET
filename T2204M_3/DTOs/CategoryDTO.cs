namespace T2204M_3.DTOs
{
    public class CategoryDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        public List<ProductDTO>? products { get; set; }
    }
}
