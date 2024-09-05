using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class AssetMaster
    {
        [Key]
        public int AMid { get; set; }
        [Display(Name = "Sr.No.")]
        public int SerialNumber { get; set; }
        [Required]
        public string AssetName { get; set; }
        [Required]
        public string AssetModel { get; set; }
        [Required]
        public string AssetDescription { get; set; }
    }
}
