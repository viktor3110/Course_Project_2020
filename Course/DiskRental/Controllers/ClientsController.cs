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
    public class ClientsController : Controller
    {
        private readonly DiskRentalContext _context;
        private readonly ClientService _service;
        private readonly int _pageSize;

        public ClientsController(DiskRentalContext context)
        {
            _context = context;
            _service = new ClientService();
            _pageSize = 5;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string selectedLastName, int? page, ClientService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }

            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name,
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref selectedLastName);
            _service.SetDefaultValuesIfNull(ref selectedLastName, ref page, ref sortState);
            _service.SetCookies(Response.Cookies, User.Identity.Name, selectedLastName, page, sortState);

            var clients = _context.Clients.AsQueryable();

            clients = _service.Filter(clients, selectedLastName);

            var count = await clients.CountAsync();

            clients = _service.Sort(clients, (ClientService.SortState)sortState);
            clients = _service.Paging(clients, isFromFilter, (int)page, _pageSize);

            ViewModels.Client.IndexClientViewModel model = new ViewModels.Client.IndexClientViewModel
            {
                Clients = await clients.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterClientViewModel = new ViewModels.Client.FilterClientViewModel(selectedLastName),
                SortClientViewModel = new ViewModels.Client.SortClientViewModel((ClientService.SortState)sortState),
            };

            return View(model);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Clients");
            }
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,MiddleName,BirthDate,Gender,Address,Phone,PassportData,Discount")] Client client)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Clients");
            }
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Clients");
            }
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,MiddleName,BirthDate,Gender,Address,Phone,PassportData,Discount")] Client client)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Clients");
            }
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Clients");
            }
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Clients");
            }
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
