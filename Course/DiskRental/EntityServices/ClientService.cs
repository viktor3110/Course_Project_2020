using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.EntityServices
{
    public class ClientService
    {
        public enum SortState
        {
            LastNameAsc,
            LastNameDesc
        }

        public IQueryable<Models.Client> Filter(IQueryable<Models.Client> clients, string selectedLastName)
        {
            if (!string.IsNullOrEmpty(selectedLastName))
            {
                clients = clients.Where(c => c.LastName.Contains(selectedLastName));
            }
            return clients;
        }

        public IQueryable<Models.Client> Sort(IQueryable<Models.Client> clients, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.LastNameAsc:
                    clients = clients.OrderBy(c => c.LastName);
                    break;
                case SortState.LastNameDesc:
                    clients = clients.OrderByDescending(c => c.LastName);
                    break;
            }
            return clients;
        }

        public IQueryable<Models.Client> Paging(IQueryable<Models.Client> clients, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return clients.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedLastName)
        {
            if (string.IsNullOrEmpty(selectedLastName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "clientSelectedLastName", out selectedLastName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, ref int? page, ref SortState? sortState)
        {
            if (page == null)
            {
                if (cookies.TryGetValue(username + "clientPage", out string pageStr))
                {
                    page = int.Parse(pageStr);
                }
            }
            if (sortState == null)
            {
                if (cookies.TryGetValue(username + "clientSortState", out string sortStateStr))
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
            cookies.Append(username + "clientSelectedLastName", selectedLastName);
            cookies.Append(username + "clientPage", page.ToString());
            cookies.Append(username + "clientSortState", sortState.ToString());
        }
    }
}
