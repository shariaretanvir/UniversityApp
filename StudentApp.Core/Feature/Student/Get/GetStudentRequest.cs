using MediatR;
using StudentApp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Get
{
    public class GetStudentRequest : IRequest<GetStudentResponse>
    {
        public ResourceParameters ResourceParameters { get; set; }
    }
}
