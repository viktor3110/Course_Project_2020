using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.ViewModels.Client
{
    public class IndexClientViewModel
    {
        public IEnumerable<Models.Client> Clients { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterClientViewModel FilterClientViewModel { get; set; }
        public SortClientViewModel SortClientViewModel { get; set; }
    }
}
