using AutoMapper;
using EnexolTask.Application.DTO.Student;
using EnexolTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, UpdateStudentDto>();
            CreateMap<Student, CreateStudentDto>().ReverseMap();
            CreateMap<UpdateStudentDto, Student>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
