using System;
using System.Collections.Generic;

namespace VideoRental
{
    public partial class Client
    {
        public Client()
        {
            RentalRecords = new HashSet<RentalRecord>();
        }

        public int Id { get; set; }
        public string Fio { get; set; }
        public int Number { get; set; }
        public string Pasport { get; set; }

        public virtual ICollection<RentalRecord> RentalRecords { get; set; }
        public override string ToString()
        {
            return $"{Id}, {Fio}, {Number}, {Pasport}";
        }
    }
}
