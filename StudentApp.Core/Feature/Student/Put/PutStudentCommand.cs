using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Put
{
    public class PutStudentCommand : IRequest<PutStudentResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public List<PutStudentDetailsModel> PutStudentDetailsModels { get; set; }
    }
}
