using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Common
{
    public class PagedList<T>
    {
        public List<T> Items { get; private set; }
        public int TotalCount { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPage { get; private set; }
        public bool HasPreviousPage
        {
            get => CurrentPage > 1;
            private set { }
        }
        public bool HasNextPage
        {
            get => TotalPage > CurrentPage;
            private set { }
        }

        private PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);            
        }

        public static PagedList<T> Create(List<T> sourceData, int pageNumber, int pageSize)
        {
            var pagedData = sourceData.Skip(pageNumber-1 * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(pagedData, sourceData.Count(), pageNumber, pageSize);
        }
    }
}
