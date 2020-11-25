using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RentalRecordViewModel
    {
        public int Id { get; set; }
        public string ClientFIO { get; set; }
        public DateTime DateOfRent { get; set; }
        public DateTime DateOfReturn { get; set; }
        public int PaymentCheck { get; set; }
        public int ReturnCheck { get; set; }
        public string DiscName { get; set; }
        public string EmployeeFIO { get; set; }
    }
}
