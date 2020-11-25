using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiskRental.Data;
using DiskRental.Models;
using DiskRental.EntityServices;

namespace DiskRental.Controllers
{
    public class PositionsController : Controller
    {
        private readonly DiskRentalContext _context;
        private readonly PositionService _service;
        private readonly int _pageSize;

        public PositionsController(DiskRentalContext context)
        {
            _context = context;
            _service = new PositionService();
            _pageSize = 5;
        }

        // GET: Positions
        public async Task<IActionResult> Index(string selectedName, int? page, PositionService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name, 
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref selectedName);
            _service.SetDefaultValuesIfNull(ref selectedName, ref page, ref sortState);
            _service.SetCookies(Response.Cookies, User.Identity.Name, selectedName, page, sortState);

            var positions = _context.Positions.AsQueryable();

            positions = _service.Filter(positions, selectedName);

            var count = await positions.CountAsync();

            positions = _service.Sort(positions, (PositionService.SortState)sortState);
            positions = _service.Paging(positions, isFromFilter, (int)page, _pageSize);

            ViewModels.Position.IndexPositionViewModel model = new ViewModels.Position.IndexPositionViewModel
            {
                Positions = await positions.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterPositionViewModel = new ViewModels.Position.FilterPositionViewModel(selectedName),
                SortPositionViewModel = new ViewModels.Position.SortPositionViewModel((PositionService.SortState)sortState),
            };

            return View(model);
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Position position)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Positions");
            }
            if (ModelState.IsValid)
            {
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Positions");
            }
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Position position)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Positions");
            }
            if (id != position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.Id))
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
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Positions");
            }
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Positions");
            }
            var position = await _context.Positions.FindAsync(id);
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.Id == id);
        }
    }
}
