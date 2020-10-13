using System;
using System.Collections.Generic;

namespace VideoRental
{
    public partial class Employee
    {
        public Employee()
        {
            RentalRecords = new HashSet<RentalRecord>();
        }

        public int Id { get; set; }
        public string Fio { get; set; }
        public string Position { get; set; }
        public DateTime DateOfWorkStart { get; set; }

        public virtual ICollection<RentalRecord> RentalRecords { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Fio}, {Position}, {DateOfWorkStart}";
        }
    }
}
