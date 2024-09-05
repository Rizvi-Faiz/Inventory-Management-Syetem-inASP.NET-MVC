using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Location
    {
        [Key]
        public int Lid { get; set; }
        [Display(Name = "Location Name")]
        [Required]
        public string LocationName { get; set; }
        [Display(Name = "Location Description")]
        [Required]
        public string LocationDescription { get; set; }
        [Display(Name = "Sr.No.")]
        public int SerialNumber { get; set; }
        [Required]
        public String Status { get; set; }
    }
}
