using System;
using System.Collections;
using System.Linq;
using System.Reflection.Emit;

namespace VideoRental
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoRentalContext db = new VideoRentalContext();
            //1 запрос
            var clients = db.Clients;
            Print("Клиенты", clients.Take(10).ToList());

            //2 запрос
            var employeesWhoStartInJanuary = db.Employees.Where(employee => employee.DateOfWorkStart.Month == 1);
            Print("Работники с большим стажем", employeesWhoStartInJanuary.Take(10).ToList());

            //3 запрос
            var averageDiscPrice = db.Discs.GroupBy(disc => disc.Price).Average(disc => disc.Key);
            Console.WriteLine($"Средняя цена дисков = {averageDiscPrice}");

            //4 запрос
            var discsWithGenres = db.Discs.Join(db.Genres, disc => disc.Genre, genre => genre.Id,
                (disc, genre) => new
                {
                    DiscName = disc.Name,
                    GenreName = genre.Name
                });
            Print("Диски с их жанрами", discsWithGenres.Take(10).ToList());

            //5 запрос
            var rentalClientDiscs = db.RentalRecords.Where(record => record.ClientId == 12).Join(db.Discs,
                record => record.DiscId, disc => disc.Id, (record, disc) => new
                {
                    record.Id,
                    disc.Name
                });
            Print("Диски которые арендовал один и тот же клиент", rentalClientDiscs.Take(10).ToList());

            //6 запрос
            Client client = new Client { Fio = "sdfsdf", Number = 1231232, Pasport = "sdfsdfsdf" };
            db.Clients.Add(client);
            db.SaveChanges();
            Console.WriteLine("Клиент добавлен");

            //7 запрос
            Random rand = new Random();
            RentalRecord rr = new RentalRecord
            {
                ClientId = db.Clients.ToList().ElementAt(rand.Next(0, db.Clients.Count() - 1)).Id,
                DateOfRent = DateTime.Now,
                DateOfReturn = DateTime.Now,
                PaymentCheck = 1,
                ReturnCheck = 1,
                DiscId = db.Discs.ToList().ElementAt(rand.Next(0, db.Discs.Count() - 1)).Id,
                EmployeeId = db.Employees.ToList().ElementAt(rand.Next(0, db.Employees.Count() - 1)).Id
            };
            db.RentalRecords.Add(rr);
            db.SaveChanges();
            Console.WriteLine("Запись добавлена");

            //8 запрос
            db.Clients.Remove(db.Clients.ToList()[db.Clients.Count() - 1]);
            db.SaveChanges();
            Console.WriteLine("Клиент удалён");

            //9 запрос
            db.RentalRecords.Remove(db.RentalRecords.ToList()[db.RentalRecords.Count() - 1]);
            db.SaveChanges();
            Console.WriteLine("Запись удалена");

            //10 запрос
            var discsWhereLowPrice = db.Discs.Where(disc => disc.Price < 100);
            foreach(var discLowPrice in discsWhereLowPrice)
            {
                discLowPrice.Price *= (decimal)1.1;
            }
            Console.WriteLine("Цены на дешевые диски увеличены");
        }
        static void Print(string sqltext, IEnumerable items)
        {
            Console.WriteLine(sqltext);
            Console.WriteLine("Записи: ");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }
    }
}
