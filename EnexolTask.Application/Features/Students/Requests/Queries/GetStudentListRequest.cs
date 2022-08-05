using EnexolTask.Application.DTO.Student;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.Features.Students.Requests.Queries
{
    public class GetStudentListRequest : IRequest<List<StudentDto>>
    {
    }
}
