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
    public class GetStudentListWithFilterRequestHandler : IRequestHandler<GetStudentListWithFilterRequest, List<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public GetStudentListWithFilterRequestHandler(IMapper mapper, IStudentRepository studentRepository)
        {
            this._mapper = mapper;
            this._studentRepository = studentRepository;
        }

        public async Task<List<StudentDto>> Handle(GetStudentListWithFilterRequest request, CancellationToken cancellationToken)
        {
            var studentlist = await _studentRepository.GetStudentList(request.studentFilterDto);
            return _mapper.Map<List<StudentDto>>(studentlist);
        }
    }
}
