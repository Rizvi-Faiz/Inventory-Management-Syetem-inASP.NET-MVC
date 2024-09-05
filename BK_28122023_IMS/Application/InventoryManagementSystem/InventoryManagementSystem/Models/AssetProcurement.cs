    using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class AssetProcurement
    {
        [Key]
        public int APid { get; set; }
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
        [DisplayName("Purchase Order")]
        public int PurchaseOrder { get; set; }

        [Required]
        [DisplayName("Purchase Date")]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public int? QuotationNumber { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]

        public string Remark { get; set; }

    }   

}
