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
    public class EmployeesController : Controller
    {
        private readonly IMSDbContext _context;

        public EmployeesController(IMSDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var iMSDbContext = _context.Employee.Include(e => e.Department).Include(e => e.Designation).Include(e => e.Location);
            for (int i = 0; i < _context.Employee.ToList().Count; i++)
            {
                _context.Employee.ToList()[i].Serialnumber = i + 1;
            }
            return View(await iMSDbContext.ToListAsync());
        }
        //public async Task<IActionResult> IndexReport(DateTime? startDate, DateTime? endDate)
        //{
        //    IQueryable<Employee> employees = _context.Employee.Include(e => e.Department).Include(e => e.Designation).Include(e => e.Location);

        //    if (startDate.HasValue)
        //    {
        //        employees = employees.Where(e => e.DateOfJoining >= startDate.Value);
        //    }

        //    if (endDate.HasValue)
        //    {
        //        employees = employees.Where(e => e.DateOfJoining <= endDate.Value);
        //    }

        //    return View(await employees.ToListAsync());
        //}
        public async Task<IActionResult> IndexReport(DateTime? startDate, DateTime? endDate,int? departmentId, int? locationId, int? employeeId)
        {
            for (int i = 0; i < _context.Employee.ToList().Count; i++)
            {
                _context.Employee.ToList()[i].Serialnumber = i + 1;
            }
            IQueryable<Employee> employees = _context.Employee
                .Include(e => e.Designation)
                .Include(e => e.Department)
                .Include(e => e.Location);

            // Apply Filters
            if (startDate.HasValue)
            {
                employees = employees.Where(e => e.DateOfJoining >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                employees = employees.Where(e => e.DateOfJoining <= endDate.Value);
            }

            if (departmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == departmentId.Value);
            }

            if (locationId.HasValue)
            {
                employees = employees.Where(e => e.LocationId == locationId.Value);
            }
            if (employeeId.HasValue)
            {
                employees = employees.Where(a => a.Eid == employeeId);
            }
            ViewBag.Employees = _context.Employee.ToList();
            ViewBag.Departments = _context.Department.ToList();
            ViewBag.Locations = _context.Location.ToList();
            return View(await employees.ToListAsync());
        }



        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Location)
                .FirstOrDefaultAsync(m => m.Eid == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        private List<SelectListItem> GetDesignation()
        {
            var lstDesignation = new List<SelectListItem>();
            List<Designation> Designations = _context.Designation.ToList();
            lstDesignation = Designations.Select(d => new SelectListItem()
            {
                Text = d.DesignationName,
                Value = d.Did.ToString()
            }).ToList();
            var defItem = new SelectListItem()
            {
                Text = "Select Designation",
                Value = ""
            };
            lstDesignation.Add(defItem);

            return lstDesignation;
        }
        private List<SelectListItem> GetDepartment()
        {
            var lstDepartment = new List<SelectListItem>();
            List<Department> Departments = _context.Department.ToList();
            var defItem = new SelectListItem()
            {
                Text = "Select Department",
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
        private List<SelectListItem> GetLocation()
        {
            var lstLocation = new List<SelectListItem>();
            List<Location> Locations = _context.Location.ToList();
            var defItem = new SelectListItem()
            {
                Text = "Select Designation",
                Value = ""
            };
            lstLocation = Locations.Select(d => new SelectListItem()
            {
                Text = d.LocationName,
                Value = d.Lid.ToString()
            }).ToList();
            lstLocation.Add(defItem);

            return lstLocation;
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> Status = new()
            {
                new SelectListItem{Value="Active",Text="Active"},
                new SelectListItem{Value="Inactive",Text="Inactive"},
            };
            ViewBag.Status = Status;
            Employee employee = new Employee();
            ViewBag.DesignationId = GetDesignation();
            ViewBag.DepartmentId = GetDepartment();
            ViewBag.LocationId = GetLocation();
            return View(employee);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

       
        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<SelectListItem> Status = new()
            {
                new SelectListItem{Value="Active",Text="Active"},
                new SelectListItem{Value="Inactive",Text="Inactive"},
            };
            ViewBag.Status = Status;
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.DesignationId = GetDesignation();
            ViewBag.DepartmentId = GetDepartment();
            ViewBag.LocationId = GetLocation();
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Eid)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            //}                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!EmployeeExists(employee.Eid))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
            ViewBag.DesignationId = GetDesignation();
            ViewBag.DepartmentId = GetDepartment();
            ViewBag.LocationId = GetLocation();

            return View(employee);

        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Location)
                .FirstOrDefaultAsync(m => m.Eid == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Eid == id);
        }

    }
}
