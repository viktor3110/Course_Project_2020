using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.EntityServices
{
    public class EmployeeService
    {
        public enum SortState
        {
            LastNameAsc,
            LastNameDesc
        }

        public IQueryable<Models.Employee> Filter(IQueryable<Models.Employee> employees, string selectedLastName)
        {
            if (!string.IsNullOrEmpty(selectedLastName))
            {
                employees = employees.Where(e => e.LastName.Contains(selectedLastName));
            }
            return employees;
        }

        public IQueryable<Models.Employee> Sort(IQueryable<Models.Employee> employees, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.LastNameAsc:
                    employees = employees.OrderBy(e => e.LastName);
                    break;
                case SortState.LastNameDesc:
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;
            }
            return employees;
        }

        public IQueryable<Models.Employee> Paging(IQueryable<Models.Employee> employees, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return employees.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedLastName)
        {
            if (string.IsNullOrEmpty(selectedLastName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "employeeSelectedLastName", out selectedLastName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, ref int? page, ref SortState? sortState)
        {
            if (page == null)
            {
                if (cookies.TryGetValue(username + "employeePage", out string pageStr))
                {
                    page = int.Parse(pageStr);
                }
            }
            if (sortState == null)
            {
                if (cookies.TryGetValue(username + "employeeSortState", out string sortStateStr))
                {
                    sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                }
            }
        }

        public void SetDefaultValuesIfNull(ref string selectedLastName, ref int? page, ref SortState? sortState)
        {
            selectedLastName ??= "";
            page ??= 1;
            sortState ??= SortState.LastNameAsc;
        }

        public void SetCookies(IResponseCookies cookies, string username, string selectedLastName, int? page, SortState? sortState)
        {
            cookies.Append(username + "employeeSelectedLastName", selectedLastName);
            cookies.Append(username + "employeePage", page.ToString());
            cookies.Append(username + "employeeSortState", sortState.ToString());
        }
    }
}
