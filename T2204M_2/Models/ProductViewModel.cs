using System.ComponentModel.DataAnnotations;
using T2204M_2.Entities;

namespace T2204M_2.Models
{
    public class ProductViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Vui lòng nhập tối thiểu 2 ký tự")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Chỉ cho phép nhập chữ số")]
        public double Price { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Vui lòng nhập tối thiểu 6 ký tự")]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
