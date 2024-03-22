using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Delete
{
    public class DeleteStudentCommand : IRequest<DeleteStudentResponse>
    {
        public Guid Id { get; set; }
    }
}
