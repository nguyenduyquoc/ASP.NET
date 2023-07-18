using System.ComponentModel.DataAnnotations;

namespace T2204M_Practice.Models
{
    public class ContaxtViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Vui lòng nhập tối thiểu 2 ký tự")]
        [MaxLength(255)]
        public string ContactName { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Chỉ cho phép nhập chữ số")]
        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string GroupName { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}

