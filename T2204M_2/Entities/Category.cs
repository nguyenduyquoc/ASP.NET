using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2204M_2.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; } // abstract property

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
