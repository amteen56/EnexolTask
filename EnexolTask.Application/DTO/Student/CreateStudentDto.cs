using EnexolTask.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.DTO.Student
{
    public class CreateStudentDto : BaseDTO, IStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public char Gender { get; set; }
    }
}
