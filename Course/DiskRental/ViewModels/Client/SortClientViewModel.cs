using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.ViewModels.Client
{
    public class SortClientViewModel
    {
        public EntityServices.ClientService.SortState LastNameSort { get; set; }
        public EntityServices.ClientService.SortState Current { get; set; }
        public SortClientViewModel(EntityServices.ClientService.SortState sortState)
        {
            LastNameSort = sortState == EntityServices.ClientService.SortState.LastNameAsc ? EntityServices.ClientService.SortState.LastNameDesc : EntityServices.ClientService.SortState.LastNameAsc;
            Current = sortState;
        }
    }
}
