using System;

namespace DiskRental.ViewModels.Employee
{
    public class SortEmployeeViewModel
    {
        public EntityServices.EmployeeService.SortState LastNameSort { get; set; }
        public EntityServices.EmployeeService.SortState Current { get; set; }
        public SortEmployeeViewModel(EntityServices.EmployeeService.SortState sortState)
        {
            LastNameSort = sortState == EntityServices.EmployeeService.SortState.LastNameAsc ? EntityServices.EmployeeService.SortState.LastNameDesc : EntityServices.EmployeeService.SortState.LastNameAsc;
            Current = sortState;
        }
    }
}
