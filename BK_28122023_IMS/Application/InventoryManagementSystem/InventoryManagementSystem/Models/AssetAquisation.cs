using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class AssetAquisation
    {
        [Key]
        public int AAid { get; set; }
        [Display(Name = "Sr.No.")]
        [Required]
        public int SerialNumber { get; set; }
        [Required]
        [ForeignKey("AssetMaster")]
        [DisplayName("AssetName")]
        public int AssetMasterId { get; set; }
        public virtual AssetMaster AssetMaster { get; set; }

        [Required]
        [ForeignKey("VendorMaster")]
        [DisplayName("Make")]
        public int VendorMasterId { get; set; }
        public virtual VendorMaster VendorMaster { get; set; }
        [Required]
        [ForeignKey("Employee")]
        [DisplayName("Employee Name")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [Required]
        [DisplayName("Received Date")]
        public DateTime ReceivedDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Remark { get; set; }
    }
}
