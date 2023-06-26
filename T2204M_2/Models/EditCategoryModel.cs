using System.ComponentModel.DataAnnotations;
using T2204M_2.Entities;

namespace T2204M_2.Models
{
    public class EditCategoryModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Vui lòng nhập tối thiểu 3 ký tự")]
        [MaxLength(255)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }


    }
}
