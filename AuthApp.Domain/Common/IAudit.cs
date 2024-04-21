using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Domain.Common
{
    public interface IAudit
    {
        DateTime CreatedOn { get; }
        DateTime ModifiedOn { get; }

        void SetCreatedOn(DateTime createdOn);
        void SetModifiedOn(DateTime modifiedOn);
    }
}
