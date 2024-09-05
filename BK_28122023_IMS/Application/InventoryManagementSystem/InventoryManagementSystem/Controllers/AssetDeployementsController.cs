using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    public class AssetDeployementsController : Controller
    {
        private readonly IMSDbContext _context;

        public AssetDeployementsController(IMSDbContext context)
        {
            _context = context;
        }

        // GET: AssetDeployements
        public async Task<IActionResult> Index()
        {
            for (int i = 0; i < _context.AssetDeployement.ToList().Count; i++)
            {
                _context.AssetDeployement.ToList()[i].SerialNumber = i + 1;
            }
            var iMSDbContext = _context.AssetDeployement.Include(a => a.AssetMaster).Include(a => a.Department).Include(a => a.Employee).Include(a => a.VendorMaster);
            return View(await iMSDbContext.ToListAsync());
        }
        //public async Task<IActionResult> IndexReport(DateTime? startDate, DateTime? endDate)
        //{
        //    for (int i = 0; i < _context.AssetDeployement.ToList().Count; i++)
        //    {
        //        _context.AssetDeployement.ToList()[i].SerialNumber = i + 1;
        //    }
        //    IQueryable<AssetDeployement> assetDeployments = _context.AssetDeployement.Include(a => a.AssetMaster).Include(a => a.Department).Include(a => a.Employee).Include(a => a.VendorMaster);

        //    if (startDate.HasValue)
        //    {
        //        assetDeployments = assetDeployments.Where(a => a.IssueDate >= startDate.Value);
        //    }

        //    if (endDate.HasValue)
        //    {
        //        assetDeployments = assetDeployments.Where(a => a.IssueDate <= endDate.Value);
        //    }

        //    return View(await assetDeployments.ToListAsync());
        //}
        public async Task<IActionResult> IndexReport(DateTime? startDate, DateTime? endDate, int? employeeId, int? departmentId, string status)
        {
            for (int i = 0; i < _context.AssetDeployement.ToList().Count; i++)
            {
                _context.AssetDeployement.ToList()[i].SerialNumber = i + 1;
            }

            IQueryable<AssetDeployement> assetDeployments = _context.AssetDeployement
                .Include(a => a.AssetMaster)
                .Include(a => a.Department)
                .Include(a => a.Employee)
                .Include(a => a.VendorMaster);

            if (startDate.HasValue)
            {
                assetDeployments = assetDeployments.Where(a => a.IssueDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                assetDeployments = assetDeployments.Where(a => a.IssueDate <= endDate.Value);
            }

            if (employeeId.HasValue)
            {
                assetDeployments = assetDeployments.Where(a => a.EmployeeId == employeeId);
            }

            if (departmentId.HasValue)
            {
                assetDeployments = assetDeployments.Where(a => a.DepartmentId == departmentId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                assetDeployments = assetDeployments.Where(a => a.Status == status);
            }
            ViewBag.Employees = _context.Employee.ToList();
            ViewBag.Departments = _context.Department.ToList();
            return View(await assetDeployments.ToListAsync());
        }


        // GET: AssetDeployements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetDeployement = await _context.AssetDeployement
                .Include(a => a.AssetMaster)
                .Include(a => a.Department)
                .Include(a => a.Employee)
                .Include(a => a.VendorMaster)
                .FirstOrDefaultAsync(m => m.ADid == id);
            if (assetDeployement == null)
            {
                return NotFound();
            }

            return View(assetDeployement);
        }

        // GET: AssetDeployements/Create
        private List<SelectListItem> GetVendorMaster()
        {
            var lstVendorMaster = new List<SelectListItem>();
            List<VendorMaster> VendorMasters = _context.VendorMaster.ToList();
            var defItem = new SelectListItem()
            {
                Text = "",
                Value = ""
            };
            lstVendorMaster = VendorMasters.Select(d => new SelectListItem()
            {
                Text = d.VendorName,
                Value = d.VMid.ToString()
            }).ToList();
            lstVendorMaster.Add(defItem);

            return lstVendorMaster;
        }
        private List<SelectListItem> GetAssetMaster()
        {
            var lstAssetMaster = new List<SelectListItem>();
            List<AssetMaster> AssetMasters = _context.AssetMaster.ToList();
            var defItem = new SelectListItem()
            {
                Text = "",
                Value = ""
            };
            lstAssetMaster = AssetMasters.Select(d => new SelectListItem()
            {
                Text = d.AssetName,
                Value = d.AMid.ToString()
            }).ToList();
            lstAssetMaster.Add(defItem);

            return lstAssetMaster;
        }
        private List<SelectListItem> GetAssetMasterModel()
        {
            var lstAssetMaster = new List<SelectListItem>();
            List<AssetMaster> AssetMasters = _context.AssetMaster.ToList();
            var defItem = new SelectListItem()
            {
                Text = "",
                Value = ""
            };
            lstAssetMaster = AssetMasters.Select(d => new SelectListItem()
            {
                Text = d.AssetModel,
                Value = d.AMid.ToString()
            }).ToList();
            lstAssetMaster.Add(defItem);

            return lstAssetMaster;
        }
        private List<SelectListItem> GetDepartment()
        {
            var lstDepartment = new List<SelectListItem>();
            List<Department> Departments = _context.Department.ToList();
            var defItem = new SelectListItem()
            {
                Text = "",
                Value = ""
            };
            lstDepartment = Departments.Select(d => new SelectListItem()
            {
                Text = d.DepartmentName,
                Value = d.Depid.ToString()
            }).ToList();
            lstDepartment.Add(defItem);

            return lstDepartment;
        }
        private List<SelectListItem> GetEmployee()
        {
            var lstEmployee = new List<SelectListItem>();
            List<Employee> Employees = _context.Employee.ToList();
            var defItem = new SelectListItem()
            {
                Text = "",
                Value = ""
            };
            lstEmployee = Employees.Select(d => new SelectListItem()
            {
                Text = d.FirstName,
                Value = d.Eid.ToString()
            }).ToList();
            lstEmployee.Add(defItem);

            return lstEmployee;
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> Status = new()
            {
                new SelectListItem{Value="In Progress",Text="In Progress"},
                new SelectListItem{Value="Close",Text="Close"},
                new SelectListItem{Value="In Part",Text="In Part"},
            };
            ViewBag.Status = Status;
            AssetDeployement assetDeployement = new AssetDeployement();
            ViewBag.VendorMasterId = GetVendorMaster();
            ViewBag.AssetMasterId = GetAssetMaster();
            ViewBag.AssetMasterModelId = GetAssetMasterModel();
            ViewBag.EmployeeId = GetEmployee();
            ViewBag.DepartmentId = GetDepartment();

            return View(assetDeployement);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(AssetDeployement assetDeployement)
        {
            _context.Add(assetDeployement);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: AssetDeployements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<SelectListItem> Status = new()
            {
                new SelectListItem{Value="In Progress",Text="In Progress"},
                new SelectListItem{Value="Close",Text="Close"},
                new SelectListItem{Value="In Part",Text="In Part"},
            };
            ViewBag.Status = Status;
            var assetDeployement = await _context.AssetDeployement.FindAsync(id);
            if (assetDeployement == null)
            {
                return NotFound();
            }
            ViewBag.VendorMasterId = GetVendorMaster();
            ViewBag.AssetMasterId = GetAssetMaster();
            ViewBag.AssetMasterModelId = GetAssetMasterModel();
            ViewBag.EmployeeId = GetEmployee();
            ViewBag.DepartmentId = GetDepartment();
            return View(assetDeployement);
        }

        // POST: AssetDeployements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ADid,SerialNumber,AssetMasterId,VendorMasterId,EmployeeId,IssueDate,DepartmentId,Status,Remark")] AssetDeployement assetDeployement)
        {
            //if (id != assetDeployement.ADid)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            _context.Update(assetDeployement);
            await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AssetDeployementExists(assetDeployement.ADid))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            return RedirectToAction(nameof(Index));
        //}
            
            ViewData["AssetMasterId"] = new SelectList(_context.AssetMaster, "AMid", "AssetDescription", assetDeployement.AssetMasterId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Depid", "Status", assetDeployement.DepartmentId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Eid", "Email", assetDeployement.EmployeeId);
            ViewData["VendorMasterId"] = new SelectList(_context.VendorMaster, "VMid", "VendorDescription", assetDeployement.VendorMasterId);
            return View(assetDeployement);
        }

        // GET: AssetDeployements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetDeployement = await _context.AssetDeployement
                .Include(a => a.AssetMaster)
                .Include(a => a.Department)
                .Include(a => a.Employee)
                .Include(a => a.VendorMaster)
                .FirstOrDefaultAsync(m => m.ADid == id);
            if (assetDeployement == null)
            {
                return NotFound();
            }

            return View(assetDeployement);
        }

        // POST: AssetDeployements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetDeployement = await _context.AssetDeployement.FindAsync(id);
            if (assetDeployement != null)
            {
                _context.AssetDeployement.Remove(assetDeployement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetDeployementExists(int id)
        {
            return _context.AssetDeployement.Any(e => e.ADid == id);
        }
    }
}
