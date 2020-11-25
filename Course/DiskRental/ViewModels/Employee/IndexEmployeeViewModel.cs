using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.ViewModels.Employee
{
    public class IndexEmployeeViewModel
    {
        public IEnumerable<Models.Employee> Employees { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterEmployeeViewModel FilterEmployeeViewModel { get; set; }
        public SortEmployeeViewModel SortEmployeeViewModel { get; set; }
    }
}
