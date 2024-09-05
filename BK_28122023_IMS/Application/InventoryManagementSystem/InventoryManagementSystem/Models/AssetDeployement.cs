using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class AssetDeployement
    {
        [Key]
        public int ADid { get; set; }
        [Display(Name = "Sr.No.")]
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
        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }
        [Required]
        [ForeignKey("Department")]
        [DisplayName("Department Name")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        public string Status { get; set; }
        [Required]
        public string Remark { get; set; }

    }
}
