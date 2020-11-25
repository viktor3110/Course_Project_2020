using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApp.MiddleWares
{
    // Компонент middleware для инициализации базы данных
    public class DbInitializMiddleWare
    {
        // Ссылка на следующий компонент
        private readonly RequestDelegate _next;

        public DbInitializMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, VideoRentalContext context)
        {
            DbInitializer.Initialize(context);
            return _next(httpContext);
        }
    }

    // Метод расширения для добавления компонента middleware
    public static class DbInitializeExtensions
    {
        public static IApplicationBuilder UseInitializeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializMiddleWare>();
        }
    }
}
