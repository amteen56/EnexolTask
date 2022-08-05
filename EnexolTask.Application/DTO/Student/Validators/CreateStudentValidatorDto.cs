using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.DTO.Student.Validators
{
    public class CreateStudentValidatorDto : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentValidatorDto()
        {
            Include(new IStudentDtoValidator());
        }
    }
}
