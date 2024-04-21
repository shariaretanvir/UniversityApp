using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Common
{
    public class PagedList<T>
    {
        public List<T> Items { get; private set; }
        public int TotalCount { get; private set; }
        public int CurrentPage { get; private set; } =1;
        public int TotalPage { get; private set; }        
        public int PageSize { get; private set; }
        public bool HasPreviousPage
        {
            get => CurrentPage > 1;
            private set { }
        }
        public bool HasNextPage
        {
            get => CurrentPage < TotalPage;
            private set { }
        }

        public PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            TotalPage = (int)Math.Ceiling((double)totalCount/pageSize);
            PageSize = pageSize;
        }

        public static PagedList<T> Create(List<T> sourceData, int pageNumber, int pageSize)
        {
            var pagingData = sourceData.Skip(pageNumber-1 * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(pagingData, sourceData.Count, pageNumber, pageSize);
        }
    }
}
