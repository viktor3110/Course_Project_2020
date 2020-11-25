using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.ViewModels.Position
{
    public class SortPositionViewModel
    {
        public EntityServices.PositionService.SortState NameSort { get; set; }
        public EntityServices.PositionService.SortState Current { get; set; }
        public SortPositionViewModel(EntityServices.PositionService.SortState sortOrder)
        {
            NameSort = sortOrder == EntityServices.PositionService.SortState.NameAsc ? EntityServices.PositionService.SortState.NameDesc : EntityServices.PositionService.SortState.NameAsc;
            Current = sortOrder;
        }
    }
}
