using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.ViewModels.Employee
{
    public class FilterEmployeeViewModel
    {
        public string SelectedLastName { get; set; }

        public FilterEmployeeViewModel(string selectedLastName)
        {
            SelectedLastName = selectedLastName;
        }
    }
}
