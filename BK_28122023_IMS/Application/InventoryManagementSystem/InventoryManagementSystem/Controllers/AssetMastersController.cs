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
    public class AssetMastersController : Controller
    {
        private readonly IMSDbContext _context;

        public AssetMastersController(IMSDbContext context)
        {
            _context = context;
        }

        // GET: AssetMasters
        public async Task<IActionResult> Index()
        {
            for (int i = 0; i < _context.AssetMaster.ToList().Count; i++)
            {
                _context.AssetMaster.ToList()[i].SerialNumber = i + 1;
            }
            return View(await _context.AssetMaster.ToListAsync());
        }

        // GET: AssetMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetMaster = await _context.AssetMaster
                .FirstOrDefaultAsync(m => m.AMid == id);
            if (assetMaster == null)
            {
                return NotFound();
            }

            return View(assetMaster);
        }

        // GET: AssetMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AMid,SerialNumber,AssetName,AssetModel,AssetDescription")] AssetMaster assetMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assetMaster);
        }

        // GET: AssetMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetMaster = await _context.AssetMaster.FindAsync(id);
            if (assetMaster == null)
            {
                return NotFound();
            }
            return View(assetMaster);
        }

        // POST: AssetMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AMid,SerialNumber,AssetName,AssetModel,AssetDescription")] AssetMaster assetMaster)
        {
            if (id != assetMaster.AMid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetMasterExists(assetMaster.AMid))
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
            return View(assetMaster);
        }

        // GET: AssetMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetMaster = await _context.AssetMaster
                .FirstOrDefaultAsync(m => m.AMid == id);
            if (assetMaster == null)
            {
                return NotFound();
            }

            return View(assetMaster);
        }

        // POST: AssetMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetMaster = await _context.AssetMaster.FindAsync(id);
            if (assetMaster != null)
            {
                _context.AssetMaster.Remove(assetMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetMasterExists(int id)
        {
            return _context.AssetMaster.Any(e => e.AMid == id);
        }
    }
}
