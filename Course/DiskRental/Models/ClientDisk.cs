using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.Models
{
    public class ClientDisk
    {
        public int Id { get; set; }
        public DateTime RecordDate { get; set; }
        public int ClientId { get; set; }
        public int DiskId { get; set; }
        public DateTime TakeDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsReturned { get; set; }
        public int EmployeeId { get; set; }

        public Client Client { get; set; }
        public Disk Disk { get; set; }
        public Employee Employee { get; set; }
    }
}
