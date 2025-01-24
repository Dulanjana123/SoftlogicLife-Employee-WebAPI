using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EPF number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "EPF number must be greater than 0.")]
        public int Epf { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\+94\d{9}$", ErrorMessage = "Mobile number must be in the format +947XXXXXXXX.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
