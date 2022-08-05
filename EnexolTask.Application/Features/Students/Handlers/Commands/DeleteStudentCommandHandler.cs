using AutoMapper;
using EnexolTask.Application.DTO.Student;
using EnexolTask.Application.DTO.Student.Validators;
using EnexolTask.Application.Exception;
using EnexolTask.Application.Features.Students.Requests.Commands;
using EnexolTask.Application.Persistence.Contract.Persistence;
using EnexolTask.Application.Responses;
using EnexolTask.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.Features.Students.Handlers.Commands
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommandRequest, BaseCommandResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public DeleteStudentCommandHandler(IStudentRepository studentRepo, IMapper mapper)
        {
            this._studentRepository = studentRepo;
            this._mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteStudentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var student = await _studentRepository.Get(request.Id);

            if (student == null) throw new NotFoundException(nameof(Student), request.Id);

            await _studentRepository.Delete(student);
            response.Success = true;
            response.Id = request.Id;
            response.Message = "Record Deleted";
            return response;
        }
    }
}
