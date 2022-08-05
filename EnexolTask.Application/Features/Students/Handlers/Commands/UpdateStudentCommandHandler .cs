using AutoMapper;
using EnexolTask.Application.DTO.Student;
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
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommandRequest, UpdateCommandResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public UpdateStudentCommandHandler(IStudentRepository studentRepo, IMapper mapper)
        {
            this._studentRepository = studentRepo;
            this._mapper = mapper;
        }

        public async Task<UpdateCommandResponse> Handle(UpdateStudentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateCommandResponse();
            var validator = new UpdateStudentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.updateStudentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Updation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var studentRecord = await _studentRepository.Get(request.updateStudentDto.Id);
                if (studentRecord == null)//create new 
                {
                    var student = _mapper.Map<Student>(request.updateStudentDto);
                    studentRecord = await _studentRepository.Add(student);
                    response.Message = "New Record Creation Successful";
                }
                else//udpate
                {
                    _mapper.Map(request.updateStudentDto, studentRecord);
                    studentRecord = await _studentRepository.Update(studentRecord);
                    response.Message = "Updation Successful";
                }
                response.Success = true;
                response.dto = _mapper.Map<StudentDto>(studentRecord);
            }
            return response;
        }
    }
}
