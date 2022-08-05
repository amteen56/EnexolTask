using FluentValidation;

namespace EnexolTask.Application.DTO.Student.Validators
{
    public class IStudentDtoValidator : AbstractValidator<IStudentDto>
    {
        public IStudentDtoValidator()
        {
            List<char> conditions = new List<char>() { 'M', 'F', 'O' };

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.Gender)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(x => conditions.Contains(x)).WithMessage("{PropertyName} is Invalid Value Only ('M', 'F', 'O') are valid.");

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .EmailAddress().WithMessage("{PropertyName} is Invalid, A valid email is required");

            RuleFor(p => p.PhoneNo)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .Matches(@"[9]{1}[2]{1}[0-9]{10}").WithMessage("{PropertyName} is Invalid, Enter Phone No like 92xxxxxxxxxx");
        }
    }
}