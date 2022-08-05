using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.DTO.Student.Validators
{
    internal class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            Include(new IStudentDtoValidator());

            RuleFor(s => s.FatherName).MaximumLength(50)
              .WithMessage("PropertyName} must not exceed {ComparisonValue} characters")
              .When(s => !string.IsNullOrEmpty(s.FatherName));

            RuleFor(p => p.Age)
             .InclusiveBetween(1,120)
             .WithMessage("{PropertyName} is not valid must be between 1 - 120")
             .When(ss => ss.Age != null);
        }
    }
}
