using System;
using System.Collections.Generic;

namespace DiskRental.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime HiringDate { get; set; }
        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}
