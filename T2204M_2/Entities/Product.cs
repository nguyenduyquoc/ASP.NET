using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2204M_2.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; } // abstract property

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Column(TypeName = "text")]
        public String Description { get; set; }

        [Column(TypeName = "nvarchar(max)")]

        [Required]
        public string Image { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
