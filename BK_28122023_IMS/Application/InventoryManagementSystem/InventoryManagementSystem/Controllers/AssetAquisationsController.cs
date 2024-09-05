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
    public class AssetAquisationsController : Controller
    {
        private readonly IMSDbContext _context;

        public AssetAquisationsController(IMSDbContext context)
        {
            _context = context;
        }

        // GET: AssetAquisations
        public async Task<IActionResult> Index()
        {
            for (int i = 0; i < _context.AssetAquisation.ToList().Count; i++)
            {
                _context.AssetAquisation.ToList()[i].SerialNumber = i + 1;
            }
            var iMSDbContext = _context.AssetAquisation.Include(a => a.AssetMaster).Include(a => a.Employee).Include(a => a.VendorMaster);
            return View(await iMSDbContext.ToListAsync());
        }
        //public async Task<IActionResult> IndexReport(DateTime? startDate, DateTime? endDate)
        //{
        //    for (int i = 0; i < _context.AssetAquisation.ToList().Count; i++)
        //    {
        //        _context.AssetAquisation.ToList()[i].SerialNumber = i + 1;
        //    }
        //    IQueryable<AssetAquisation> assetAquisations = _context.AssetAquisation.Include(a => a.AssetMaster).Include(a => a.Employee).Include(a => a.VendorMaster);

        //    if (startDate.HasValue)
        //    {
        //        assetAquisations = assetAquisations.Where(a => a.ReceivedDate >= startDate.Value);
        //    }

        //    if (endDate.HasValue)
        //    {
        //        assetAquisations = assetAquisations.Where(a => a.ReceivedDate <= endDate.Value);
        //    }

        //    return View(await assetAquisations.ToListAsync());
        //}
        public async Task<IActionResult> IndexReport(DateTime? startDate, DateTime? endDate, int? employeeId, string status)
        {
            for (int i = 0; i < _context.AssetAquisation.ToList().Count; i++)
            {
                _context.AssetAquisation.ToList()[i].SerialNumber = i + 1;
            }

            IQueryable<AssetAquisation> assetAcquisitions = _context.AssetAquisation
                .Include(a => a.AssetMaster)
                .Include(a => a.Employee)
                .Include(a => a.VendorMaster);

            // Filter by Start Date
            if (startDate.HasValue)
            {
                assetAcquisitions = assetAcquisitions.Where(a => a.ReceivedDate >= startDate.Value);
            }

            // Filter by End Date
            if (endDate.HasValue)
            {
                assetAcquisitions = assetAcquisitions.Where(a => a.ReceivedDate <= endDate.Value);
            }

            // Filter by Employee
            if (employeeId.HasValue)
            {
                assetAcquisitions = assetAcquisitions.Where(a => a.EmployeeId == employeeId);
            }

            // Filter by Status
            if (!string.IsNullOrEmpty(status))
            {
                assetAcquisitions = assetAcquisitions.Where(a => a.Status == status);
            }

            ViewBag.Employees = _context.Employee.ToList();

            return View(await assetAcquisitions.ToListAsync());
        }


        // GET: AssetAquisations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAquisation = await _context.AssetAquisation
                .Include(a => a.AssetMaster)
                .Include(a => a.Employee)
                .Include(a => a.VendorMaster)
                .FirstOrDefaultAsync(m => m.AAid == id);
            if (assetAquisation == null)
            {
                return NotFound();
            }

            return View(assetAquisation);
        }

        // GET: AssetAquisations/Create
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
            AssetAquisation assetAquisation = new AssetAquisation();
            ViewBag.VendorMasterId = GetVendorMaster();
            ViewBag.AssetMasterId = GetAssetMaster();
            ViewBag.AssetMasterModelId = GetAssetMasterModel();
            ViewBag.EmployeeId = GetEmployee();
            ViewBag.DepartmentId = GetDepartment();

            return View(assetAquisation);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(AssetAquisation assetAquisation)
        {
            _context.Add(assetAquisation);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AssetAquisations/Edit/5
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
            var assetAquisation = await _context.AssetAquisation.FindAsync(id);
            if (assetAquisation == null)
            {
                return NotFound();
            }
            ViewBag.VendorMasterId = GetVendorMaster();
            ViewBag.AssetMasterId = GetAssetMaster();
            ViewBag.AssetMasterModelId = GetAssetMasterModel();
            ViewBag.EmployeeId = GetEmployee();
            ViewBag.DepartmentId = GetDepartment();
            return View(assetAquisation);
        }

        // POST: AssetAquisations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AAid,SerialNumber,AssetMasterId,VendorMasterId,EmployeeId,ReceivedDate,Status,Remark")] AssetAquisation assetAquisation)
        {
            if (id != assetAquisation.AAid)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
                     _context.Update(assetAquisation);
                    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!AssetAquisationExists(assetAquisation.AAid))
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
            ViewData["AssetMasterId"] = new SelectList(_context.AssetMaster, "AMid", "AssetDescription", assetAquisation.AssetMasterId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Eid", "Email", assetAquisation.EmployeeId);
            ViewData["VendorMasterId"] = new SelectList(_context.VendorMaster, "VMid", "VendorDescription", assetAquisation.VendorMasterId);
            return View(assetAquisation);
        }

        // GET: AssetAquisations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAquisation = await _context.AssetAquisation
                .Include(a => a.AssetMaster)
                .Include(a => a.Employee)
                .Include(a => a.VendorMaster)
                .FirstOrDefaultAsync(m => m.AAid == id);
            if (assetAquisation == null)
            {
                return NotFound();
            }

            return View(assetAquisation);
        }

        // POST: AssetAquisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetAquisation = await _context.AssetAquisation.FindAsync(id);
            if (assetAquisation != null)
            {
                _context.AssetAquisation.Remove(assetAquisation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetAquisationExists(int id)
        {
            return _context.AssetAquisation.Any(e => e.AAid == id);
        }
    }
}
