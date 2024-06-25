using NHibernate.Linq;
using QLSV.Data.Entities;
using QLSV.Data.Repositories.Contracts;
using QLSV.DTO.StudentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public async Task AddNewStudentAsync(Student student)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    var studentEntity = new Student()
                    {
                        FullName = student.FullName,
                        Birthday = student.Birthday,
                        Address = student.Address,
                        StudentClass = student.StudentClass,
                    };
                    await session.SaveAsync(studentEntity);
                    await transaction.CommitAsync();
                }
            }
        }

        public async Task DeleteStudentAsync(Student student)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.DeleteAsync(student);
                    await transaction.CommitAsync();
                }
            }
        }

        public async Task<List<GetAllStudentDTO>?> GetAllStudentsAsync()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return await session.Query<Student>()
                        .Fetch(s => s.StudentClass)
                        .Select(s => new GetAllStudentDTO
                        {
                            Id = s.Id,
                            Address = s.Address,
                            Birthday = s.Birthday.ToShortDateString(),
                            ClassName = s.StudentClass.Name,
                            FullName = s.FullName,
                        })
                        .ToListAsync();
                }
            }
        }

        public async Task<GetDetailStudentByIdDTO?> GetDetailStudentByIdAsync(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return await session.Query<Student>()
                        .Fetch(s => s.StudentClass)
                        .ThenFetch(c => c.ClassTeacher)
                        .Select(s => new GetDetailStudentByIdDTO
                        {
                            Id= s.Id,
                            Address = s.Address,
                            Birthday = s.Birthday.ToShortDateString(),
                            ClassName = s.StudentClass.Name,
                            FullName = s.FullName,
                            ClassSubject = s.StudentClass.Subject,
                            ClassTeacherFullName = s.StudentClass.ClassTeacher.FullName,
                            ClassTeacherBirthday = s.StudentClass.ClassTeacher.Birthday.ToShortDateString(),
                        })
                        .FirstOrDefaultAsync(s => s.Id == id);
                }
            }
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    return await session.GetAsync<Student>(id);
                }
            }
        }

        public async Task UpdateStudentAsync(Student student)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.UpdateAsync(student);
                    await transaction.CommitAsync();
                }
            }
        }
    }
}
