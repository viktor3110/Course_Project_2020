using System;
using System.Collections.Generic;

namespace WebApp.Models
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
    }
}
