using AutoMapper;
using EnexolTask.Application.DTO.Student;
using EnexolTask.Application.Features.Students.Requests.Queries;
using EnexolTask.Application.Persistence.Contract.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.Features.Students.Handlers.Queries
{
    public class GetLeaveAllocationListWithDetailsRequestHandler : IRequestHandler<GetStudentListRequest, List<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public GetLeaveAllocationListWithDetailsRequestHandler(IMapper mapper, IStudentRepository studentRepository)
        {
            this._mapper = mapper;
            this._studentRepository = studentRepository;
        }

        public async Task<List<StudentDto>> Handle(GetStudentListRequest request, CancellationToken cancellationToken)
        {
            var studentlist = await _studentRepository.GetStudentList();
            return _mapper.Map<List<StudentDto>>(studentlist);
        }
    }
}
