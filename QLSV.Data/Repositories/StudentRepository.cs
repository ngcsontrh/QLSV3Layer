using NHibernate.Linq;
using QLSV.Data.Entities;
using QLSV.Data.Repositories.Contracts;
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

        public async Task<List<Student>?> GetAllStudentsAsync()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    List<Student> students = await session.Query<Student>()
                        .Fetch(s => s.StudentClass)
                        .ToListAsync();
                    return students;
                }
            }
        }

        public async Task<Student?> GetStudentDetailsByIdAsync(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Student student = await session.Query<Student>()
                        .Fetch(s => s.StudentClass)
                        .ThenFetch(c => c.ClassTeacher)
                        .FirstOrDefaultAsync(s => s.Id == id);
                    return student;
                }
            }
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Student student = await session.Query<Student>()
                        .Fetch(s => s.StudentClass)
                        .ThenFetch(c => c.ClassTeacher)
                        .FirstOrDefaultAsync(s => s.Id == id);
                    return student;
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

        public async Task<int> CountStudentsAsync()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    int count = await session.Query<Student>().CountAsync();
                    return count;
                }
            }
        }
    }
}
