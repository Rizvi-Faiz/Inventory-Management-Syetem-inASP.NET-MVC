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
    public class VendorMastersController : Controller
    {
        private readonly IMSDbContext _context;

        public VendorMastersController(IMSDbContext context)
        {
            _context = context;
        }

        // GET: VendorMasters
        public async Task<IActionResult> Index()
        {
            for (int i = 0; i < _context.VendorMaster.ToList().Count; i++)
            {
                _context.VendorMaster.ToList()[i].SerialNumber = i + 1;
            }
            return View(await _context.VendorMaster.ToListAsync());
        }

        // GET: VendorMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorMaster = await _context.VendorMaster
                .FirstOrDefaultAsync(m => m.VMid == id);
            if (vendorMaster == null)
            {
                return NotFound();
            }

            return View(vendorMaster);
        }

        // GET: VendorMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VMid,SerialNumber,VendorName,VendorDescription")] VendorMaster vendorMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorMaster);
        }

        // GET: VendorMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorMaster = await _context.VendorMaster.FindAsync(id);
            if (vendorMaster == null)
            {
                return NotFound();
            }
            return View(vendorMaster);
        }

        // POST: VendorMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VMid,SerialNumber,VendorName,VendorDescription")] VendorMaster vendorMaster)
        {
            if (id != vendorMaster.VMid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorMasterExists(vendorMaster.VMid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendorMaster);
        }

        // GET: VendorMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorMaster = await _context.VendorMaster
                .FirstOrDefaultAsync(m => m.VMid == id);
            if (vendorMaster == null)
            {
                return NotFound();
            }

            return View(vendorMaster);
        }

        // POST: VendorMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorMaster = await _context.VendorMaster.FindAsync(id);
            if (vendorMaster != null)
            {
                _context.VendorMaster.Remove(vendorMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorMasterExists(int id)
        {
            return _context.VendorMaster.Any(e => e.VMid == id);
        }
    }
}
