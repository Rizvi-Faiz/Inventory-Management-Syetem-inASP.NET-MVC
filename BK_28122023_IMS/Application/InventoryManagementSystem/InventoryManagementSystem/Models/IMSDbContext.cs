//using Microsoft.EntityFrameworkCore;

//namespace InventoryManagementSystem.Models
//{
//    public class IMSDbContext : DbContext
//    {
//        public IMSDbContext(DbContextOptions options):base(options)
//        {

//        }
//        public DbSet<User>Users { get; set; }
//        public DbSet<Designation>Designation { get; set; }
//        public DbSet<Employee>Employee { get; set; }
//    }
//}

using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Models
{
    public class IMSDbContext : DbContext
    {
        public IMSDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<AssetMaster> AssetMaster { get; set; }
        public DbSet<VendorMaster> VendorMaster { get; set; }
        public DbSet<AssetProcurement> AssetProcurement { get; set; }
        public DbSet<AssetDeployement> AssetDeployement { get; set; }
        public DbSet<AssetAquisation> AssetAquisation { get; set; }


       
    }
}

