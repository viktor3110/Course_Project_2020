using System;
using System.Linq;

namespace WebApp.Models
{
    // Класс, содержащий метод инициализации базы данных
    public static class DbInitializer
    {
        // Метод инициализации базы данных путем заполнения таблиц тестовыми наборами данных
        public static void Initialize(VideoRentalContext db)
        {
            // Метод, который проверяет существование базы данных
            db.Database.EnsureCreated();

            // Объекты для генерации случайных чисел и записей
            Random randObj = new Random();

            char[] letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".ToCharArray();

            // Проверка на наличие записей в таблице Клиенты
            if (!db.Clients.Any())
            {
                int clientId;
                string fio = "";
                int numb;
                string passport = "";

                // Создание 40 записей в таблице
                for (int id = 1; id <= 40; id++)
                {
                    // Получение Id
                    clientId = db.Clients.Count() + 1;

                    // Создание ФИО
                    int rand = randObj.Next(17, 100);
                    for (int i = 1; i <= rand; i++)
                    {
                        fio += letters[randObj.Next(33)];
                    }

                    // Создание номера клиента
                    numb = randObj.Next(1000000, 9999999);

                    // Создание паспорта
                    for (int i = 1; i <= 9; i++)
                    {
                        passport += letters[randObj.Next(33)];
                    }

                    // Добавление записи в таблицу
                    db.Clients.Add(new Client { Id = clientId, Fio = fio, Pasport = passport, Number = numb });
                }
                db.SaveChanges();
            }

            // Проверка на наличие записей в таблице Работники
            if (!db.Employees.Any())
            {
                int employeeId;
                string fio = "";
                string position = "";
                DateTime date;

                // Создание 40 записей
                for (int id = 1; id <= 40; id++)
                {
                    // Получение Id
                    employeeId = db.Employees.Count() + 1;

                    // Создание ФИО работника
                    int rand = randObj.Next(15, 100);
                    for (int i = 1; i <= rand; i++)
                    {
                        fio += letters[randObj.Next(33)];
                    }

                    // Создание должности работника
                    rand = randObj.Next(5, 50);
                    for (int i = 1; i <= rand; i++)
                    {
                        position += letters[randObj.Next(33)];
                    }

                    // Создание даты начала работы
                    date = DateTime.Now.AddYears(-2);

                    // Добавление записи в таблицу
                    db.Employees.Add(new Employee { Id = employeeId, Fio = fio, Position = position, DateOfWorkStart = date });
                }
                db.SaveChanges();
            }

            // Проверка на наличие записей в таблице Жанры
            if (!db.Genres.Any())
            {
                int genreId;
                string name = "";
                string descr = "";

                // Создание 40 записей
                for (int id = 1; id <= 40; id++)
                {
                    // Получение Id
                    genreId = db.Genres.Count() + 1;

                    // Создание названия жанра
                    int rand = randObj.Next(3, 21);
                    for (int i = 1; i <= rand; i++)
                    {
                        name += letters[randObj.Next(33)];
                    }

                    // Создание описания жанра
                    rand = randObj.Next(17, 200);
                    for (int i = 1; i <= rand; i++)
                    {
                        descr += letters[randObj.Next(33)];
                    }

                    // Добавление записи в таблицу
                    db.Genres.Add(new Genre { Id = genreId, Name = name, Description = descr });
                }
                db.SaveChanges();
            }

            // Проверка на наличие записей в таблице Диски
            if (!db.Discs.Any())
            {
                int discId;
                string name = "";
                DateTime date;
                string creat = "";
                string country = "";
                string mainAct = "";
                DateTime recordDate;
                int genreId;
                string type = "";
                decimal price;

                // Создание 100 записей
                for (int id = 1; id <= 100; id++)
                {
                    // Получение Id
                    discId = db.Discs.Count() + 1;

                    // Создание названия диска
                    int rand = randObj.Next(3, 21);
                    for (int i = 1; i <= rand; i++)
                    {
                        name += letters[randObj.Next(33)];
                    }

                    // Получение даты создания фильма
                    date = DateTime.Now.AddYears(-1);

                    // Создание даты записи
                    recordDate = DateTime.Now.AddDays(-3);

                    // Создание создателя фильма
                    rand = randObj.Next(3, 21);
                    for (int i = 1; i <= rand; i++)
                    {
                        creat += letters[randObj.Next(33)];
                    }

                    // Создание страны
                    rand = randObj.Next(3, 51);
                    for (int i = 1; i <= rand; i++)
                    {
                        country += letters[randObj.Next(33)];
                    }

                    // Создание главного актера
                    rand = randObj.Next(17, 101);
                    for (int i = 1; i <= rand; i++)
                    {
                        mainAct += letters[randObj.Next(33)];
                    }

                    // Создание стоимости фильма
                    price = (decimal)randObj.NextDouble() * 100;

                    // Получение Id жанра
                    genreId = randObj.Next(1, db.Genres.Select(elem => elem.Id).Max());

                    // Создание типа диска
                    rand = randObj.Next(3, 8);
                    for (int i = 1; i <= rand; i++)
                    {
                        type += letters[randObj.Next(33)];
                    }

                    // Добавление записи в таблицу
                    db.Discs.Add(new Disc { Id = discId, Name = name, Country = country, Creater = creat, DateOfCreation = date, DateOfRecord = recordDate, Genre = genreId, MainActor = mainAct, TypeOfDisc = type, Price = price });
                }
                db.SaveChanges();
            }

            // Проверка на наличие записей в таблице Записи проката
            if (!db.RentalRecords.Any())
            {
                int rentId;
                int clientId;
                DateTime dateRent;
                DateTime dateReturn;
                int payCheck;
                int discId;
                int emplId;

                // Создание 100 записей
                for (int id = 1; id <= 100; id++)
                {
                    // Получение Id
                    rentId = db.RentalRecords.Count() + 1;

                    // Создание Id клиента
                    clientId = randObj.Next(1, db.Clients.Select(elem => elem.Id).Max());

                    // Создание даты проката
                    dateRent = DateTime.Now.AddDays(-10);

                    // Создание даты возврата
                    dateReturn = DateTime.Now.AddDays(-3);

                    // Создание проверки оплаты
                    payCheck = randObj.Next(0,2);

                    // Создание Id диска
                    discId = randObj.Next(1, db.Discs.Select(elem => elem.Id).Max());

                    // Получение Id работника
                    emplId = randObj.Next(1, db.Employees.Select(elem => elem.Id).Max());

                    // Добавление записи в таблицу
                    db.RentalRecords.Add(new RentalRecord { Id = rentId, ClientId = clientId, DateOfRent = dateRent, DateOfReturn = dateReturn, DiscId = discId, PaymentCheck = payCheck, ReturnCheck = 1, EmployeeId = emplId});
                }
                db.SaveChanges();
            }
        }
    }
}