using System.Collections.Generic;
using System.Linq;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    // Контроллер представления страниц записей из таблиц
    public class TablesController : Controller
    {
        // Объект контекста данных
        private readonly VideoRentalContext db;

        public TablesController(VideoRentalContext applicationContext)
        {
            db = applicationContext;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 256 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetClients()
        {
            List<Client> clients = db.Clients.ToList();
            return View(clients);
        }

        // Метод получения страницы работников.
        // Данная страница кэшируется на 256 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetEmployees()
        {
            List<Employee> employees = db.Employees.ToList();
            return View(employees);
        }

        // Метод получения страницы жанров.
        // Данная страница кэшируется на 256 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetGenres()
        {
            List<Genre> genres = db.Genres.ToList();
            return View(genres);
        }

        // Метод получения страницы дисков.
        // Данная страница кэшируется на 256 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetDiscs()
        {
            List<Disc> discs = db.Discs.ToList();

            // Преобразование данных для удобного представления
            List<DiscViewModel> models = new List<DiscViewModel>();
            foreach (var disc in discs)
            {
                var genre = db.Genres.Where(elem => elem.Id == disc.Genre).First().Name;
                models.Add(new DiscViewModel()
                {
                    Id = disc.Id,
                    Country = disc.Country,
                    DateOfCreation = disc.DateOfCreation,
                    DateOfRecord = disc.DateOfRecord,
                    TypeOfDisc = disc.TypeOfDisc,
                    Creater = disc.Creater,
                    MainActor = disc.MainActor,
                    Name = disc.Name,
                    Price = disc.Price,
                    GenreName = genre
                });
            }
            return View(models);
        }

        // Метод получения страницы Записей проката.
        // Данная страница кэшируется на 256 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetRentalRecords()
        {
            List<RentalRecord> rentalRecords = db.RentalRecords.ToList();

            // Преобразование данных для удобного представления
            List<RentalRecordViewModel> models = new List<RentalRecordViewModel>();
            foreach (var rentalRecord in rentalRecords)
            {
                string clentFIO = db.Clients.Where(item => item.Id == rentalRecord.ClientId).Select(item => item.Fio).First();
                string emplFIO = db.Employees.Where(item => item.Id == rentalRecord.EmployeeId).Select(item => item.Fio).First();
                string discName = db.Discs.Where(item => item.Id == rentalRecord.DiscId).Select(item => item.Name).First();
                models.Add(new RentalRecordViewModel()
                {
                    Id = rentalRecord.Id,
                    DateOfRent = rentalRecord.DateOfRent,
                    ClientFIO = clentFIO,
                    DateOfReturn = rentalRecord.DateOfReturn,
                    DiscName = discName,
                    PaymentCheck = rentalRecord.PaymentCheck,
                    ReturnCheck = rentalRecord.ReturnCheck,
                    EmployeeFIO = emplFIO
                });
            }
            return View(models);
        }
    }
}
