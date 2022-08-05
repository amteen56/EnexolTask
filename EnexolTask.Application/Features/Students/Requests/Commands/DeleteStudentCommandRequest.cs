using EnexolTask.Application.DTO.Student;
using EnexolTask.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.Features.Students.Requests.Commands
{
    public class DeleteStudentCommandRequest : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
