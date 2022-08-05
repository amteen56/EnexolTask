using AutoMapper;
using EnexolTask.Application.DTO.Student.Validators;
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
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommandRequest, BaseCommandResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public CreateStudentCommandHandler(IStudentRepository studentRepo, IMapper mapper)
        {
            this._studentRepository = studentRepo;
            this._mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateStudentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateStudentValidatorDto();
            var validationResult = await validator.ValidateAsync(request.createStudentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var student = _mapper.Map<Student>(request.createStudentDto);

                student = await _studentRepository.Add(student);

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = student.Id;
            }

            return response;
        }
    }
}
