using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Employee
    {
        public Employee(){}
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Position { get; set; }
        public DateTime DateOfWorkStart { get; set; }
    }
}
