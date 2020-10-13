using System;
using System.Collections.Generic;

namespace VideoRental
{
    public partial class RentalRecord
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateOfRent { get; set; }
        public DateTime DateOfReturn { get; set; }
        public int PaymentCheck { get; set; }
        public int ReturnCheck { get; set; }
        public int DiscId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Disc Disc { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
