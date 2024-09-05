using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Designation
    {

        [Key]
        public int Did { get; set; }
        [Required]
        [Display(Name = "Designation Name")]
        public string DesignationName { get; set; }
        [Required]
        [Display(Name = "Designation Description")]
        public string DesignaionDescription { get; set; }
        
        [Display(Name = "Sr.No.")]
        public int SerialNumber { get; set; }
        [Required]
        public String Status { get; set; }
    }
}
