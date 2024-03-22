using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Domain.Common
{
    public interface IAudit
    {
        DateTime Created { get; }
        DateTime Modified { get; }

        void SetCreatedOn(DateTime created);
        void SetModifiedOn(DateTime modified);
    }
}
