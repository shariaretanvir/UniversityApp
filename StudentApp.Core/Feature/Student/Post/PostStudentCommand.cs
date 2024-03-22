using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Post
{
    public class PostStudentCommand : IRequest<PostStudentResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public List<StudentAddressModel> StudentDetailsModels { get; set; }

    }
}
