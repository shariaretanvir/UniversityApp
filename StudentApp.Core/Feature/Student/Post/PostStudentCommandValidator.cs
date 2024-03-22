using FluentValidation;
using StudentApp.Domain.Entities;
using StudentApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Post
{
    public class PostStudentCommandValidator : AbstractValidator<PostStudentCommand>
    {
        public PostStudentCommandValidator() 
        {
            RuleFor(x=>x.Id).NotNull();
            RuleFor(x=>x.Name).NotNull().NotEmpty();
            RuleFor(x=>x.Age).NotNull().NotEmpty().GreaterThan(0).WithMessage("Invalid age");
            RuleForEach(x => x.StudentDetailsModels).SetValidator(new ValidationForStudentAddress());
        }
    }

    public class ValidationForStudentAddress : AbstractValidator<StudentAddressModel>
    {
        public ValidationForStudentAddress() 
        {
            RuleFor(x=>x.Id).NotNull().NotEmpty();
            RuleFor(x=>x.AddressType).NotNull().NotEmpty().IsInEnum();
            RuleFor(x=>x.FullAddress).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(x=>x.StudentId).NotNull().NotEmpty();
        }
    }
}
