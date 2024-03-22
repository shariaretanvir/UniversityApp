using StudentApp.Domain.Enums;

namespace StudentApp.Core.Feature.Student.Put
{
    public class PutStudentDetailsModel
    {
        public Guid AddressId { get; set; }
        public Guid StudentId { get; set; }
        public AddressType AddressType { get; set; }
        public string FullAddress { get; set; }
    }
}