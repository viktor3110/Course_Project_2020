using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.Data
{
    public class DBInitializer
    {
        int _refferenceTableSize;
        int _operationalTableSize;
        public DBInitializer(int refferenceTableSize = 100, int operationalTableSize = 10000)
        {
            _refferenceTableSize = refferenceTableSize;
            _operationalTableSize = operationalTableSize;
        }
        public void Initialize(DiskRentalContext dbContext)
        {
            Random rand = new Random();

            if (!dbContext.Positions.Any())
            {
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    dbContext.Positions.Add(new Models.Position
                    {
                        Name = GetRandomString(50)
                    });
                }
            }
            dbContext.SaveChanges();

            if (!dbContext.Employees.Any())
            {
                var positions = dbContext.Positions.ToList();

                for (int i = 0; i < _operationalTableSize; i++)
                {
                    var position = positions.ElementAt(rand.Next(dbContext.Positions.Count() - 1));

                    dbContext.Employees.Add(new Models.Employee
                    {
                        LastName = GetRandomString(50),
                        FirstName = GetRandomString(50),
                        MiddleName = GetRandomString(50),
                        HiringDate = GetRandomDate(new DateTime(2000, 1, 1), DateTime.Now),
                        PositionId = position.Id,
                        Position = position
                    });
                }
            }
            dbContext.SaveChanges();

            if (!dbContext.Clients.Any())
            {
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    dbContext.Clients.Add(new Models.Client
                    {
                        LastName = GetRandomString(50),
                        FirstName = GetRandomString(50),
                        MiddleName = GetRandomString(50),
                        Address = GetRandomString(50),
                        Phone = rand.Next(1000000000, int.MaxValue),
                        PassportData = GetRandomString(50)
                    });
                }
            }
            dbContext.SaveChanges();
        }
        public string GetRandomString(int maxLength)
        {
            Random rand = new Random();
            int length = rand.Next(maxLength / 3, maxLength);
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var str = new char[length];
            for (int i = 0; i < length; i++)
            {
                if ((i + 1) % 10 == 0)
                {
                    str[i] = ' ';
                    continue;
                }
                str[i] = chars[rand.Next(chars.Length)];
            }
            return new string(str);
        }
        public DateTime GetRandomDate(DateTime minDate, DateTime maxDate)
        {
            Random rand = new Random();
            int range = (maxDate - minDate).Days;
            return minDate.AddDays(rand.Next(range));
        }
    }
}
