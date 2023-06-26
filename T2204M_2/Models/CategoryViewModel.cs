using System.ComponentModel.DataAnnotations;
namespace T2204M_2.Models
{
    public class CategoryViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Vui lòng nhập tối thiểu 3 ký tự")]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
