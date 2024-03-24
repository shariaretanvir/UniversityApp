using StudentApp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Get
{
    public class ResourceParameters : PagingParameter
    {
        public string OrderBy { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;
        public string SearchQuery { get; set; } = string.Empty;
        public int AddressType { get; set; } = 0;
    }
}
