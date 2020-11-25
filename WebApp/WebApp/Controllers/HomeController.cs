using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    // Контроллер начальной страницы
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Метод получения начальной страницы.
        // Начальная страница не кэшируется.
        [ResponseCache(CacheProfileName = "NoCaching")]
        public IActionResult Index()
        {
            return View();
        }

        // Метод получения страницы ошибки.
        // Страница ошибки не кэшируется.
        [ResponseCache(CacheProfileName = "NoCaching")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
