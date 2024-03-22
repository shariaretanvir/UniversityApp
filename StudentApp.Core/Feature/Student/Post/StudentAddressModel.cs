using StudentApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Post
{
    public class StudentAddressModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public AddressType  AddressType{ get; set; }
        public string FullAddress { get; set; }
    }
}
