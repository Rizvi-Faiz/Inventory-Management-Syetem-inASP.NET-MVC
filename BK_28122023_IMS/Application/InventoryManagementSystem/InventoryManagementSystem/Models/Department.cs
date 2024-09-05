using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Department
    {
        [Key]
        public int Depid { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Display(Name = "Department Description")]
        public string DepartmentDescription { get; set; }
        [Display(Name = "Sr.No.")]
        public int SerialNumber { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
