using FluentValidation;
using StudentApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Get
{
    public class GetStudentRequestValidator : AbstractValidator<GetStudentRequest>
    {
        public GetStudentRequestValidator()
        {
            RuleFor(x => x.ResourceParameters).SetValidator(new ResourceParameterValidator());
        }
    }

    public class ResourceParameterValidator : AbstractValidator<ResourceParameters>
    {
        public ResourceParameterValidator()
        {
            RuleFor(x => x.OrderBy)
                .Must(x => x.ToLower().Contains("asc") || x.ToLower().Contains("desc"))
                .WithMessage("Invalid order by expression")
                .When(x => !string.IsNullOrWhiteSpace(x.OrderBy));

            RuleFor(x => x.AddressType).GreaterThanOrEqualTo(0);
            
            RuleFor(x => x.FieldName)
                .Must(x => (typeof(Entities.Student).GetProperty(x, BindingFlags.IgnoreCase) != null) || (typeof(Entities.StudentAddress).GetProperty(x, BindingFlags.IgnoreCase) != null))
                 .WithMessage("Invalid column name to order by")
                 .When(x => !string.IsNullOrWhiteSpace(x.FieldName));
        }
    }
}
