using StudentApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Domain.Entities
{
    public class Student : Entity<Guid>, IAudit
    {
        public override Guid Id { get; protected set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public bool IsActive { get; private set; }

        public Student(Guid id, string name, int age, bool isActive)
        {
            Id = id;
            Name = name;
            Age = age;
            IsActive = isActive;
        }

        #region audit
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
        #endregion

    }
}
