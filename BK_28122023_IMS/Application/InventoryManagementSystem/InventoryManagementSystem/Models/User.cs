using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Sr.No.")]
        public int SerialNumber { get; set; }
        [Required]
        public int UserId { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Invalid Email")]
        [Required]
        public string Email { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        public string Status { get; set; }
        public string by { get; set; }
        public DateTime when { get; set; }
    }
}
