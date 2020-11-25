using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.EntityServices
{
    public class PositionService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc
        }
        public IQueryable<Models.Position> Filter(IQueryable<Models.Position> positions, string selectedPositionName)
        {
            if (!string.IsNullOrEmpty(selectedPositionName))
            {
                positions = positions.Where(p => p.Name.Contains(selectedPositionName));
            }
            return positions;
        }

        public IQueryable<Models.Position> Sort(IQueryable<Models.Position> positions, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    positions = positions.OrderBy(p => p.Name);
                    break;
                case SortState.NameDesc:
                    positions = positions.OrderByDescending(p => p.Name);
                    break;
            }
            return positions;
        }

        public IQueryable<Models.Position> Paging(IQueryable<Models.Position> positions, bool isFromFilter, int page, int pageSize)
        {
            if(isFromFilter)
            {
                page = 1;
            }
            return positions.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName)
        {
            if(string.IsNullOrEmpty(selectedName))
            {
                if(!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "positionSelectedName", out selectedName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, ref int? page, ref SortState? sortState)
        {
            if(page == null)
            {
                if(cookies.TryGetValue(username + "positionPage", out string pageStr))
                {
                    page = int.Parse(pageStr);
                }
            }
            if(sortState == null)
            {
                if(cookies.TryGetValue(username + "positionSortState", out string sortStateStr))
                {
                    sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                }
            }
        }

        public void SetDefaultValuesIfNull(ref string selectedName, ref int? page, ref SortState? sortState)
        {
            selectedName ??= "";
            page ??= 1;
            sortState ??= SortState.NameAsc;
        }

        public void SetCookies(IResponseCookies cookies, string username, string selectedName, int? page, SortState? sortState)
        {
            cookies.Append(username + "positionSelectedName", selectedName);
            cookies.Append(username + "positionPage", page.ToString());
            cookies.Append(username + "positionSortState", sortState.ToString());
        }
    }
}
