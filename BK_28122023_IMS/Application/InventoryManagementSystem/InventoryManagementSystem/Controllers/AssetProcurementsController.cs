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
    public class AssetProcurementsController : Controller
    {
        private readonly IMSDbContext _context;

        public AssetProcurementsController(IMSDbContext context)
        {
            _context = context;
        }

        // GET: AssetProcurements
        public async Task<IActionResult> Index()
        {
            for (int i = 0; i < _context.AssetProcurement.ToList().Count; i++)
            {
                _context.AssetProcurement.ToList()[i].SerialNumber = i + 1;
            }

            var iMSDbContext = _context.AssetProcurement.Include(a => a.AssetMaster).Include(a => a.VendorMaster);
            return View(await iMSDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexReport(DateTime? startDate, DateTime? endDate, int? purchaseOrder, int? quotationNumber, DateTime? deliveryDateStart, DateTime? deliveryDateEnd)
        {
            for (int i = 0; i < _context.AssetProcurement.ToList().Count; i++)
            {
                _context.AssetProcurement.ToList()[i].SerialNumber = i + 1;
            }

            IQueryable<AssetProcurement> assetProcurements = _context.AssetProcurement.Include(a => a.AssetMaster).Include(a => a.VendorMaster);

            if (startDate.HasValue)
            {
                assetProcurements = assetProcurements.Where(a => a.PurchaseDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                assetProcurements = assetProcurements.Where(a => a.PurchaseDate <= endDate.Value);
            }

            if (purchaseOrder.HasValue)
            {
                assetProcurements = assetProcurements.Where(a => a.PurchaseOrder == purchaseOrder.Value);
            }

            if (quotationNumber.HasValue)
            {
                assetProcurements = assetProcurements.Where(a => a.QuotationNumber == quotationNumber.Value);
            }

            if (deliveryDateStart.HasValue)
            {
                assetProcurements = assetProcurements.Where(a => a.DeliveryDate >= deliveryDateStart.Value);
            }

            if (deliveryDateEnd.HasValue)
            {
                assetProcurements = assetProcurements.Where(a => a.DeliveryDate <= deliveryDateEnd.Value);
            }

            return View(await assetProcurements.ToListAsync());
        }



        // GET: AssetProcurements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetProcurement = await _context.AssetProcurement
                .Include(a => a.AssetMaster)
                .Include(a => a.VendorMaster)
                .FirstOrDefaultAsync(m => m.APid == id);
            if (assetProcurement == null)
            {
                return NotFound();
            }

            return View(assetProcurement);
        }

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
            AssetProcurement assetProcurement = new AssetProcurement();
            ViewBag.VendorMasterId = GetVendorMaster();
            ViewBag.AssetMasterId = GetAssetMaster();
            ViewBag.AssetMasterModelId = GetAssetMasterModel();
            ViewBag.EmployeeId = GetEmployee();
            ViewBag.DepartmentId = GetDepartment();

            return View(assetProcurement);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(AssetProcurement assetProcurement)
        {
            _context.Add(assetProcurement);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AssetProcurements/Edit/5
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
            var assetProcurement = await _context.AssetProcurement.FindAsync(id);
            if (assetProcurement == null)
            {
                return NotFound();
            }
            ViewBag.VendorMasterId = GetVendorMaster();
            ViewBag.AssetMasterId = GetAssetMaster();
            ViewBag.AssetMasterModelId = GetAssetMasterModel();
            ViewBag.EmployeeId = GetEmployee();
            ViewBag.DepartmentId = GetDepartment();
            return View(assetProcurement);
        }

        // POST: AssetProcurements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("APid,SerialNumber,AssetMasterId,VendorMasterId,PurchaseOrder,PurchaseDate,QuotationNumber,Quantity,DeliveryDate,Status,Remark")] AssetProcurement assetProcurement)
        {
            if (id != assetProcurement.APid)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
                    _context.Update(assetProcurement);
                    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!AssetProcurementExists(assetProcurement.APid))
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
            ViewData["AssetMasterId"] = new SelectList(_context.AssetMaster, "AMid", "AssetDescription", assetProcurement.AssetMasterId);
            ViewData["VendorMasterId"] = new SelectList(_context.VendorMaster, "VMid", "VendorDescription", assetProcurement.VendorMasterId);
            return View(assetProcurement);
        }

        // GET: AssetProcurements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetProcurement = await _context.AssetProcurement
                .Include(a => a.AssetMaster)
                .Include(a => a.VendorMaster)
                .FirstOrDefaultAsync(m => m.APid == id);
            if (assetProcurement == null)
            {
                return NotFound();
            }

            return View(assetProcurement);
        }

        // POST: AssetProcurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetProcurement = await _context.AssetProcurement.FindAsync(id);
            if (assetProcurement != null)
            {
                _context.AssetProcurement.Remove(assetProcurement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetProcurementExists(int id)
        {
            return _context.AssetProcurement.Any(e => e.APid == id);
        }
    }
}
