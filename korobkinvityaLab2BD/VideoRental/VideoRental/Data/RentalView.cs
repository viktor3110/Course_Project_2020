using System;
using System.Collections.Generic;

namespace VideoRental
{
    public partial class RentalView
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public DateTime DateOfRent { get; set; }
        public DateTime DateOfReturn { get; set; }
        public int PaymentCheck { get; set; }
        public int ReturnCheck { get; set; }
        public string Name { get; set; }
        public string Employee { get; set; }
    }
}
