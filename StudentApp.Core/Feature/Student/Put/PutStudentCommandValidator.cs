using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Put
{
    public class PutStudentCommandValidator : AbstractValidator<PutStudentCommand>
    {
        public PutStudentCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().NotNull();
            RuleFor(x=>x.Name).NotEmpty().NotNull();
            RuleFor(x=>x.Age).NotNull().NotNull().GreaterThan(0).WithMessage("Invalid roll");
            RuleForEach(x => x.PutStudentDetailsModels).SetValidator(new PutStudentAddressValidator());
        }
    }

    public class PutStudentAddressValidator : AbstractValidator<PutStudentDetailsModel>
    {
        public PutStudentAddressValidator()
        {
            RuleFor(x=>x.AddressId).NotEmpty().NotNull();
            RuleFor(x=>x.StudentId).NotEmpty().NotNull();
            RuleFor(x=>x.AddressType).NotEmpty().NotNull().IsInEnum().WithMessage("Invalida Address Type");
            RuleFor(x=>x.FullAddress).NotEmpty().NotNull().MinimumLength(5);
        }
    }
}
