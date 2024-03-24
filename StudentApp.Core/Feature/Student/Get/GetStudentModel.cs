using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Get
{
    public class GetStudentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public List<GetStudentAddressModel> GetStudentAddressModels { get; set; }
    }

    public class GetStudentAddressModel
    {
        public Guid AddressId { get; set; }
        public string AddressType { get; set; }
        public string FullAddress { get; set; }
        public Guid StudentId { get; set; }
    }
}
