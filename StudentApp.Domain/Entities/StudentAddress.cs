using StudentApp.Domain.Common;
using StudentApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Domain.Entities
{
    public class StudentAddress : Entity<Guid>, IAudit
    {
        public override Guid Id { get; protected set; }
        public AddressType AddressType { get; private set; }
        public string FullAddress { get; private set; }
        public Student Student { get; private set; }

        public StudentAddress(Guid id, AddressType addressType, string fullAddress)
        {
            Id = id;
            AddressType = addressType;
            FullAddress = fullAddress;
        }

        public void SetStudent(Student student)=> Student = student;

        public DateTime Created { get; private set; }

        public DateTime Modified { get; private set; }

        public void SetCreatedOn(DateTime created)
        {
            Created = created;
        }

        public void SetModifiedOn(DateTime modified)
        {
            Modified = modified;
        }
    }
}
