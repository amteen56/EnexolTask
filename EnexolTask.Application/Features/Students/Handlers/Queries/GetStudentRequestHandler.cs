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
    public class GetStudentRequestHandler : IRequestHandler<GetStudentRequest, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentRequestHandler(IStudentRepository studentRepo, IMapper mapper)
        {
            this._studentRepository = studentRepo;
            this._mapper = mapper;
        }

        public async Task<StudentDto> Handle(GetStudentRequest request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.Get(request.Id);
            return _mapper.Map<StudentDto>(student);
        }
    }
}
