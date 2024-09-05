using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;



namespace InventoryManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int Eid { get; set; }
        [Column("Sr.No.")]
        [Display(Name = "Sr.No.")]
        public int Serialnumber { get; set; }
        //[Required(ErrorMessage ="Empid")]
        public int? EmpId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Status { get; set; }

        [Required]
        [ForeignKey("Designation")]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        public virtual Designation Designation { get; set; }
        [Required]
        [ForeignKey("Department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Required]
        [ForeignKey("Location")]
        [DisplayName("Location")]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        [Required]

        public DateTime DateOfBirth { get; set; }
        [Required]

        public DateTime DateOfJoining { get; set; }
    }
}
