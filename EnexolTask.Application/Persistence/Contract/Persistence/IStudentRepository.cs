using EnexolTask.Application.DTO.Student;
using EnexolTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.Persistence.Contract.Persistence
{
    public interface IStudentRepository
    {
        Task<IReadOnlyList<Student>> GetStudentList();
        Task<Student> Get(int Id);
        Task<Student> Add(Student entity);
        Task<Student> Update(Student entity);
        Task Delete(Student student);
        Task<IReadOnlyList<Student>> GetStudentList(StudentFilterDto studentFilterDto);
    }
}
