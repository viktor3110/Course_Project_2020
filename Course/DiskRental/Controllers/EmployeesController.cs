using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiskRental.Data;
using DiskRental.EntityServices;
using DiskRental.Models;

namespace DiskRental.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly Services.CachingService _caching;
        private readonly DiskRentalContext _context;
        private readonly EmployeeService _service;
        private readonly int _pageSize;

        public EmployeesController(DiskRentalContext context, Services.CachingService caching)
        {
            _caching = caching;
            _context = context;
            _service = new EmployeeService();
            _pageSize = 5;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string selectedLastName, int? page, EmployeeService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (_caching.TryGetValue($"employees-{selectedLastName}-{page}-{sortState}", out ViewModels.Employee.IndexEmployeeViewModel cachedModel))
            {
                return View(cachedModel);
            }
            else
            {
                bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

                _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name,
                    ref page, ref sortState);
                _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                    ref selectedLastName);
                _service.SetDefaultValuesIfNull(ref selectedLastName, ref page, ref sortState);
                _service.SetCookies(Response.Cookies, User.Identity.Name, selectedLastName, page, sortState);

                var employees = _context.Employees.Include(e => e.Position).AsQueryable();

                employees = _service.Filter(employees, selectedLastName);

                var count = await employees.CountAsync();

                employees = _service.Sort(employees, (EmployeeService.SortState)sortState);
                employees = _service.Paging(employees, isFromFilter, (int)page, _pageSize);

                ViewModels.Employee.IndexEmployeeViewModel model = new ViewModels.Employee.IndexEmployeeViewModel
                {
                    Employees = await employees.ToListAsync(),
                    PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                    FilterEmployeeViewModel = new ViewModels.Employee.FilterEmployeeViewModel(selectedLastName),
                    SortEmployeeViewModel = new ViewModels.Employee.SortEmployeeViewModel((EmployeeService.SortState)sortState),
                };

                _caching.Set($"employees-{selectedLastName}-{page}-{sortState}", model);

                return View(model);
            }
        }

        // GET: Employees/Details/5
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

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Employees");
            }
            ViewData["Positions"] = new SelectList(_context.Positions, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,MiddleName,BirthDate,PositionId")] Employee employee)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Employees");
            }
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                _caching.Clean();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Positions"] = new SelectList(_context.Positions, "Id", "Name", employee.PositionId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Employees");
            }
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Positions"] = new SelectList(_context.Positions, "Id", "Name", employee.PositionId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,MiddleName,BirthDate,PositionId")] Employee employee)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Employees");
            }
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    _caching.Clean();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", employee.PositionId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Employees");
            }
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Employees");
            }
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            _caching.Clean();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
