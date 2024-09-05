using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace InventoryManagementSystem.Models
{
    public class VendorMaster
    {
        [Key]
        public int VMid { get; set; }
        [Display(Name = "Sr.No.")]
        public int SerialNumber { get; set; }
        [Required]

        public string VendorName { get; set; }
        [Required]
        public string VendorDescription { get; set; }
    }
}
