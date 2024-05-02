using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Common
{
    public abstract class PagingParameter
    {
        private const int maxPageSize = 50;
        private const int defaultPageSize = 10;
        private int _pageSize;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : (value == 0 ? defaultPageSize : value); }
        }
    }
}
