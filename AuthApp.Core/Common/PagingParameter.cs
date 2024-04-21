using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Common
{
    public class PagingParameter
    {
        private const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => PageSize;
            set
            {
                PageSize = value > maxPageSize ? maxPageSize : value;
            }
        }
    }
}
