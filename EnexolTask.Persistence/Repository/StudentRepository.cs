using EnexolTask.Application.DTO.Student;
using EnexolTask.Application.Exception;
using EnexolTask.Application.Persistence.Contract.Persistence;
using EnexolTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnexolTask.Persistence.Repository
{
    internal class StudentRepository : IStudentRepository
    {
        public List<Student> Students;
        private readonly string fileName = "student.json";

        public StudentRepository()
        {
            var jsondata = File.ReadAllText(fileName);
            if (string.IsNullOrWhiteSpace(jsondata)) Students = new List<Student>();
            else Students = JsonSerializer.Deserialize<List<Student>>(jsondata);
        }
        private void save()
        {
            string json = JsonSerializer.Serialize(Students);
            File.WriteAllText(fileName, json);
        }

        public async Task<Student> Add(Student entity)
        {
            Students.Add(entity);
            save();
            return entity;
        }

        public async Task<IReadOnlyList<Student>> GetStudentList()
        {
            return Students;
        }

        public async Task<Student> Get(int Id)
        {
            var student = Students.FirstOrDefault(x => x.Id == Id);
            return student;
        }

        public async Task<Student> Update(Student entity)
        {
            save();
            return entity;
        }

        public async Task Delete(Student student)
        {
            Students.Remove(student);
            save();
        }

        public async Task<IReadOnlyList<Student>> GetStudentList(StudentFilterDto studentFilterDto)
        {
            var data = from student in Students
                       where (student.Name == studentFilterDto.Name || studentFilterDto.Name == null)
                       && (student.Age == studentFilterDto.Age || studentFilterDto.Age == null)
                       && (student.Email == studentFilterDto.Email || studentFilterDto.Email == null)
                       && (student.PhoneNo == studentFilterDto.Phone || studentFilterDto.Phone == null)
                       && (student.Gender == studentFilterDto.Gender || studentFilterDto.Gender == null)
                       select student;

            return data.ToList();
        }
    }
}


